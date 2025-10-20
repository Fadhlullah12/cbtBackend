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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    Id = "AMIN",
                    Password = BCrypt.Net.BCrypt.HashPassword("Amin@@77"),
                    Email = "AminOmoyele@gmail.com",
                    IsDeleted = false,
                    Role = "Administrator",
                    FirstName = "Amin",
                    LastName = "Omoyele"
                }
            );

        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<SubAdmin> SubAdmins { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }


    }
}