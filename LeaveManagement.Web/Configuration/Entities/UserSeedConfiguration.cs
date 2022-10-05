using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configuration.Entities
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
                    Email = "admin2@test.com",
                    NormalizedEmail = "ADMIN2@TEST.COM",
                    UserName = "admin2@test.com",
                    NormalizedUserName = "ADMIN2@TEST.COM",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "Admin12345!"),
                    EmailConfirmed = true

                },
                new Employee
                {
                    Id = "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                    Email = "user@localhost.com",
                    NormalizedEmail = "USER@LOCALHOST.COM",
                    UserName = "user@localhost.com",
                    NormalizedUserName = "USER@LOCALHOST.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "User12345!"),
                    EmailConfirmed = true

                }

                ); ;
        }
    }
}