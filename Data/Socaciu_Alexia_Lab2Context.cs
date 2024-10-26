using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Socaciu_Alexia_Lab2.Models;

namespace Socaciu_Alexia_Lab2.Data
{
    public class Socaciu_Alexia_Lab2Context : DbContext
    {
        public Socaciu_Alexia_Lab2Context (DbContextOptions<Socaciu_Alexia_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Socaciu_Alexia_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Socaciu_Alexia_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    }
}
