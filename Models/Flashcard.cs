#pragma warning disable CS8618

namespace Flashcards.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Flashcard
{
    [Key]
    public int FlashcardId { get;set; }

    [Required(ErrorMessage = "Title is required.")]
    [MinLength(2, ErrorMessage = "Title must be at least 2 characters long.")]
    [MaxLength(30, ErrorMessage = "Title cannot exceed 30 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [MinLength(2, ErrorMessage = "Description must be at least 2 characters long.")]
    public string Description { get; set; }

    public bool Blur { get;set; } = true;

    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public int FolderId { get; set; }

    public Folder? Folder { get; set; }
}