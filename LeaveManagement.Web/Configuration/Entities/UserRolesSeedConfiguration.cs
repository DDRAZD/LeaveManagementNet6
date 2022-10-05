using LeaveManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Web.Configuration.Entities
{
    public class UserRolesSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId= "0e496d22-f74d-4bd4-9be2-f3b0436f29ed",
                    UserId= "82f7a62a-832a-418b-b6ae-340d98f3e40b"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "82f7772a-888a-418b-b6ae-340d98f3e40b",
                     UserId = "0e496d22-f74d-4bd4-9be2-f3b0436f29ed"
                 }


                );
        }
    }
}