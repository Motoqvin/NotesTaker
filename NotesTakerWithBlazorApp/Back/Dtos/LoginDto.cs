using System.ComponentModel.DataAnnotations;

namespace Back.Dtos;

public class LoginDto
{
    [EmailAddress]
    public required string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public required string Password { get; set; } = string.Empty;
}