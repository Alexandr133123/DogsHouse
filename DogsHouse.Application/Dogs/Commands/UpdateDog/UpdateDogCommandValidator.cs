using FluentValidation;

namespace DogsHouse.Application.Dogs.Commands.UpdateDog
{
    public class UpdateDogCommandValidator : AbstractValidator<UpdateDogCommand>
    {
        public UpdateDogCommandValidator() 
        {
            RuleFor(d => d.Id).NotEmpty();
            RuleFor(d => d.Name).NotEmpty();
            RuleFor(d => d.Color).NotEmpty();
            RuleFor(d => d.Weight).NotEmpty().Must(w => w > 0);
            RuleFor(d => d.TailLength).NotEmpty().Must(tl => tl > 0);
        }
    }
}
