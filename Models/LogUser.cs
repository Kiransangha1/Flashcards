#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Flashcards.Models;
public class LogUser
{        
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string LogEmail { get; set; }        
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [DisplayName("Password")]
    public string LogPassword { get; set; }
}