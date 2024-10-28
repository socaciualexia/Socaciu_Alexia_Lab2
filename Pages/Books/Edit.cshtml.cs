using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Socaciu_Alexia_Lab2.Data;
using Socaciu_Alexia_Lab2.Models;

namespace Socaciu_Alexia_Lab2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Socaciu_Alexia_Lab2Context _context;

        public EditModel(Socaciu_Alexia_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }

            Book = book;

            PopulateAssignedCategoryData(_context, Book);

            // Populează dropdown-ul pentru autori
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");

            // Populează dropdown-ul pentru publisher
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.AuthorID,
                i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateAssignedCategoryData(_context, bookToUpdate);
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName", bookToUpdate.AuthorID);
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", bookToUpdate.PublisherID);
            return Page();
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}
