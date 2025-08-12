using cbtBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Context
{
    public class ApplicationContext : DbContext
    {
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    Id = "AMIN",
                    Password = "Amin@@77",
                    IsDeleted = false,
                    Role = "Administrator",
                    FirstName = "Amin",
                    LastName = "Omoyele"
                }
            );
      
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
    
    }
}