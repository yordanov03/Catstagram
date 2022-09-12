namespace Catstagram.Server.Infrastructure
{
    public static class ConfigurationExtensions
    {
        public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            var appSettings = applicationSettingsConfiguration.Get<AppSettings>();
            return appSettings;
        }
    }
}
