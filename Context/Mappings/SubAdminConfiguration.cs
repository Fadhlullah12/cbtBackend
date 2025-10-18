using cbtBackend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubAdminConfiguration : IEntityTypeConfiguration<SubAdmin>
{
    public void Configure(EntityTypeBuilder<SubAdmin> builder)
    {
        builder.HasKey(s => s.UserId);

        builder.HasOne(s => s.User)
               .WithOne()
               .HasForeignKey<SubAdmin>(s => s.UserId);

        builder.HasMany(s => s.Students)
               .WithOne()
               .HasForeignKey("SubAdminId");

        builder.HasMany(s => s.Subjects)
               .WithOne()
               .HasForeignKey("SubAdminId");

        builder.HasMany(s => s.Exams)
               .WithOne()
               .HasForeignKey("SubAdminId");
    }
}
