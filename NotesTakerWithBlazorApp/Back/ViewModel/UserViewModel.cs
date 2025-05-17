namespace Back.ViewModel
{
    public class UserViewModel
    {
    public required string UserName { get; set; }
    public string Email { get; set; }
    public required string Password { get; set; }
    public string Roles { get; set; }
    }
}