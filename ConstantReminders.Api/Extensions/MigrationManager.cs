using ConstantReminders.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstantReminder.Api.Extensions;

public static class MigrationManager
{
    public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        await using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await appContext.Database.MigrateAsync();

        return webApp;
    }
}