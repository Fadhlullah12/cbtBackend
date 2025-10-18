using cbtBackend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SubjectName)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne(s => s.SubAdmin)
               .WithMany(sa => sa.Subjects)
               .HasForeignKey(s => s.SubAdminId);

        builder.HasMany(s => s.StudentSubjects)
               .WithOne()
               .HasForeignKey("SubjectId");

        builder.HasMany(s => s.Results)
               .WithOne()
               .HasForeignKey("SubjectId");

        builder.HasMany(s => s.Questions)
               .WithOne()
               .HasForeignKey("SubjectId");

        builder.HasMany(s => s.Exams)
               .WithOne()
               .HasForeignKey("SubjectId");
    }
}
