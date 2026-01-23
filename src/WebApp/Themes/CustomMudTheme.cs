using MudBlazor;

namespace WebApp.Themes
{
    public static class CustomMudTheme
    {
        public static MudTheme AgcTheme = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Brand core (Navy + Magenta) — plus "premium"
                Primary = "#0B2A5B",         // deep navy
                PrimaryDarken = "#071C3D",
                PrimaryLighten = "#133C7A",

                Secondary = "#E11D48",       // modern magenta/rose
                SecondaryDarken = "#B81437",
                SecondaryLighten = "#FF4D7D",

                // Backgrounds / Surfaces
                Background = "#F5F7FB",      // soft neutral
                Surface = "#FFFFFF",

                // AppBar / Drawer
                AppbarBackground = "#0B2A5B",
                AppbarText = Colors.Shades.White,

                DrawerBackground = "#FFFFFF",
                DrawerText = "#0F172A",
                DrawerIcon = "#0B2A5B",

                // Text
                TextPrimary = "#0B1220",
                TextSecondary = "#475569",   // slate 600

                // Status
                Success = "#16A34A",
                Warning = "#F59E0B",
                Error = "#DC2626",
                Info = "#2563EB",

                // Lines / borders
                LinesDefault = "#E6EAF2",

                // Nice-to-have extras (si ta version MudBlazor les expose)
                // Divider = "#E6EAF2",
                // ActionDefault = "#94A3B8",
                // ActionDisabled = "#CBD5E1",
                // ActionDisabledBackground = "#EEF2FF",
                // ActionDisabledOpacity = 0.55,
                // HoverOpacity = 0.08,
            },

            PaletteDark = new PaletteDark()
            {
                // Dark brand alignment (deep + comfortable)
                Primary = "#6C8CFF",
                PrimaryDarken = "#4B6BFF",
                PrimaryLighten = "#9DB2FF",

                Secondary = "#FF4D7D",
                SecondaryDarken = "#E11D48",
                SecondaryLighten = "#FF7AA0",

                Background = "#060A14",      // blue-black
                Surface = "#0B1220",         // cards

                AppbarBackground = "#070F22",
                AppbarText = Colors.Shades.White,

                DrawerBackground = "#0B1220",
                DrawerText = "#E5E7EB",
                DrawerIcon = "#6C8CFF",

                TextPrimary = "#E5E7EB",
                TextSecondary = "#A3ADC2",

                Success = "#22C55E",
                Warning = "#FBBF24",
                Error = "#F87171",
                Info = "#6C8CFF",

                LinesDefault = "#1E2A44",

                // Extras optionnels selon version
                // Divider = "#1E2A44",
                // ActionDefault = "#A3ADC2",
                // ActionDisabled = "#5B6478",
                // ActionDisabledBackground = "#0F172A",
                // ActionDisabledOpacity = 0.5,
                // HoverOpacity = 0.08,
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "280px",
                DrawerWidthRight = "320px",
                DefaultBorderRadius = "16px"
            },

            Shadows = new Shadow()
            {
                // Un peu plus doux/élégant que les défauts (si tu veux)
                // Elevation1..25 existent selon versions
            },

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = new[] { "Inter", "Segoe UI", "Roboto", "Arial", "sans-serif" }
                },

                H6 = new H6Typography()
                {
                    FontWeight = "700",
                    LetterSpacing = "0.2px"
                },

                Body1 = new Body1Typography()
                {
                    LineHeight = "1.55"
                }
            }
        };
    }
}
