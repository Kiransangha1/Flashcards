using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flashcards.Models;
using System.Security.Claims;

namespace Flashcards.Controllers
{
    [SessionCheck]
    public class FolderController : Controller
    {
        private readonly MyContext _context;
        private readonly ILogger<FolderController> _logger;

        public FolderController(ILogger<FolderController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");

            List<Folder> allFolders = _context.Folders
                                    .Where(f => f.UserId == userId)
                                    .OrderByDescending(p => p.CreatedAt)
                                    .ToList();
            return View(allFolders);
        }

        [HttpGet("folders/new")]
        public ViewResult NewFolder() => View();

        [HttpPost("folders/create")]
        public IActionResult CreateFolder(Folder newFolder)
        {
            if (!ModelState.IsValid)
            {
                return View("NewFolder", newFolder);
            }

            newFolder.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newFolder);
            _context.SaveChanges();

            return RedirectToAction("AllFlashcards", "Flashcards", new { FolderId = newFolder.FolderId });
        }

        [HttpPost("folders/{FolderId}/delete")]
        public IActionResult DeleteFolder(int FolderId)
        {
            Folder? ToBeDeleted = _context.Folders.FirstOrDefault(u => u.FolderId == FolderId);
            if (ToBeDeleted != null)
            {
                _context.Remove(ToBeDeleted);
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet("BreakTime")]
        public ViewResult TakeABreak() => View();
    }
}
