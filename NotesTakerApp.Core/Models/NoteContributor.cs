namespace NotesTakerApp.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class NoteContributor
{
    public int Id { get; set; }
    
    public int NoteId { get; set; }
    [ForeignKey("NoteId")]
    public Note Note { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    
}