using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();

        private List<CartItem> GetCart()
        {
            return Session["Cart"] as List<CartItem> ?? new List<CartItem>();
        }

        // GET: /Checkout
        public ActionResult Index()
        {
            var cart = GetCart();

            ViewBag.TotalAmount = cart.Sum(x => x.Phone.Price * x.Quantity);

            return View(cart);
        }

        // POST: /Checkout
        [HttpPost]
        public ActionResult Index(string name, string phone, string address, string paymentMethod, string note)
        {
            var cart = GetCart();

            if (cart.Count == 0)
            {
                TempData["Info"] = "Your bag is empty. Add a product before checkout.";
                return RedirectToAction("Index", "Cart");
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                TempData["Error"] = "Please enter your name, phone number, and shipping address.";
                return RedirectToAction("Index");
            }

            if (paymentMethod != "Cash" && paymentMethod != "Bank")
            {
                TempData["Error"] = "Please choose cash or bank payment.";
                return RedirectToAction("Index");
            }

            // Save order
            Order order = new Order
            {
                CustomerName = name,
                Address = address + " | Phone: " + phone + (string.IsNullOrWhiteSpace(note) ? "" : " | Note: " + note),
                OrderDate = DateTime.Now
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            foreach (var item in cart)
            {
                _db.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    PhoneId = item.Phone.Id,
                    Quantity = item.Quantity,
                    Price = item.Phone.Price
                });
            }

            _db.SaveChanges();

            Session["Cart"] = null;

            TempData["OrderId"] = order.Id;
            TempData["PaymentMethod"] = paymentMethod == "Bank" ? "Bank payment" : "Cash on delivery";
            TempData["TotalAmount"] = cart.Sum(x => x.Phone.Price * x.Quantity).ToString("N0");

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
