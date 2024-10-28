using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Socaciu_Alexia_Lab2.Data;
using Socaciu_Alexia_Lab2.Models;

namespace Socaciu_Alexia_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Socaciu_Alexia_Lab2Context _context;

        public DetailsModel(Socaciu_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Include BookCategories and the related Category entities, and also include the Author
            Book = await _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
