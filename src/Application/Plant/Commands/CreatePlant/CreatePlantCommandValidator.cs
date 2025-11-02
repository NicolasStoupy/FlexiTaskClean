using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Plant.Commands.CreatePlant
{
    public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlantCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Code)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Le code est requis.")
                .Must(code => code?.Trim().Length > 0)
                    .WithMessage("Le code ne peut pas être vide ou des espaces.")
                .MaximumLength(4).WithMessage("Le code doit contenir au maximum 4 caractères.")
                .Matches("^[A-Z0-9]+$").WithMessage("Le code doit contenir uniquement des lettres majuscules et des chiffres (A–Z, 0–9).")
                // Vérifie l'unicité en base (sur le code normalisé en MAJUSCULES)
                .MustAsync(BeUniqueCode)
                    .WithMessage("Ce code existe déjà.")
                    .WithErrorCode("Unique");
        }

        private async Task<bool> BeUniqueCode(string code, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;

            var normalized = code.Trim().ToUpperInvariant();

            return !await _context.Plant
                .AnyAsync(p => p.Code == normalized, ct);
        }
    }
}
