namespace NotesTakerApp.Infrastructure.Validators;
using FluentValidation;
using NotesTakerApp.Core.Models;

public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        RuleFor(note => note.Title)
            .NotEmpty().WithMessage("Title mustn't be empty.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters long.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(note => note.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(5000).WithMessage("Content must not exceed 5000 characters.");
    }
}