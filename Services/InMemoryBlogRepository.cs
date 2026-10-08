using MicroBlog.Models;

namespace MicroBlog.Services
{

    public class InMemoryBlogRepository : IBlogRepository
    {

        public readonly List<Post> _posts = new();
        private int _nextId = 1;

        public IEnumerable<Post> GetAll()
        {
            return _posts
                .OrderByDescending(p => p.CreatedUTC)
                .ToList();
        }

        public Post? GetById(int id)
        {
            return _posts.FirstOrDefault(p => p.Id == id);
        }

        public Post Add(Post post)
        {
            post.Id = _nextId++;
            post.CreatedUTC = DateTime.UtcNow;
            _posts.Add(post);
            return post;
        }

        public void Save() { }

    }

}
