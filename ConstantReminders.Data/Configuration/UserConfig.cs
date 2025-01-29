using ConstantReminders.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstantReminders.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Create an index on Auth0Id
            builder.HasIndex(u => u.AuthOId)
                   .IsUnique(); 
        }
    }
}
