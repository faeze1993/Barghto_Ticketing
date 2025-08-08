using Barghto_Ticketing.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barghto_Ticketing.Data.EntitiesConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(el => el.FullName)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(el => el.Password)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(el => el.Email)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(el => el.Role)
          .IsRequired();

        builder.HasMany<Ticket>()
            .WithOne()
            .HasForeignKey(el => el.CreatedByUserId)
            .IsRequired();

        builder.HasMany<Ticket>()
           .WithOne()
           .HasForeignKey(el => el.AssignedToUserId)
           .IsRequired(false);
    }
}
