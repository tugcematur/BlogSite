using BlogApp.Models.Data;
using BlogApp.Models.Data.Classes;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class CommentController : Controller
    {
        blogContext _db;
    

        public CommentController(blogContext db)
        {
            _db = db;
       
        }
        public IActionResult Add(int postId)
        {
            var model = new CommentModel
            {
                PostId = postId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CommentModel model)
        {

            if (string.IsNullOrWhiteSpace(model.CommentText))
            {
                ViewBag.Message = "Boş alan bırakmayın!";
               return  View();
            }
            int? userId = HttpContext.Session.GetInt32("UserId");

            var comment = new Comments
            {
                PostId = model.PostId,
                UserId = userId.Value,
                CommentText = model.CommentText
            };

            _db.Comment.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("List","Post");
        }




        [HttpPost]

        public IActionResult DeleteComment(int id)
        {
            var comment = _db.Set<Comments>().FirstOrDefault(c => c.CommentId == id);

            if (comment == null)
            {
                return View();
            }



            _db.Set<Comments>().Remove(comment);
            _db.SaveChanges();

            return RedirectToAction("List", "Post");
        }


        

        public IActionResult Update(int id)
        {
            var comment = _db.Set<Comments>().FirstOrDefault(c => c.CommentId == id);

            if(comment == null)
            {
                return View();
            }

        

            var model = new CommentModel
            {
                PostId = comment.PostId,
                CommentId = comment.CommentId,
                CommentText = comment.CommentText
            };

            return View(model);
        }

        [HttpPost]

        public IActionResult Update(CommentModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CommentText))
            {
                ViewBag.Message = "Boş alan bırakmayın!";
                return View();
            }

            var comment = _db.Set<Comments>().FirstOrDefault(c => c.CommentId == model.CommentId);

            int? userId = HttpContext.Session.GetInt32("UserId");

            if(comment == null)
            {
                return View(model);
            }

            comment.PostId = model.PostId;
            comment.UserId = userId.Value;
            comment.CommentText = model.CommentText;
           

            _db.Set<Comments>().Update(comment);
            _db.SaveChanges();

            return RedirectToAction("List", "Post");
        }
    }
}
