namespace WebApp.Services
{
    /// <summary>
    /// Service léger responsable de l'état du thème de l'application.
    /// </summary>
    /// <remarks>
    /// Fournit un point central pour consulter et basculer le mode sombre,
    /// et pour notifier les composants abonnés lorsque l'état du thème change.
    /// Conçu pour être utilisé comme service singleton dans une application Blazor.
    /// </remarks>
    public class ThemeService
    {
        /// <summary>
        /// Indique si l'application est en mode sombre.
        /// </summary>
        /// <value>
        /// <c>true</c> si le mode sombre est actif ; sinon <c>false</c>.
        /// La valeur par défaut est <c>false</c>.
        /// </value>
        public bool IsDarkMode { get; set; } = false;

        /// <summary>
        /// Événement invoqué lorsqu'il y a une modification de l'état du thème.
        /// Les abonnés peuvent s'enregistrer pour rafraîchir l'interface ou appliquer des classes CSS.
        /// </summary>
        public event Action OnChange;

        /// <summary>
        /// Bascule l'état du mode sombre et notifie les abonnés.
        /// </summary>
        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            NotifyStateChanged();
        }

        /// <summary>
        /// Notifie de façon encapsulée les abonnés que l'état a changé.
        /// Utilise une invocation sûre (null-conditional) pour éviter les exceptions si aucun abonné.
        /// </summary>
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
