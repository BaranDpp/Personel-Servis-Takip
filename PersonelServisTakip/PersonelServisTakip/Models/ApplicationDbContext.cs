using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonelServisTakip.Models.Entity;

namespace PersonelServisTakip.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet'lerinizi burada tanımlayın
        public DbSet<Personel> Personels { get; set; }

        public DbSet<ServiceVehicle> ServiceVehicles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<PersonelTask> PersonelTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Personel>()
                .Ignore(p => p.PhotoFile); // IFormFile özelliğini görmezden gel
        }

    }
        
}