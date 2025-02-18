using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniShip.Domain.Users;

namespace UniShip.Infrastructure.Configurations;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.ToTable("UserRoles");

        // Primary Key (Birçok-to-bir ilişkisi olduğu için her iki kolonu da birleştiriyoruz)
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasOne<AppUser>()
            .WithMany()  // Her kullanıcı, birçok role sahip olabilir.
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silindiğinde rol ilişkisinin de silinmesi sağlanır

        builder.HasOne<IdentityRole<Guid>>()
            .WithMany() // Her rol, birçok kullanıcıya sahip olabilir.
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // Rol silindiğinde ilişkiyi kaldırır.

        builder.Property(ur => ur.UserId).HasColumnName("UserId");
        builder.Property(ur => ur.RoleId).HasColumnName("RoleId");
    }
}
