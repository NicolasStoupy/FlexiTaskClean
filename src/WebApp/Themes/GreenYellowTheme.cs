using MudBlazor;

namespace WebApp.Themes
{
    // Source - https://stackoverflow.com/a
    // Posted by martinstoeckli
    // Retrieved 2026-01-20, License - CC BY-SA 4.0

    public class GreenYellowTheme : MudTheme
    {
        public GreenYellowTheme()
        {
            PaletteLight = new PaletteLight
            {
                Black = "#000000",
                White = "#FFFFFF",
                Primary = "#FF0000",
                Secondary = "#00FF00",
                Tertiary = "#0000FF",
                Success = "#00FFFF",
                Info = "#FFFF00",
                Warning = "#FF00FF",
                Error = "#C0C0C0",
                Dark = "#333333",
                Background = "#FFFFFF",
            };

            PaletteDark = new PaletteDark
            {
                Black = "#000000",
                White = "#FFFFFF",
                Primary = "#FFFFFF", //white
                Secondary = "#c5b858", // yellow
                Tertiary = "#1b5e20", // green
                Success = "#00FFFF",
                Info = "#FFFF00",
                Warning = "#FF00FF",
                Error = "#C0C0C0",
                Dark = "#303030",
                Background = "#303030",
            };
        }
    }

}
