using MicroBlog.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroBlog.Services
{

    public class JsonBlogRepository : IBlogRepository
    {

        private readonly string _dataDir;
        private readonly string _dataFile;

        public readonly JsonSerializerOptions _json = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private List<Post> _cache = new();
        private int _nextId = 1;

        public JsonBlogRepository(IWebHostEnvironment env)
        {
            _dataDir = Path.Combine(env.ContentRootPath, "data");
            _dataFile = Path.Combine(_dataDir, "posts.json");
            Directory.CreateDirectory(_dataDir);
            LoadFromDisk();
        }

        public IEnumerable<Post> GetAll()
        {
            return _cache
                .OrderByDescending(p => p.CreatedUTC)
                .ToList();
        }

        public Post? GetById(int id)
        {
            return _cache.FirstOrDefault(p => p.Id == id);
        }

        public Post Add(Post post)
        {
            post.Id = _nextId++;
            post.CreatedUTC = DateTime.UtcNow;
            _cache.Add(post);
            return post;
        }

        public void Save()
        {
            var text = JsonSerializer.Serialize(_cache, _json);
            File.WriteAllText(_dataFile, text);
        }

        public void LoadFromDisk()
        {
            if (!File.Exists(_dataFile))
            {
                _cache = new();
                _nextId = 1;
                return;
            }
            var text = File.ReadAllText(_dataFile);
            _cache = JsonSerializer.Deserialize<List<Post>>(text, _json) ?? new();
            _nextId = _cache.Count == 0 ? 1 : _cache.Max(p => p.Id + 1);
        }

    }
}
