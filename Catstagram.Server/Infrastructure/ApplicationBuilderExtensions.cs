using Catstagram.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<CatstagramDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
