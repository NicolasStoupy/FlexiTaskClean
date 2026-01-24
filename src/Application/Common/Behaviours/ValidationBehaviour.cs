using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        /// <summary>
        /// Initialise une nouvelle instance de <see cref="ValidationBehaviour{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="validators">
        /// Une collection d'objets <see cref="IValidator{TRequest}"/> fournie par l'injection de dépendances.
        /// Peut être vide si aucun validateur n'est enregistré pour le type de requête.
        /// Ne doit pas être <c>null</c> (la méthode <see cref="Handle"/> suppose une collection non nulle).
        /// </param>
        /// <remarks>
        /// Ce comportement MediatR exécute les validateurs FluentValidation pour la requête avant
        /// d'appeler le gestionnaire suivant dans le pipeline. Si des erreurs de validation sont détectées,
        /// une <see cref="FluentValidation.ValidationException"/> sera levée lors de l'exécution du pipeline.
        /// </remarks>
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next();

        }
    }
}
