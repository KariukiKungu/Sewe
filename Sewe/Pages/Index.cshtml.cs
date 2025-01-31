using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sewe.Data;
using Sewe.Models;

namespace Sewe.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AseweContext context;
        public IndexModel(AseweContext context) =>
            this.context = context;
        public List<Product> Products { get; set; } = new();
        public async Task OnGetAsync() =>
            Products = await context.Products.ToListAsync();
    }
}
