using Microsoft.AspNetCore.Mvc;
using ComiaMidtermStore.Data;
using ComiaMidtermStore.Models;

namespace ComiaMidtermStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var items = _db.CartItems.ToList();
            return View(items);
        }

        public IActionResult Add(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index", "Products");

            var item = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };
            _db.CartItems.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);
            if (item != null)
            {
                item.Quantity = quantity;
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var product = _db.CartItems.Find(id);
            if (product != null)
            {
                _db.CartItems.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}