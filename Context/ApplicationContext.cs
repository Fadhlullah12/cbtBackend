using cbtBackend.Context.Mappings;
using cbtBackend.Model;
using cbtBackend.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace cbtBackend.Context
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// {
//     optionsBuilder.UseMySql("your_mysql_connection_string", ServerVersion.AutoDetect("your_mysql_connection_string"))
//         .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
// }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    Id = "AMIN",
                    IsDeleted = false,
                    FirstName = "Amin",
                    LastName = "Omoyele",
                    Role = "Administrator",
                    Email = "AminOmoyele@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Amin@@77"),
                }
            );
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubAdmin> SubAdmins { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

    
    }
}