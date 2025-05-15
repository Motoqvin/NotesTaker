using System.ComponentModel.DataAnnotations;

namespace NotesTakerApp.Core.Dtos;
public class RegisterDto
{
    public required string UserName { get; set; } = string.Empty;

    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public required string Password { get; set; } = string.Empty;
}