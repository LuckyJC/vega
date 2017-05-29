using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        //properties
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

        //constructor
        public VegaDbContext(DbContextOptions<VegaDbContext> options)
            : base(options)
        {
        }

        //other methods
        //need to override OnModelCreating to make a composite primary key using vehicle id and feature id
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
                new { vf.VehicleId, vf.FeatureId });
        }
    }
}