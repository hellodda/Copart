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

        public DbSet<Bid> Bids { get; set; } = default!;

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
            builder.HasKey(lot => lot.Id);
            builder.Property(lot => lot.LotNumber).IsRequired();
            builder.HasOne(lot => lot.Vehicle);

            builder.HasMany(lot => lot.Bids)
                .WithOne(bid => bid.Lot)
                .HasForeignKey(bid => bid.LotId);
        }
    }

    internal class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(bid => bid.Id);
            builder.Property(bid => bid.UserId).IsRequired();
            builder.HasOne(bid => bid.User);
            builder.HasOne(bid => bid.Lot);
        }
    }
}
