using FluentValidation;

namespace DogsHouse.Application.Dogs.Commands.UpdateDog
{
    public class UpdateDogCommandValidator : AbstractValidator<UpdateDogCommand>
    {
        public UpdateDogCommandValidator() 
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty");

            RuleFor(d => d.Color)
                .NotEmpty()
                .WithMessage("Color can't be empty");

            RuleFor(d => d.Weight)
                .NotEmpty()
                .WithMessage("Weight can't be empty")
                .Must(w => w > 0)
                .WithMessage($"Weight can't less/equal 0");

            RuleFor(d => d.TailLength)
                .NotEmpty()
                .WithMessage("Tail length can't be empty")
                .Must(tl => tl > 0)
                .WithMessage($"Tail can't be less/equal 0");
        }
    }
}
