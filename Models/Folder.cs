#pragma warning disable CS8618

namespace Flashcards.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Folder
{
    [Key]
    public int FolderId { get;set; }

    [Required]
    [MinLength(2)]
    [MaxLength(30)]
    public string FolderName { get;set; }

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    
    public int UserId { get; set; }

    public User? User { get; set; }

    public List<Flashcard> Flashcards { get; set; } = new();
}