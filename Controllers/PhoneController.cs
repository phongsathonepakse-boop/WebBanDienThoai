using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    public class PhoneController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        private const string MacBookImageUrl = "https://www.apple.com/v/macbook-air/z/images/overview/hero/hero_static__c9sislzzicq6_large.png";

        // GET: /Phone or /Phone/Index → shows all phones
        // GET: /Phone?search=iphone → filters by name or brand
        // GET: /Phone or /Phone/Index → shows all phones
        // GET: /Phone?search=iphone → filters by name or brand
        public ActionResult Index(string category, string search)
        {
            EnsureCatalogDetails();
            var products = db.Phones.AsQueryable();

            // Trim the string to remove accidental spaces
            if (!string.IsNullOrWhiteSpace(category))
            {
                // Use .Trim() and ensure we are comparing correctly
                string selectedCategory = category.Trim();
                var filteredProducts = products.Where(p => p.Category == selectedCategory).ToList();

                ViewBag.IsHomePage = false;
                return View(filteredProducts);
            }

            // Default Home Page logic...
            ViewBag.IsHomePage = true;
            return View(db.Phones.ToList());
        }
        public ActionResult Search(string query) // 'query' must match the name="query" in the HTML
        {
            EnsureCatalogDetails();
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            query = query.Trim();
            var results = db.Phones.Where(p => p.Name.Contains(query)).ToList();
            ViewBag.IsHomePage = false; // Ensures the slider doesn't show on search results
            ViewBag.SearchQuery = query;
            return View("Index", results);
        }
        public ActionResult Details(int? id)
        {
            EnsureCatalogDetails();

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var phone = db.Phones.Find(id.Value);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone);
        }

        // GET: /Phone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Phone/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: /Phone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var phone = db.Phones.Find(id.Value);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone);
        }

        // POST: /Phone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phone);
        }

        // GET: /Phone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var phone = db.Phones.Find(id.Value);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone);
        }

        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var phone = db.Phones.Find(id);
            if (phone != null)
            {
                db.Phones.Remove(phone);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db?.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        [Authorize] // Only logged-in users can review!
        public ActionResult AddReview(int phoneId, string content, int rating)
        {
            // 1. Create the review object
            var review = new Review
            {
                PhoneId = phoneId,
                Username = User.Identity.Name, // Gets the logged-in user's name automatically
                Content = content,
                Rating = rating,
                DateCreated = DateTime.Now
            };

            // 2. Save to Database (Example using ADO.NET or Entity Framework)
            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = phoneId });
        }

        private void EnsureCatalogDetails()
        {
            var changed = false;
            changed = EnsureMacBooks() || changed;

            foreach (var product in db.Phones.ToList())
            {
                if (NeedsBetterDescription(product.Description))
                {
                    product.Description = BuildDescription(product);
                    changed = true;
                }
            }

            if (changed)
            {
                db.SaveChanges();
            }
        }

        private bool EnsureMacBooks()
        {
            var changed = false;
            changed = AddProductIfMissing("MacBook Air 13-inch M4", 24990000, MacBookImageUrl, "MacBook",
                "MacBook Air 13-inch with M4 chip, Liquid Retina display, silent fanless design, light aluminum body, and up to 18 hours of battery life for study, travel, and daily work.") || changed;
            changed = AddProductIfMissing("MacBook Air 15-inch M4", 30990000, MacBookImageUrl, "MacBook",
                "MacBook Air 15-inch with M4 chip, a larger Liquid Retina display, slim portable design, all-day battery life, and roomier workspace for multitasking.") || changed;
            changed = AddProductIfMissing("MacBook Pro 14-inch M5", 39990000, MacBookImageUrl, "MacBook",
                "MacBook Pro 14-inch with Apple silicon performance, Liquid Retina XDR display, pro ports, long battery life, and strong power for coding, editing, and creative workflows.") || changed;
            changed = AddProductIfMissing("MacBook Pro 16-inch M4 Pro", 62990000, MacBookImageUrl, "MacBook",
                "MacBook Pro 16-inch with M4 Pro performance, expansive Liquid Retina XDR display, advanced cooling, pro connectivity, and desktop-class power in a portable body.") || changed;

            return changed;
        }

        private bool AddProductIfMissing(string name, decimal price, string imageUrl, string category, string description)
        {
            if (db.Phones.Any(p => p.Name == name))
            {
                return false;
            }

            db.Phones.Add(new Phone
            {
                Name = name,
                Price = price,
                ImageUrl = imageUrl,
                Category = category,
                Description = description
            });

            return true;
        }

        private static bool NeedsBetterDescription(string description)
        {
            return string.IsNullOrWhiteSpace(description) || description.Trim().Length < 100;
        }

        private static string BuildDescription(Phone product)
        {
            var name = product.Name ?? "Apple product";
            var category = product.Category ?? "Apple";

            if (category == "iPhone")
            {
                return name + " is a premium iPhone with fast Apple silicon performance, a vivid display, reliable battery life, advanced cameras, and a polished design for everyday communication, photos, gaming, and work.";
            }

            if (category == "iPad")
            {
                return name + " gives you a portable Apple tablet experience with a sharp display, smooth performance, Apple Pencil and keyboard support on compatible models, and enough flexibility for study, drawing, streaming, and productivity.";
            }

            if (category == "MacBook")
            {
                return name + " is built for portable work with Apple silicon performance, a crisp display, comfortable keyboard, long battery life, and a clean macOS experience for study, business, coding, and creative apps.";
            }

            if (category == "AirPod")
            {
                return name + " delivers an easy wireless audio experience with quick Apple pairing, clear calls, comfortable listening, and practical sound features for music, meetings, workouts, and daily travel.";
            }

            if (category == "Accessory")
            {
                return name + " is a useful Apple accessory selected to improve charging, protection, organization, or productivity while keeping your setup clean, reliable, and easy to use every day.";
            }

            return name + " is selected for the store with a clean Apple-style presentation, practical everyday value, dependable performance, and support from AURIONAS before and after checkout.";
        }
    }
}
