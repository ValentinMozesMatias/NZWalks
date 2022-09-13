using FluentValidation;

namespace NZwalks.API.Validators
{
    public class AddWalkDifficultyValidator: AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            //RuleFor(x => x.Code).NotEqual();
        }
    }
}
