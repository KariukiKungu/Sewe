using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sewe.Data;
using Sewe.Models;

namespace Sewe.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Sewe.Data.AseweContext _context;
        private readonly IWebHostEnvironment environment;

        public CreateModel(Sewe.Data.AseweContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty, Display(Name = "Product Image")]
        public IFormFile ProductImage { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Product.ImageName");
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }

            Product.ImageName = ProductImage.FileName;
            var imageFile = Path.Combine(environment.WebRootPath, "images", "products", ProductImage.FileName);
            using var fileStream = new FileStream(imageFile, FileMode.Create);
            await ProductImage.CopyToAsync(fileStream);
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
