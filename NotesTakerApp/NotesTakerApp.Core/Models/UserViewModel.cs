using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Models
{
    public class UserViewModel
    {
        public User NewUser { get; set; } = new();
        public IEnumerable<User> AllUsers { get; set; } = new List<User>();
    }
}