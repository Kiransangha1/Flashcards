using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flashcards.Models;

namespace Flashcards.Controllers;

    [SessionCheck]
    public class FlashcardsController : Controller
    {

        private MyContext _context;
        private readonly ILogger<FlashcardsController> _logger;

        public FlashcardsController(ILogger<FlashcardsController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("flashcards/{FolderId}")]
        public IActionResult AllFlashcards(string sortOrder, int FolderId)
        {
            var userFlashcards = _context.Flashcards
                .Where(f => f.FolderId == FolderId)
                .AsQueryable();

            switch (sortOrder)
            {
                case "a-z":
                    userFlashcards = userFlashcards.OrderBy(f => f.Title);
                    break;
                case "z-a":
                    userFlashcards = userFlashcards.OrderByDescending(f => f.Title);
                    break;
                case "newest":
                    userFlashcards = userFlashcards.OrderByDescending(f => f.CreatedAt);
                    break;
                case "oldest":
                    userFlashcards = userFlashcards.OrderBy(f => f.CreatedAt);
                    break;
                default:
                    userFlashcards = userFlashcards.OrderByDescending(f => f.CreatedAt);
                    break;
            }
            ViewBag.FolderId = FolderId;

            return View(userFlashcards.ToList());
        }

        [HttpGet("flashcards/{FolderId}/new")]
        public ViewResult NewFlashcard(int folderId)
        {
            Console.WriteLine($"Incoming FolderId: {folderId}");
            var flashcard = new Flashcard
            {
                FolderId = folderId 
            };
            return View(flashcard);
        }

        [HttpPost("flashcards/create")]
        public IActionResult CreateFlashcard(Flashcard newFlashcard)
        {
            Console.WriteLine($"Received FolderId: {newFlashcard.FolderId}");
            if (!ModelState.IsValid)
            {
                return View("NewFlashcard", newFlashcard);
            }

            if (newFlashcard.FolderId == 0)
            {
                ModelState.AddModelError("FolderId", "A valid Folder ID must be provided.");
                return View("NewFlashcard", newFlashcard);
            }

            _context.Flashcards.Add(newFlashcard);
            _context.SaveChanges();

            return RedirectToAction("AllFlashcards", new { FolderId = newFlashcard.FolderId });
        }

    [HttpPost("flashcards/{FlashcardId}/delete")]
    public IActionResult DeleteFlashcard(int FlashcardId)
    {
        Flashcard? ToBeDeleted = _context.Flashcards.FirstOrDefault(u => u.FlashcardId == FlashcardId);
        if (ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("AllFlashcards", ToBeDeleted);
    }
    [HttpPost]
    public IActionResult UpdateBlur(int flashcardId, bool blur)
    {
        var flashcard = _context.Flashcards.Find(flashcardId);
        if (flashcard != null)
        {
            flashcard.Blur = blur;

            _context.SaveChanges();
        }

        return RedirectToAction("AllFlashcards");
    }
    [HttpGet("flashcard/{FlashcardId}/edit")]
    public IActionResult EditFlashcard(int FlashcardId)
    {
        Flashcard? Editing = _context.Flashcards.FirstOrDefault(e => e.FlashcardId == FlashcardId);
        if (Editing == null)
        {
            return RedirectToAction("AllFlashcards");
        }
        return View("EditFlashcard", Editing);
    }

    [HttpPost("flashcard/{FlashcardId}/update")]
    public IActionResult UpdateFlashcard(int FlashcardId, Flashcard editedFlashcard)
    {
        Flashcard? OldFlashcard = _context.Flashcards.FirstOrDefault(p => p.FlashcardId == FlashcardId);
        if (!ModelState.IsValid || OldFlashcard == null)
        {
            if (OldFlashcard == null)
            {
                ModelState.AddModelError(string.Empty, "Flashcard not found, what did you do?!?");
            }
            return View("EditFlashcard", editedFlashcard);
        }
        OldFlashcard.Title = editedFlashcard.Title;
        OldFlashcard.Description = editedFlashcard.Description;
        OldFlashcard.UpdatedAt = DateTime.Now;
        _context.SaveChanges();
        return RedirectToAction("AllFlashcards", new { FolderId = OldFlashcard.FolderId });
    }

    [HttpGet]
    [Route("Flashcards/TakeABreak")]
    public ViewResult TakeABreak() => View();
}
