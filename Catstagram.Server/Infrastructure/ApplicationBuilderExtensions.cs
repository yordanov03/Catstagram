using Catstagram.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Catstagram.Server.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(ApplicationBuilder app)
        {
            return app.UseSwagger()
                        .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<CatstagramDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
