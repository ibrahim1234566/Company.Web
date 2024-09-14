using Company.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Contexts.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(e => e.Age)
                   .IsRequired();

            builder.Property(e => e.Address)
                   .HasMaxLength(200); 

            builder.Property(e => e.Salary)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Email)
                   .HasMaxLength(150);

            builder.Property(e => e.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(e => e.HiringDate)
                   .IsRequired();

            builder.Property(e => e.ImgeUrl)
                   .HasMaxLength(250); 

            builder.Property(e => e.IsDeleted)
                   .IsRequired();

            builder.Property(e => e.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
