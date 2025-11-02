using FluentValidation;

namespace Application.Plant.Commands.UpdatePlant
{
    public class UpdatePlantCommandValidator : AbstractValidator<UpdatePlantCommand>
    {

        public UpdatePlantCommandValidator()
        {
            RuleFor(v => v.Code)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
