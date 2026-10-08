using MicroBlog.Models;
using MicroBlog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroBlog.Pages;

public class IndexModel : PageModel
{

    private readonly IBlogRepository _store;
    public List<Post> Posts { get; private set; } = new();
    public IndexModel(IBlogRepository store) => _store = store;

    public void OnGet()
    {
        Posts = _store.GetAll().ToList();
    }
}
