using Barghto_Ticketing.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barghto_Ticketing.Data.EntitiesConfig;

public class TicketConfig : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(el => el.Title)
           .HasMaxLength(128)
           .IsRequired();
        builder.Property(el => el.Description)
          .HasMaxLength(500)
          .IsRequired();

        builder.Property(el => el.Status)
          .IsRequired();

        builder.Property(el => el.Priority)
         .IsRequired();

        builder.Property(el => el.CreatedAt)
         .IsRequired();

        builder.Property(el => el.UpdatedAt)
        .IsRequired();

    }
}