namespace Back.Models;
using Microsoft.AspNetCore.Identity;
public class User : IdentityUser
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles{ get; set; }
}