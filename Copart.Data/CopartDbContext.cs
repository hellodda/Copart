using Copart.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Copart.Data
{
    public class CopartDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        public DbSet<Lot> Lots { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;

        public CopartDbContext(DbContextOptions<CopartDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehiclesConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    internal class VehiclesConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(vhs => vhs.Id);
            builder.Property(vhs => vhs.Vin).IsRequired();
            builder.Property(vhs => vhs.Make).IsRequired();
            builder.Property(vhs => vhs.Model).IsRequired();
        }
    }

    internal class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.LotNumber).IsRequired();
            builder.HasOne(l => l.Vehicle);
           
            builder.Property(p => p.CurrentBid)
            .HasPrecision(18, 2); 

            builder.Property(p => p.MinimalBid)
            .HasPrecision(18, 2);
        }
    }

}
