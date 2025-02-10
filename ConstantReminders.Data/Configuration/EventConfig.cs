using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ConstantReminders.Contracts.Models;

namespace ConstantReminders.Data.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id)
              .IsRequired()
              .ValueGeneratedNever();
    }
}

