using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var products = db.Phones.ToList();
            ViewBag.ProductCount = products.Count;
            ViewBag.TotalValue = products.Sum(p => (decimal?)p.Price) ?? 0;
            ViewBag.CategoryCount = products
                .Where(p => !string.IsNullOrEmpty(p.Category))
                .Select(p => p.Category)
                .Distinct()
                .Count();

            return View(products);
        }

        public ActionResult ManageProducts(string search, string category)
        {
            var products = db.Phones.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p => p.Name.Contains(search) || p.Category.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                products = products.Where(p => p.Category == category);
            }

            ViewBag.Search = search;
            ViewBag.Category = category;
            ViewBag.Categories = db.Phones
                .Where(p => p.Category != null && p.Category != "")
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p)
                .ToList();

            return View(products.OrderBy(p => p.Category).ThenBy(p => p.Name).ToList());
        }

        public ActionResult Create()
        {
            return View(new Phone());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                TempData["StatusMessage"] = "Product created successfully.";
                return RedirectToAction("ManageProducts");
            }

            return View(phone);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ManageProducts");
            }

            var phone = db.Phones.Find(id.Value);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                TempData["StatusMessage"] = "Product updated successfully.";
                return RedirectToAction("ManageProducts");
            }

            return View(phone);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ManageProducts");
            }

            var phone = db.Phones.Find(id.Value);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone);
        }

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

            TempData["StatusMessage"] = "Product deleted successfully.";
            return RedirectToAction("ManageProducts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
