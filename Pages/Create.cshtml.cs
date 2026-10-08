using MicroBlog.Models;
using MicroBlog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MicroBlog.Pages
{
    public class CreateModel : PageModel
    {

        private readonly IBlogRepository _store;
        public CreateModel(IBlogRepository store) => _store = store;
        [BindProperty]
        public InputModel Form { get; set; } = new();

        public class InputModel
        {
            [Required, StringLength(100)]
            public string Title { get; set; } = string.Empty;
            [Required]
            public string Body { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var post = new Post
            {
                Title = Form.Title.Trim(),
                Body = Form.Body.Trim(),
            };
            post = _store.Add(post);
            return RedirectToPage("/Details", new { id = post.Id });
        }

    }
}
