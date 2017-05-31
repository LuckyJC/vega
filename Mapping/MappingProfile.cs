using AutoMapper;
using System.Linq;
using vega.Controllers.Resources;
using vega.Models;
using System.Collections.Generic;

namespace vega.Mapping
{
    public class MappingProfile : Profile
    {
         public MappingProfile()
         {
             //Domain to API resource; can cut and paste into separate profiles if app gets larger
             CreateMap<Make, MakeResource>();
             CreateMap<Model, ModelResource>();
             CreateMap<Feature, FeatureResource>();
             CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

             //API resource to domain
             CreateMap<VehicleResource, Vehicle>()
                //this line tells automapper to ignore mapping id property
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                //ignoring the mapping of features because we need to do some processing
                .AfterMap((vr, v) => {
                    //remove unselected features
                    var removedFeatures = new List<VehicleFeature>(); //need to create list to store removed features; can't remove directly or you get runtime error
                    foreach(var f in v.Features)
                        if(!vr.Features.Contains(f.FeatureId))
                            removedFeatures.Add(f);
                    //iterate over removed features list and remove from Features collection
                    foreach(var f in removedFeatures)
                        v.Features.Remove(f);

                    //add new features
                    foreach(var id in vr.Features)
                        if(!v.Features.Any(f => f.FeatureId == id))  //if we don't have a feature with this identifier, add to collection
                            v.Features.Add(new VehicleFeature { FeatureId = id})  //initialize immediately
                });
                
         }
    }
}