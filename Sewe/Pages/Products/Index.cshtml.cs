using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sewe.Data;
using Sewe.Models;

namespace Sewe.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Sewe.Data.AseweContext _context;

        public IndexModel(Sewe.Data.AseweContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
