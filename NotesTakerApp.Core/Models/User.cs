namespace NotesTakerApp.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
      
    public ICollection<Note> SharedNotes { get; set; } = new List<Note>();
    
    [InverseProperty("Owner")] 
    public ICollection<Note> OwnedNotes { get; set; } = new List<Note>();

    public ICollection<NoteContributor> ContributedNotes { get; set; } = new List<NoteContributor>();
}