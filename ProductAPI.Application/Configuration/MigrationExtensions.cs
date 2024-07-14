using Microsoft.EntityFrameworkCore;
using ProductAPI.Infra;

namespace ProductAPI.Application.Configuration
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductAPIContext>();
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }
        }
    }
}
