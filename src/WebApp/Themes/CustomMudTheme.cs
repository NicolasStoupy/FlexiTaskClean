using MudBlazor;

namespace WebApp.Themes
{
    /// <summary>
    /// Fournit des thèmes MudBlazor prédéfinis et réutilisables pour l'application.
    /// </summary>
    /// <remarks>
    /// Cette classe regroupe plusieurs instances statiques de <see cref="MudTheme"/> (ex : <see cref="AgcTheme"/>,<br />
    /// <see cref="AgcTheme2"/>, <see cref="AgcThemeElectricCyber"/>, <see cref="AgcThemeGlassmorphism"/> et<br />
    /// <see cref="AgcThemeOfficial"/>). Chaque thème contient une configuration complète pour
    /// <c>PaletteLight</c>, <c>PaletteDark</c>, <c>LayoutProperties</c>, <c>Typography</c> et d'autres options
    /// spécifiques à MudBlazor.
    /// </remarks>
    public static class CustomMudTheme
    {
        /// <summary>
        /// Thème "AGC" — palette principale navy + magenta, look premium.
        /// </summary>
        /// <remarks>
        /// Conçu pour une interface élégante, avec un fond clair doux et une version sombre cohérente.
        /// Utilisez ce thème lorsque vous voulez un rendu professionnel, contrasté et moderne.
        /// </remarks>
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
                DrawerWidthLeft = "260px",     // 280 -> 260
                DrawerWidthRight = "300px",    // 320 -> 300
                DefaultBorderRadius = "10px"   // 16 -> 10 
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

        /// <summary>
        /// Thème "AGC 2" — palette émeraude + charcoal, look moderne et légèrement sharp.
        /// </summary>
        /// <remarks>
        /// Pensé pour des interfaces où la lisibilité et la modernité sont prioritaires.
        /// Comporte des variantes claires et sombres calibrées pour un bon contraste.
        /// </remarks>
        public static MudTheme AgcTheme2 = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Brand core (Emerald + Charcoal)
                Primary = "#065F46",          // Vert émeraude profond
                PrimaryDarken = "#064E3B",
                PrimaryLighten = "#10B981",

                Secondary = "#F59E0B",        // Ambre pour les boutons d'action (contraste fort)
                SecondaryDarken = "#D97706",
                SecondaryLighten = "#FBBF24",

                // Backgrounds / Surfaces
                Background = "#F8FAFC",       // Blanc cassé bleuté très léger
                Surface = "#FFFFFF",

                // AppBar / Drawer
                AppbarBackground = "#065F46",
                AppbarText = Colors.Shades.White,

                DrawerBackground = "#FFFFFF",
                DrawerText = "#1E293B",
                DrawerIcon = "#065F46",

                // Text
                TextPrimary = "#0F172A",      // Slate 900
                TextSecondary = "#64748B",    // Slate 500

                // Status
                Success = "#10B981",
                Warning = "#F59E0B",
                Error = "#E11D48",
                Info = "#0EA5E9",

                // Lines / borders
                LinesDefault = "#E2E8F0",
                Divider = "#E2E8F0",
            },

            PaletteDark = new PaletteDark()
            {
                // Dark Forest (confort visuel maximal)
                Primary = "#34D399",          // Émeraude clair pour le mode sombre
                PrimaryDarken = "#10B981",
                PrimaryLighten = "#6EE7B7",

                Secondary = "#FBBF24",
                SecondaryDarken = "#F59E0B",
                SecondaryLighten = "#FCD34D",

                Background = "#0F172A",       // Slate 900 (mieux que le noir pur)
                Surface = "#1E293B",          // Slate 800 (cartes légèrement plus claires)

                AppbarBackground = "#0F172A",
                AppbarText = "#F8FAFC",

                DrawerBackground = "#1E293B",
                DrawerText = "#F1F5F9",
                DrawerIcon = "#34D399",

                TextPrimary = "#F1F5F9",
                TextSecondary = "#94A3B8",

                Success = "#10B981",
                Warning = "#FBBF24",
                Error = "#FB7185",
                Info = "#38BDF8",

                LinesDefault = "#334155",
                Divider = "#334155",
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DrawerWidthRight = "300px",
                DefaultBorderRadius = "12px"   // Un peu plus "sharp" mais moderne
            },

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = new[] { "Plus Jakarta Sans", "Inter", "Segoe UI", "Arial", "sans-serif" }
                },
                H6 = new H6Typography()
                {
                    FontWeight = "600",
                    LetterSpacing = "0.1px"
                },
                Button = new ButtonTypography()
                {
                    TextTransform = "none",    // Enlève l'aspect "tout en majuscules" par défaut
                    FontWeight = "600"
                },
                Body1 = new Body1Typography()
                {
                    LineHeight = "1.6"
                }
            }
        };

        /// <summary>
        /// Thème "Electric Cyber" — violet électrique + cyan néon, look technologique / moderne.
        /// </summary>
        /// <remarks>
        /// Adapté pour des interfaces orientées "tech" ou produits numériques souhaitant un style énergique.
        /// Propose des variations claires et sombres avec des accents néon.
        /// </remarks>
        public static MudTheme AgcThemeElectricCyber = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Brand core (Electric Violet + Cyber Cyan)
                Primary = "#6366F1",          // Indigo/Violet moderne
                PrimaryDarken = "#4F46E5",
                PrimaryLighten = "#818CF8",

                Secondary = "#06B6D4",        // Cyan vibrant
                SecondaryDarken = "#0891B2",
                SecondaryLighten = "#22D3EE",

                // Backgrounds / Surfaces
                Background = "#F1F5F9",       // Gris très clair froid
                Surface = "#FFFFFF",

                // AppBar / Drawer
                AppbarBackground = "#1E1B4B", // Indigo très sombre (presque noir)
                AppbarText = Colors.Shades.White,

                DrawerBackground = "#FFFFFF",
                DrawerText = "#1E1B4B",
                DrawerIcon = "#6366F1",

                // Text
                TextPrimary = "#1E293B",      // Slate 800
                TextSecondary = "#64748B",    // Slate 500

                // Status
                Success = "#10B981",          // Émeraude
                Warning = "#F59E0B",          // Ambre
                Error = "#EF4444",            // Rouge pur
                Info = "#3B82F6",             // Bleu tech

                LinesDefault = "#E2E8F0",
                Divider = "#F1F5F9",
            },

            PaletteDark = new PaletteDark()
            {
                // "Deep Space" feel
                Primary = "#818CF8",          // Indigo clair
                PrimaryDarken = "#6366F1",
                PrimaryLighten = "#A5B4FC",

                Secondary = "#22D3EE",        // Cyan néon
                SecondaryDarken = "#06B6D4",
                SecondaryLighten = "#67E8F9",

                Background = "#020617",       // Le "Midnight" le plus profond (Slate 950)
                Surface = "#0F172A",          // Slate 900 pour les cartes

                AppbarBackground = "#020617",
                AppbarText = "#F8FAFC",

                DrawerBackground = "#020617",
                DrawerText = "#E2E8F0",
                DrawerIcon = "#818CF8",

                TextPrimary = "#F8FAFC",
                TextSecondary = "#94A3B8",

                Success = "#34D399",
                Warning = "#FBBF24",
                Error = "#F87171",
                Info = "#60A5FA",

                LinesDefault = "#1E293B",
                Divider = "#1E293B",
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "280px",
                DefaultBorderRadius = "8px"    // Plus angulaire pour le côté "Software"
            },

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = new[] { "Ubuntu", "Roboto", "Helvetica", "Arial", "sans-serif" }
                },
                H6 = new H6Typography()
                {
                    FontWeight = "700",
                    LetterSpacing = "0.5px",
                    TextTransform = "uppercase" // Titres de sections en majuscules pour le look Pro
                },
                Button = new ButtonTypography()
                {
                    TextTransform = "uppercase",
                    FontWeight = "700",
                    LetterSpacing = "1px"
                }
            }
        };

        /// <summary>
        /// Thème "Glassmorphism" — accents orange laser, surfaces très arrondies, effet "papier" et verre.
        /// </summary>
        /// <remarks>
        /// Idéal pour des interfaces très design/produit avec priorité sur l'esthétique : arrondis prononcés,
        /// polices display et contrastes travaillés. Ajuster les couleurs si nécessaire pour l'accessibilité.
        /// </remarks>
        public static MudTheme AgcThemeGlassmorphism = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // Accent "Laser Copper" — Très innovant, aspect industriel de luxe
                Primary = "#FF4D00",          // Orange pur / Laser
                PrimaryDarken = "#CC3E00",
                PrimaryLighten = "#FF8533",

                Secondary = "#27272A",        // Zinc très sombre pour le contraste
                SecondaryDarken = "#18181B",
                SecondaryLighten = "#3F3F46",

                // Backgrounds (Léger grain "papier")
                Background = "#F4F4F5",
                Surface = "#FFFFFF",

                // AppBar & Drawer (Look minimaliste blanc)
                AppbarBackground = "#FFFFFF",
                AppbarText = "#18181B",

                DrawerBackground = "#18181B", // Drawer inversé (Noir sur fond blanc)
                DrawerText = "#F4F4F5",
                DrawerIcon = "#FF4D00",

                // Text
                TextPrimary = "#09090B",
                TextSecondary = "#71717A",

                // Status (Plus désaturés pour ne pas voler la vedette au Primary)
                Success = "#2DD4BF",          // Teal
                Warning = "#FACC15",          // Jaune vif
                Error = "#FB7185",            // Rose rouge
                Info = "#60A5FA",

                LinesDefault = "#E4E4E7",
                Divider = "#F4F4F5",
            },

            PaletteDark = new PaletteDark()
            {
                // "Obsidian & Ember"
                Primary = "#FF5F15",          // Neon Orange
                PrimaryDarken = "#E64A19",
                PrimaryLighten = "#FF8A50",

                Secondary = "#A1A1AA",
                SecondaryDarken = "#71717A",
                SecondaryLighten = "#D4D4D8",

                Background = "#09090B",       // Noir pur (OLED)
                Surface = "#111113",          // Très proche du noir pour l'effet "unifié"

                AppbarBackground = "#09090B",
                AppbarText = "#F4F4F5",

                DrawerBackground = "#09090B",
                DrawerText = "#F4F4F5",
                DrawerIcon = "#FF5F15",

                TextPrimary = "#FAFAFA",
                TextSecondary = "#A1A1AA",

                Success = "#2DD4BF",
                Warning = "#FACC15",
                Error = "#FB7185",
                Info = "#60A5FA",

                LinesDefault = "#27272A",
                Divider = "#18181B",
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "240px",
                DefaultBorderRadius = "24px",  // Très arrondi pour un look "App Mobile" moderne

            },

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    // Utilisation d'une police "Display" plus impactante
                    FontFamily = new[] { "Montserrat", "Inter", "system-ui", "sans-serif" }
                },
                H6 = new H6Typography()
                {
                    FontWeight = "800",       // Très gras
                    LetterSpacing = "-0.5px", // Resserré pour un look plus "Design"
                },
                Button = new ButtonTypography()
                {
                    TextTransform = "none",
                    FontWeight = "700",
                    LetterSpacing = "0px"
                }
            }
        };


        /// <summary>
        /// Thème officiel AGC — palette corporate (bleu AGC + rouge), calibré pour usage institutionnel.
        /// </summary>
        /// <remarks>
        /// Conçu pour respecter la charte visuelle AGC : couleurs, contrastes et typographie "corporate".
        /// Utilisez ce thème pour les écrans nécessitant un aspect formel/entreprise.
        /// </remarks>
        public static MudTheme AgcThemeOfficial = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                // AGC Blue (Pantone 281 C / 282 C)
                Primary = "#00205B",
                PrimaryDarken = "#00163E",
                PrimaryLighten = "#003399",

                // AGC Red (Pantone 186 C)
                Secondary = "#C8102E",
                SecondaryDarken = "#A50D26",
                SecondaryLighten = "#E31B3D",

                // Backgrounds & Surfaces
                Background = "#F2F4F7",       // Gris neutre très clair (type industriel)
                Surface = "#FFFFFF",

                // Navigation (Le bleu AGC domine ici)
                AppbarBackground = "#00205B",
                AppbarText = "#FFFFFF",
                DrawerBackground = "#FFFFFF",
                DrawerText = "#00205B",
                DrawerIcon = "#C8102E",       // Les icônes en rouge pour le rappel de marque

                // Text
                TextPrimary = "#001540",      // Bleu très sombre pour une meilleure lisibilité que le noir
                TextSecondary = "#4A5568",

                // Status
                Success = "#008751",          // Vert "Sécurité"
                Warning = "#FFB600",          // Or AGC
                Error = "#C8102E",            // Rouge AGC utilisé pour les erreurs
                Info = "#005596",

                LinesDefault = "#D1D5DB",
                Divider = "#E5E7EB",
            },

            PaletteDark = new PaletteDark()
            {
                // Dark Mode calibré pour l'industrie
                Primary = "#3366CC",
                Secondary = "#E31B3D",

                Background = "#0B1120",       // Fond bleu-nuit très profond
                Surface = "#171E2E",          // Cartes légèrement bleutées

                AppbarBackground = "#0B1120",
                AppbarText = "#F8FAFC",

                DrawerBackground = "#0B1120",
                DrawerText = "#E2E8F0",
                DrawerIcon = "#E31B3D",

                TextPrimary = "#F8FAFC",
                TextSecondary = "#94A3B8",

                LinesDefault = "#1E293B",
                Divider = "#1F2937",
            },

            LayoutProperties = new LayoutProperties()
            {
                DrawerWidthLeft = "260px",
                DefaultBorderRadius = "4px",   // Retour à un rayon faible pour un aspect "Ingénierie/Sérieux"

            },

            Typography = new Typography()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = new[] { "Segoe UI", "Roboto", "Helvetica", "Arial", "sans-serif" }
                },
                H6 = new H6Typography()
                {
                    FontWeight = "700",
                    TextTransform = "uppercase",
                    LetterSpacing = "1.5px"    // Style "Corporate" très marqué
                },
                Button = new ButtonTypography()
                {
                    FontWeight = "600",
                    TextTransform = "none",
                    LetterSpacing = "0.5px"
                }
            }
        };
    }
}
