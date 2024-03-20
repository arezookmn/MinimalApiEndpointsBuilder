using FluentValidation;
using MinimalApiApplication.Contracts.TodoContracts;

namespace MinimalApiApplication.Validators.TodoValidators;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(p => p.Name)
        .NotEmpty().WithMessage("Name can't be empty or null.")
        .MaximumLength(300).WithMessage("Name length should be less than 300 character");
    }
}
