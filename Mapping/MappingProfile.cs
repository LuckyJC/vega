using AutoMapper;
using System.Linq;
using vega.Controllers.Resources;
using vega.Core.Models;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
         public MappingProfile()
         {
            //Domain to API resource; can cut and paste into separate profiles if app gets larger
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
               .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
               .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make)) //this means that eager loading the model will also include the make
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource {Id = vf.Feature.Id, Name = vf.Feature.Name })));


            //API resource to domain
            CreateMap<SaveVehicleResource, Vehicle>()
               //this line tells automapper to ignore mapping id property
               .ForMember(v => v.Id, opt => opt.Ignore())
               .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
               .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
               .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
               .ForMember(v => v.Features, opt => opt.Ignore())
               .AfterMap((vr, v) =>
               {
                    //remove unselected features
                    var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                   foreach (var f in removedFeatures)
                       v.Features.Remove(f);

                    //add new features
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                   foreach (var f in addedFeatures)
                       v.Features.Add(f);
               });

        }
    }
}