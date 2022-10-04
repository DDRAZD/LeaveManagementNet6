using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configuration
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var hasher = new PasswordHasher<Employee>();
            builder.HasData(
                new Employee
                {
                    Id = "82f7a62a-832a-418b-b6ae-340d98f3e40b",
                    Email= "admin2@test.com",
                    NormalizedEmail= "ADMIN2@TEST.COM",
                    FirstName ="System",
                    LastName="Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin12345!")

                }

                );
        }
    }
}