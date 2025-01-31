using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sewe.Data;
using Sewe.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Sewe.Pages
{
    public class OrderModel : PageModel
    {
        private AseweContext context;
        public OrderModel(AseweContext context) =>
            this.context = context;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Product Product { get; set; }

        [BindProperty, Range(1, int.MaxValue, ErrorMessage = "You must order at least one item")]
        public int Quantity { get; set; } = 1;
        [BindProperty, Required, EmailAddress, Display(Name = "Your Email Address")]
        public string OrderEmail { get; set; }
        [BindProperty, Required, Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [BindProperty]
        public decimal UnitPrice { get; set; }
        [TempData]
        public string Confirmation { get; set; }
        public async Task OnGetAsync() =>
            Product = await context.Products.FindAsync(Id);

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Basket basket = new();
                if (Request.Cookies[nameof(Basket)] is not null)
                {
                    basket = JsonSerializer.Deserialize<Basket>(Request.Cookies[nameof(Basket)]);
                }
                basket.Items.Add(new OrderItem
                {
                    ProductId = Id,
                    UnitPrice = UnitPrice,
                    Quantity = Quantity
                });
                var json = JsonSerializer.Serialize(basket);
                Response.Cookies.Append(nameof(Basket), json);

                return RedirectToPage("/Index");
            }
            Product = await context.Products.FindAsync(Id);
            return Page();
        }

    }
}
