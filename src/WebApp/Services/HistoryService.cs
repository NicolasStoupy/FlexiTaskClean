using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace WebApp.Services
{
    /// <summary>
    /// Service de gestion de l'historique de navigation pour une application Blazor.
    /// Conserve une liste d'URI visitées et permet de naviguer en arrière/avant
    /// sans enregistrer la navigation effectuée par les méthodes <see cref="GoBack"/> et <see cref="GoForward"/>.
    /// </summary>
    public class HistoryService
    {
        /// <summary>
        /// Fournit les services de navigation de Blazor (<see cref="NavigationManager"/>).
        /// </summary>
        private readonly NavigationManager _navManager;

        /// <summary>
        /// Liste séquentielle des URI visitées. L'élément courant est déterminé par <see cref="_currentIndex"/>.
        /// </summary>
        private readonly List<string> _history = new();

        /// <summary>
        /// Index de l'élément courant dans <see cref="_history"/>. -1 signifie non initialisé.
        /// </summary>
        private int _currentIndex = -1;

        /// <summary>
        /// Indicateur utilisé pour différencier la navigation initiée par le service (interne)
        /// de celle initiée par l'utilisateur / le routeur. Permet d'éviter d'ajouter une
        /// nouvelle entrée lors d'un déplacement dans l'historique via <see cref="GoBack"/> / <see cref="GoForward"/>.
        /// </summary>
        private bool _isNavigatingInternally;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="HistoryService"/>.
        /// S'abonne à l'événement <see cref="NavigationManager.LocationChanged"/> et enregistre l'URI initiale.
        /// </summary>
        /// <param name="navManager">Le <see cref="NavigationManager"/> fourni par DI.</param>
        public HistoryService(NavigationManager navManager)
        {
            _navManager = navManager;
            _history.Add(_navManager.Uri);
            _currentIndex = 0;
            _navManager.LocationChanged += OnLocationChanged;
        }

        /// <summary>
        /// Gestionnaire de l'événement <see cref="NavigationManager.LocationChanged"/>.
        /// Enregistre la nouvelle URI dans l'historique sauf si la navigation a été déclenchée
        /// par ce service (indiqué par <see cref="_isNavigatingInternally"/>).
        /// </summary>
        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (_isNavigatingInternally)
            {
                _isNavigatingInternally = false;
                return;
            }

            // Si l'utilisateur navigue normalement, on coupe le "futur" et on ajoute la nouvelle page
            if (_currentIndex < _history.Count - 1)
                _history.RemoveRange(_currentIndex + 1, _history.Count - (_currentIndex + 1));

            _history.Add(e.Location);
            _currentIndex++;
        }

        /// <summary>
        /// Indique s'il est possible de reculer dans l'historique.
        /// </summary>
        public bool CanGoBack => _currentIndex > 0;

        /// <summary>
        /// Indique s'il est possible d'avancer dans l'historique.
        /// </summary>
        public bool CanGoForward => _currentIndex < _history.Count - 1;

        /// <summary>
        /// Navigue vers l'URI précédente dans l'historique.
        /// Définit <see cref="_isNavigatingInternally"/> pour empêcher l'enregistrement de cette navigation.
        /// </summary>
        public void GoBack()
        {
            if (CanGoBack)
            {
                _isNavigatingInternally = true;
                _currentIndex--;
                _navManager.NavigateTo(_history[_currentIndex]);
            }
        }

        /// <summary>
        /// Navigue vers l'URI suivante dans l'historique.
        /// Définit <see cref="_isNavigatingInternally"/> pour empêcher l'enregistrement de cette navigation.
        /// </summary>
        public void GoForward()
        {
            if (CanGoForward)
            {
                _isNavigatingInternally = true;
                _currentIndex++;
                _navManager.NavigateTo(_history[_currentIndex]);
            }
        }

        public void Refresh()
        {
            _navManager.NavigateTo(_navManager.Uri, forceLoad: true);
        }
    }
}
