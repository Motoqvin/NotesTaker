namespace NotesTakerApp.Core.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public List<string> Roles { get; set; } = new List<string> { "User" };

    public List<Note> SharedNotes { get; set; } = new List<Note>();
    [NotMapped]
    public ICollection<Note> OwnedNotes { get; set; } = new List<Note>();
    public ICollection<NoteContributor> ContributedNotes { get; set; } = new List<NoteContributor>();
}