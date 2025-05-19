using BlogApp.Models.Data;
using BlogApp.Models.Data.Classes;
using BlogApp.Models.DTO;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;


namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        blogContext _db;
        PostCreateModel _model;

        public PostController(blogContext db, PostCreateModel model)
        {
            _db = db;
            _model = model;
        }

        public IActionResult Add()
        {
            var model = new PostCreateModel
            {
                Post = new Posts(),
                CategoryList = GetCategoryList()
              
            };


            return View(model);
        }
        [HttpPost]
        public IActionResult Add(PostCreateModel model)
        {


            if (string.IsNullOrWhiteSpace(model.Post.Title) || string.IsNullOrWhiteSpace(model.Post.Content))
            {
                ViewBag.Message = "Lütfen boş alan bırakmayın!";
                model.CategoryList = GetCategoryList();
                return View(model);
               
            }


            // Kullanıcı ID'sini al, ancak burada kaydetme işlemi yapılmadı
            int? userId = HttpContext.Session.GetInt32("UserId");


            if (userId == null)
            {
                // Kullanıcı giriş yapmamış, login sayfasına yönlendir
                return RedirectToAction("Login", "User");
            }

            //  Post verisini kaydetme işlemi yapılacak, ancak şu an sadece model döndürülüyor
            var post = new Posts
            {
                Title = model.Post.Title,
                Content = model.Post.Content,
                CategoryId = model.Post.CategoryId,
                UserId = userId.Value,
            };
            _db.Post.Add(post);

            _db.SaveChanges();





            return RedirectToAction("List", "Post");


            // model.CategoryList = GetCategoryList();

            // return View(model);


        }

        public List<CategoryDTO> GetCategoryList()
        {
            return _db.Set<Categories>().Select(x => new CategoryDTO
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList();

        }
        //Her bir commenti dönüştürür liste elde ederiz
        public IActionResult List()
        {
          

            var plist = _db.Set<Posts>().Select(p => new PostDTO
            {
                Title = p.Title,
                Content = p.Content,
                Username = p.User.FullName,
                CategoryName = p.Category.CategoryName,
                PostId = p.PostId,
                UserId=p.UserId,
                Comments = p.Comments.Select(c => new CommentDTO
                {
                    CommentText = c.CommentText,
                    Username = c.User.FullName,
                    UserId = c.UserId,
                    CommentId = c.CommentId


                }).ToList()

            }).ToList();

            var userId = HttpContext.Session.GetInt32("UserId");

            ViewBag.UserId = userId;

            return View(plist);
        }


        public IActionResult DeletePost(int id)
        {
            var post = _db.Set<Posts>().FirstOrDefault(p => p.PostId == id);

            if(post == null)
            {
                return View();

            }

            

            var comments = _db.Set<Comments>().Where(c => c.PostId == id).ToList();

            _db.Set<Posts>().Remove(post);
            _db.SaveChanges();

            return RedirectToAction("List", "Post");
        }

        public IActionResult Update (int id)
        {
            var post = _db.Set<Posts>().FirstOrDefault(p => p.PostId == id);

            if(post == null)
            {
                return View();// return NotFound();
            }

            var model = new PostCreateModel
            {
                Post = post,
                CategoryList = GetCategoryList()


            };

            return View(model);
        }

        [HttpPost]

        public IActionResult Update(PostCreateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Post.Title) || string.IsNullOrWhiteSpace(model.Post.Content))
            {
                ViewBag.Message = "Lütfen boş alan bırakmayın!";
                model.CategoryList = GetCategoryList();
                return View(model);

            }


            var post = _db.Set<Posts>().FirstOrDefault(p => p.PostId == model.Post.PostId);

            if(post == null)
            {
                return View(model);
            }

            post.Title = model.Post.Title;
            post.Content = model.Post.Content;
            post.CategoryId = model.Post.CategoryId;

            _db.Set<Posts>().Update(post);

            _db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
