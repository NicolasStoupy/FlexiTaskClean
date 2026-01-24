namespace WebApp.Services
{
    /// <summary>
    /// Fournit des options de localisation (<see cref="RequestLocalizationOptions"/>) en lisant la section "Cultures"
    /// de l'<see cref="IConfiguration"/>.
    /// </summary>
    /// <remarks>
    /// Format attendu dans la configuration (par exemple appsettings.json) :
    /// {
    ///   "Cultures": {
    ///     "fr-FR": "Français (France)",
    ///     "en-US": "English (US)"
    ///   }
    /// }
    /// Les clés de la section "Cultures" sont utilisées comme codes de cultures supportées.
    /// </remarks>
    public class LocalizationOptionsProvider
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Crée une nouvelle instance de <see cref="LocalizationOptionsProvider"/>.
        /// </summary>
        /// <param name="configuration">Configuration utilisée pour lire la section "Cultures".</param>
        public LocalizationOptionsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Construit et retourne une instance de <see cref="RequestLocalizationOptions"/> initialisée
        /// avec les cultures définies dans la section "Cultures" de la configuration.
        /// </summary>
        /// <returns>
        /// Un <see cref="RequestLocalizationOptions"/> dont les collections SupportedCultures et SupportedUICultures
        /// sont peuplées à partir des clés trouvées dans la section "Cultures".
        /// </returns>
        /// <remarks>
        /// Si la section "Cultures" est absente ou ne contient aucune entrée, les collections retournées seront vides.
        /// </remarks>
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