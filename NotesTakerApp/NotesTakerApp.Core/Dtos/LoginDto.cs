using System.ComponentModel.DataAnnotations;

namespace NotesTakerApp.Core.Dtos;

public class LoginDto
{
    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public required string Password { get; set; } = string.Empty;
}