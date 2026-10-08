using System.ComponentModel.DataAnnotations;

namespace MicroBlog.Models
{
    public class Post
    {



            public int Id { get; set; }

            [Required, StringLength(100, ErrorMessage = "Title must be 100 characters or fewer.")]
            public string Title { get; set; } = string.Empty;

            [Required]
            public string Body { get; set; } = string.Empty;

            public DateTime CreatedUTC { get; set; } = DateTime.UtcNow;

        

    }
}
