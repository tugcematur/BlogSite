using BlogApp.Models.Data;
using BlogApp.Models.Data.Classes;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BlogApp.Controllers
{
    public class AccountController : Controller
    {
        blogContext _db;
        UserModel _model;
        public AccountController(blogContext db, UserModel model)
        {
            _db = db;
            _model = model;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(UserModel model)
        {
            var existingUser = _db.Set<Users>().FirstOrDefault(u => u.Email == model.Users.Email);


            if (string.IsNullOrWhiteSpace(model.Users.Email) || string.IsNullOrWhiteSpace(model.Users.FullName)
                || string.IsNullOrWhiteSpace(model.Users.Name) || string.IsNullOrWhiteSpace(model.Users.PasswordHash))
            {
                ViewBag.Message = "Lütfen tüm alanları doldurunuz!";
                return View();
            }

            if (existingUser != null)
            {
                ViewBag.Message = "Bu email adresi zaten kayıtlı";
                return View();
               
            }



            _db.Set<Users>().Add(model.Users);
            _db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Login(string email, string password)
        {
            var user = _db.Set<Users>().FirstOrDefault(u => u.Email == email && u.PasswordHash == password);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Message = "Lütfen tüm alanları doldurun!";
                return View();
            }

            if (user != null)
            {
                // Session'a kullanıcı bilgilerini kaydet
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Email", user.Email);



                return RedirectToAction("List", "Post");
            }

            else
            {
                ViewBag.Message = "Geçersiz kullanıcı adı veya şifre!";
                return View();
            }
        }

        public IActionResult Logout()
        {
            // Tüm session verilerini temizle
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
