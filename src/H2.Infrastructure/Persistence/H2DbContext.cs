using H2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace H2_Infrastructure.Persistence
{
    public class H2DbContext : DbContext
    {
        public H2DbContext(DbContextOptions<H2DbContext> options) : base(options)
        {
        }

        public DbSet<SensorData> sensorDatas { get; set; } = null!;
        public DbSet<ThermalImage> thermalImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.HydrogenPpm).IsRequired();
                entity.Property(e => e.AlertLevel)
                    .HasConversion<string>()
                    .IsRequired();
            });

            modelBuilder.Entity<ThermalImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DeviceId).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.ImageType)
                    .HasConversion<string>()            
                    .IsRequired();
            });
        }
        
    }
}
