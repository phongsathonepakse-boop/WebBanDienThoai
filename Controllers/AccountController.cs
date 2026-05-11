using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using WebBanDienThoai.Models;

namespace WebBanDienThoai.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Login(string returnUrl)
        {
            EnsureDefaultAdmin();
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            EnsureDefaultAdmin();

            if (ModelState.IsValid)
            {
                var user = FindUser(model.Username);
                if (user != null && VerifyPassword(model.Password, user))
                {
                    var role = string.IsNullOrWhiteSpace(user.Role) ? "User" : user.Role;
                    SignIn(user.Username, role);

                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction(role == "Admin" ? "Index" : "Index", role == "Admin" ? "Admin" : "Phone");
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            EnsureDefaultAdmin();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var username = (model.Username ?? "").Trim();
            if (FindUser(username) != null)
            {
                ModelState.AddModelError("", "That username is already registered.");
                return View(model);
            }

            var salt = NewSalt();
            db.Users.Add(new UserAccount
            {
                FullName = (model.FullName ?? "").Trim(),
                Username = username,
                PhoneNumber = (model.PhoneNumber ?? "").Trim(),
                Role = "User",
                Password = null,
                PasswordSalt = salt,
                PasswordHash = HashPassword(model.Password, salt)
            });
            db.SaveChanges();

            SignIn(username, "User");
            return RedirectToAction("Index", "Phone");
        }

        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            EnsureDefaultAdmin();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var normalizedPhone = NormalizePhone(model.PhoneNumber);
            var user = db.Users.ToList().FirstOrDefault(u =>
                string.Equals(u.Username, model.Username, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(NormalizePhone(u.PhoneNumber), normalizedPhone, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                ModelState.AddModelError("", "We could not match that username and phone number.");
                return View(model);
            }

            user.Password = null;
            user.PasswordSalt = NewSalt();
            user.PasswordHash = HashPassword(model.NewPassword, user.PasswordSalt);
            db.SaveChanges();

            TempData["AccountMessage"] = "Password updated. You can sign in now.";
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Phone");
        }

        private void EnsureDefaultAdmin()
        {
            if (db.Users.Any(u => u.Role == "Admin"))
            {
                return;
            }

            var existingAdmin = FindUser("admin");
            var salt = NewSalt();
            if (existingAdmin != null)
            {
                existingAdmin.Role = "Admin";
                existingAdmin.Password = null;
                existingAdmin.PasswordSalt = salt;
                existingAdmin.PasswordHash = HashPassword("123", salt);
            }
            else
            {
                db.Users.Add(new UserAccount
                {
                    FullName = "Store Admin",
                    Username = "admin",
                    PhoneNumber = "0793247090",
                    Role = "Admin",
                    PasswordSalt = salt,
                    PasswordHash = HashPassword("123", salt)
                });
            }

            db.SaveChanges();
        }

        private static void SignIn(string username, string role)
        {
            var ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddDays(7), false, role);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private UserAccount FindUser(string username)
        {
            return db.Users.FirstOrDefault(u => u.Username == username);
        }

        private bool VerifyPassword(string password, UserAccount user)
        {
            if (!string.IsNullOrWhiteSpace(user.PasswordHash) && !string.IsNullOrWhiteSpace(user.PasswordSalt))
            {
                return string.Equals(HashPassword(password, user.PasswordSalt), user.PasswordHash, StringComparison.Ordinal);
            }

            if (!string.IsNullOrEmpty(user.Password) && string.Equals(user.Password, password, StringComparison.Ordinal))
            {
                user.Password = null;
                user.PasswordSalt = NewSalt();
                user.PasswordHash = HashPassword(password, user.PasswordSalt);
                if (string.IsNullOrWhiteSpace(user.Role))
                {
                    user.Role = "User";
                }
                db.SaveChanges();
                return true;
            }

            return false;
        }

        private static string NormalizePhone(string phone)
        {
            return new string((phone ?? "").Where(char.IsDigit).ToArray());
        }

        private static string NewSalt()
        {
            var bytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return Convert.ToBase64String(bytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes((password ?? "") + ":" + salt);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
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
