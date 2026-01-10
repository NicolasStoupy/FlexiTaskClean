namespace WebApp.Services
{
    public class LocalizationOptionsProvider
    {
        private readonly IConfiguration _configuration;

        public LocalizationOptionsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RequestLocalizationOptions GetLocalizationOptions()
        {
            var cultures = _configuration.GetSection("Cultures")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);
            var supportedCultures = cultures.Keys.ToArray();
            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            return localizationOptions;
        }
    }
}
