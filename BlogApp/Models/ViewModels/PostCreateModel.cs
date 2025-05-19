using BlogApp.Models.Data.Classes;
using BlogApp.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.Models.ViewModels
{
    public class PostCreateModel
    {
        public Posts Post { get; set; }

        public List<CategoryDTO> CategoryList { get; set; }
        public int PostId { get; set; }

    }
}
