using Microsoft.AspNetCore.Mvc;
using NoteBookProject.Models;
using System.Linq;

namespace NoteBookProject.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var notes = _context.Notes.ToList();
            ViewBag.Notes = notes;
            return View(notes);
        }

        public IActionResult Details(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            ViewBag.Notes = _context.Notes.ToList();
            return View(note);
        }

        public IActionResult Create()
        {
            ViewBag.Notes = _context.Notes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Notes = _context.Notes.ToList();
            return View(note);
        }

        public IActionResult Edit(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            ViewBag.Notes = _context.Notes.ToList();
            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Notes.Update(note);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Notes = _context.Notes.ToList();
            return View(note);
        }

        public IActionResult Delete(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            ViewBag.Notes = _context.Notes.ToList();
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            _context.Notes.Remove(note);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
