namespace NotesTakerApp.Infrastructure.Validators;
using FluentValidation;
using NotesTakerApp.Core.Models;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username mustn't be empty.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number.");
    }
}