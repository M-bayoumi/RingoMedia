using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RingoMedia.DAL.Data.Models;

namespace RingoMedia.DAL.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepartmentID);

            builder.HasMany(d => d.SubDepartments)
                   .WithOne(d => d.ParentDepartment)
                   .HasForeignKey(d => d.ParentDepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
