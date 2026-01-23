using System.Globalization;

namespace WebApp.Components.Layout
{
    public partial class LocalizationSelector
    {
        private string selectedCulture = CultureInfo.CurrentCulture.Name;
        private Dictionary<string, string?> cultures = new();
        private Uri? flagUrlBase;
        protected override void OnInitialized()
        {
            cultures = Configuration.GetSection("Cultures")
                .GetChildren()
                .ToDictionary(x => x.Key, x => x.Value);

            var template = Configuration["CdnConfig:FlagUrlTemplate"];

            // On tente de créer l'URI, si ça échoue, flagUrlBase restera nul
            if (!string.IsNullOrEmpty(template))
            {
                Uri.TryCreate(template, UriKind.Absolute, out flagUrlBase);
            }
        }

        private string GetFlagUrl(string cultureCode)
        {
            if (flagUrlBase == null) return string.Empty;

            var countryCode = cultureCode.Split('-').Last().ToLower(); 
            var relativePath = $"{countryCode}.png";
            return new Uri(flagUrlBase, relativePath).ToString();
        }

        private void OnCultureChanged(string newCulture)
        {
            selectedCulture = newCulture;
            RequestCultureChange();
        }

        private void RequestCultureChange()
        {
            var uri = new Uri(Nav.Uri)
                .GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);

            var query = $"?culture={Uri.EscapeDataString(selectedCulture)}&" +
                        $"redirectUri={Uri.EscapeDataString(uri)}";

            Nav.NavigateTo("/culture/SetCulture" + query, forceLoad: true);
        }
    }
}