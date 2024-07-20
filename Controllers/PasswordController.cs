using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    public class PasswordController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasswordController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var passwords = await _context.PasswordEntries.ToListAsync();
            return View(passwords);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,URL,User,Password,ConfirmPassword")] PasswordEntry entry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entry.URL = EnsureUrlProtocol(entry.URL);

                    _context.Add(entry);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(entry);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.PasswordEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,URL,User,Password,ConfirmPassword")] PasswordEntry entry)
        {
            if (id != entry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    entry.URL = EnsureUrlProtocol(entry.URL);

                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasswordEntryExists(entry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _context.PasswordEntries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.PasswordEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasswordEntryExists(int id)
        {
            return _context.PasswordEntries.Any(e => e.Id == id);
        }

        private string EnsureUrlProtocol(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                return "https://" + url;
            }
            return url;
        }
    }
}
