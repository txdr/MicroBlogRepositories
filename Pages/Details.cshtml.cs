using MicroBlog.Models;
using MicroBlog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroBlog.Pages
{
    public class DetailsModel : PageModel
    {

        public readonly IBlogRepository _store;
        public DetailsModel(IBlogRepository store) => _store = store;
        public Post? Post { get; private set; }


        public IActionResult OnGet(int id)
        {
            Post = _store.GetById(id);
            if (Post is null) return NotFound();
            return Page();
        }
    }
}
