﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly Socaciu_Alexia_Lab2.Data.Socaciu_Alexia_Lab2Context _context;

        public IndexModel(Socaciu_Alexia_Lab2.Data.Socaciu_Alexia_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(b=>b.Publisher)
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}
