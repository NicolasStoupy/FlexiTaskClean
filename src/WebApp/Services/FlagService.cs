namespace WebApp.Services
{
    public class FlagService
    {
        private readonly Uri _flagUrlBase;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="FlagService"/>.
        /// Lit la valeur de configuration <c>CdnConfig:FlagUrlTemplate</c> et la valide comme une URI absolue.
        /// </summary>
        /// <param name="config">Source de configuration (par ex. <c>appsettings.json</c> ou variables d'environnement).</param>
        /// <param name="logger">Instance de <see cref="ILogger{FlagService}"/> utilisée pour journaliser les erreurs critiques.</param>
        /// <exception cref="InvalidOperationException">
        /// Levée lorsque la clé de configuration <c>CdnConfig:FlagUrlTemplate</c> est absente, vide ou que sa valeur
        /// n'est pas une URI absolue valide. Dans ces cas, un message critique est également enregistré via <paramref name="logger"/>.
        /// </exception>
        public FlagService(IConfiguration config, ILogger<FlagService> logger)
        {
            var url = config["CdnConfig:FlagUrlTemplate"];

            if (string.IsNullOrWhiteSpace(url))
            {
                logger.LogCritical("CdnConfig:FlagUrlTemplate is missing in configuration.");
                throw new InvalidOperationException(
                    "Missing configuration: CdnConfig:FlagUrlTemplate");
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                logger.LogCritical("Invalid CdnConfig:FlagUrlTemplate value: {Url}", url);
                throw new InvalidOperationException(
                    $"Invalid URL in configuration: CdnConfig:FlagUrlTemplate = '{url}'");
            }

            _flagUrlBase = uri;
        }

        public string GetFlagUrl(string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
                return string.Empty;

            var countryCode = cultureCode.Split('-').Last().ToLowerInvariant();
            return new Uri(_flagUrlBase, $"{countryCode}.png").ToString();
        }
    }
}
