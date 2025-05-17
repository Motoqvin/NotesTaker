using System.ComponentModel.DataAnnotations;

namespace Back.Dtos;
public class RegisterDto
{
    public required string UserName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
}