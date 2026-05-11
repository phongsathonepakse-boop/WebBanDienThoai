using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        // Helper: Get cart from session (never null)
        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Helper: Save cart back to session (optional - can be called explicitly)
        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        // GET: /Cart/Add/5
        [HttpGet]
        public ActionResult Add(int? id)
        {
            if (id == null || id <= 0)
            {
                TempData["Error"] = "Invalid product.";
                return RedirectToAction("Index", "Phone");
            }

            var phone = _db.Phones.Find(id.Value);
            if (phone == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction("Index", "Phone");
            }

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(x => x.Phone?.Id == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    Phone = phone,
                    Quantity = 1
                });
            }

            SaveCart(cart); // optional - since reference is same object

            TempData["Success"] = $"{phone.Name} was added to your bag.";
            return RedirectToAction("Index");
        }

        // GET: /Cart
        // GET: /Cart
        public ActionResult Index()
        {
            var cart = GetCart();
            ViewBag.Subtotal = cart.Sum(i => i.Phone.Price * i.Quantity);
            ViewBag.TotalQuantity = cart.Sum(i => i.Quantity);

            return View(cart);
        }
        // GET: /Cart/Checkout
        public ActionResult Checkout()
        {
            var cart = GetCart();

            if (cart.Count == 0)
            {
                TempData["Info"] = "Your bag is empty. Add some products first!";
                return RedirectToAction("Index", "Home");
            }

            // Pass the total to the checkout page as well
            ViewBag.TotalAmount = cart.Sum(x => x.Quantity * (x.Phone?.Price ?? 0)).ToString("N0");

            return View(cart);
        }

        // GET: /Cart/Remove/5
        public ActionResult Remove(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Phone?.Id == id);

            if (item != null)
            {
                cart.Remove(item);
                TempData["Info"] = "Product removed from your bag.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Decrease(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Phone?.Id == id);

            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetQuantity(int phoneId, int qty)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Phone?.Id == phoneId);

            if (item == null)
            {
                TempData["Error"] = "Product not found in your bag.";
                return RedirectToAction("Index");
            }

            if (qty <= 0)
            {
                cart.Remove(item);
                TempData["Info"] = "Product removed from your bag.";
            }
            else
            {
                item.Quantity = Math.Min(qty, 50);
                TempData["Success"] = "Bag updated.";
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/UpdateQuantity?phoneId=5&qty=3
        public ActionResult UpdateQuantity(int? phoneId, int qty)
        {
            if (phoneId == null || phoneId <= 0 || qty < 1)
            {
                return Json(new { success = false, message = "Invalid request" });
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Phone?.Id == phoneId);

            if (item != null)
            {
                // Optional: max quantity limit
                if (qty > 50)
                {
                    return Json(new { success = false, message = "Maximum quantity is 50" }, JsonRequestBehavior.AllowGet);
                }

                item.Quantity = qty;

                return Json(new
                {
                    success = true,
                    quantity = item.Quantity,
                    total = cart.Sum(x => x.Phone.Price * x.Quantity)
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "Product is not in your bag" }, JsonRequestBehavior.AllowGet);
        }

        // Optional: Clear entire cart
        public ActionResult Clear()
        {
            Session["Cart"] = null;
            TempData["Success"] = "Your bag has been cleared.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
