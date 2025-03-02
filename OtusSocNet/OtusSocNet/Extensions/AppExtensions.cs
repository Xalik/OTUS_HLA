using Microsoft.EntityFrameworkCore;
using OtusSocNet.DAL;

namespace OtusSocNet.Extensions;

public static class AppExtensions
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<OtusSocNetDbContext>();
        dataContext.Database.Migrate();
    }
}