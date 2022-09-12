using Catstagram.Server.Data;
using Catstagram.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var appSettings = builder.Services.GetApplicationSettings(builder.Configuration);

builder.Services.AddDbContext<CatstagramDbContext>(options =>
    options.UseSqlServer(connectionString))
    .AddIdentity()
    .AddJWTAuthentication(appSettings)
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseCors(options => options
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.ApplyMigrations();
app.Run();
