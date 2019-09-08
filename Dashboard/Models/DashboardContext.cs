using Microsoft.EntityFrameworkCore;

namespace Dashboard.Models
{
    public class DashboardContext : DbContext
    {
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<PackageNote> PackageNotes { get; set; }
        public DbSet<Picker> Pickers { get; set; }

        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryOrder>()
              .HasKey(x => new { x.DeliveryOrderId, x.TransCode, x.BranchId });
            modelBuilder.Entity<Location>()
                .HasKey(x => x.LocationId);
            modelBuilder.Entity<Location>()
                .Property(x => x.LocationId)
                .ValueGeneratedNever();
            modelBuilder.Entity<Location>()
                .HasMany(x => x.DeliveryOrders)
                .WithOne(x => x.Location);
            modelBuilder.Entity<Line>()
              .HasOne(l => l.DeliveryOrder)
              .WithMany(x => x.Lines)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PackageNote>()
              .HasOne(l => l.DeliveryOrder)
              .WithMany(x => x.PackageNotes)
              .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Picker>()
              .HasOne(l => l.DeliveryOrder)
              .WithMany(x => x.Pickers)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
