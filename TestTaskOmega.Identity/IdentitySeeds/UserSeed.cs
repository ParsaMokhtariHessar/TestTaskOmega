using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTaskOmega.Identity.IdentityModels;

namespace TestTaskOmega.Identity.IdentitySeeds
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@admin.com",
                     NormalizedEmail = "ADMIN@admin.COM",
                     UserName = "admin",
                     NormalizedUserName = "ADMIN",
                     PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssword1"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
