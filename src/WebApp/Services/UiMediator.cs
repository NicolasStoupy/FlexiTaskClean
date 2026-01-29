using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebApp.Services
{
    /// <summary>
    /// Résultat d'un appel UI via <see cref="UiMediator.Send{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type des données retournées par la requête.</typeparam>
    /// <param name="Data">Données retournées (ou <c>null</c> en cas d'erreur).</param>
    /// <param name="Handled">
    /// Indique si l'erreur a été gérée par une action UI (par ex. redirection vers la page de connexion).
    /// Si <c>true</c>, le composant appelant n'a normalement pas besoin d'afficher de message additionnel.
    /// </param>
    /// <param name="Message">Message lisible destiné à l'utilisateur (peut être <c>null</c>).</param>
    public record UiCallResult<T>(T? Data, bool Handled, string? Message, bool success=true);

    /// <summary>
    /// Adaptateur entre MediatR et l'interface utilisateur (navigation + notifications).
    /// </summary>
    /// <remarks>
    /// Utilisé par les composants Blazor pour envoyer des requêtes/commandes via MediatR et
    /// centraliser la gestion des erreurs, des notifications (via <see cref="ISnackbar"/>)
    /// et des redirections (via <see cref="NavigationManager"/>).
    /// </remarks>
    public class UiMediator
    {
        private readonly IMediator _mediator;
        private readonly NavigationManager _nav;
        private readonly ISnackbar _snackbar; // optionnel MudBlazor

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="UiMediator"/>.
        /// </summary>
        /// <param name="mediator">Instance MediatR pour envoyer les requêtes.</param>
        /// <param name="nav">NavigationManager pour effectuer les redirections.</param>
        /// <param name="snackbar">Service de notification (MudBlazor). Peut être fourni pour afficher des messages UI.</param>
        public UiMediator(IMediator mediator, NavigationManager nav, ISnackbar snackbar)
        {
            _mediator = mediator;
            _nav = nav;
            _snackbar = snackbar;
        }

        /// <summary>
        /// Envoie une requête/commande via MediatR et gère les retours UI courants (notifications et redirections).
        /// </summary>
        /// <typeparam name="T">Type de résultat attendu par la requête.</typeparam>
        /// <param name="request">La requête ou commande MediatR à envoyer.</param>
        /// <param name="ct">Token d'annulation optionnel.</param>
        /// <returns>
        /// Un <see cref="UiCallResult{T}"/> contenant les données (ou <c>null</c>), un indicateur
        /// <c>Handled</c> signifiant si l'interface a déjà géré l'erreur (ex : redirection) et
        /// un message lisible à afficher à l'utilisateur si nécessaire.
        /// </returns>
        /// <remarks>
        /// Comportements gérés :
        /// - Si la requête est un <see cref="ICommand"/> et que l'appel réussit, affiche une notification de succès.
        /// - <see cref="UnauthorizedAccessException"/> : redirige vers la page de connexion et retourne Handled = true.
        /// - <see cref="ForbiddenAccessException"/> : redirige vers /forbidden et retourne Handled = true.
        /// - <see cref="FluentValidation.ValidationException"/> : affiche un avertissement avec les messages de validation.
        /// - <see cref="NotFoundException"/> : affiche une erreur indiquant l'élément introuvable.
        /// - Autres exceptions : affiche une erreur générique.
        /// </remarks>
        public async Task<UiCallResult<T>> Send<T>(IRequest<T> request, CancellationToken ct = default)
        {
            try
            {
                var data = await _mediator.Send(request, ct);
                var result = new UiCallResult<T>(data, Handled: false, Message: null);
                if (request is ICommand cmd && !result.Handled)
                {
                    _snackbar.Add("Opération exécutée avec succès", Severity.Success);
                }

                return result;
            }
            catch (UnauthorizedAccessException)
            {
                _nav.NavigateTo("/Account/Login", forceLoad: true);
                return new UiCallResult<T>(default, Handled: true, "Veuillez vous connecter.",false);
            }
            catch (ForbiddenAccessException ex)
            {
                _nav.NavigateTo("/forbidden");
                return new UiCallResult<T>(default, Handled: true, ex.Message,false);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var msg = "Validation: " + string.Join(" | ", ex.Errors.Select(e => e.ErrorMessage));
                _snackbar.Add(msg, Severity.Warning);
                return new UiCallResult<T>(default, Handled: false, msg,false);
            }
            catch (NotFoundException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new UiCallResult<T>(default, Handled: false, ex.Message, false);
            }
            catch (DomainException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return new UiCallResult<T>(default, Handled: false, ex.Message, false);
            }
            catch (Exception ex)
            {
                _snackbar.Add("Une erreur inattendue est survenue.", Severity.Error);
                return new UiCallResult<T>(default, Handled: false, ex.Message, false);
            }
        }
    }
}
