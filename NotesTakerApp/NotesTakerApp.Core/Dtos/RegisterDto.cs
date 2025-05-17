using System.ComponentModel.DataAnnotations;

namespace NotesTakerApp.Core.Dtos;
public class RegisterDto
{
    public required string UserName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
}