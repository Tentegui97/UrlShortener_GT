using System.Text;
using UrlShortener_GT.Proyecto.Data;
using UrlShortener_GT.Proyecto.Entities;
using UrlShortener_GT.Proyecto.Models;

namespace UrlShortener_GT.Proyecto.Services
{
    public class UrlShortenerService
    {
        private readonly UrlShortenerContext _context;

        public UrlShortenerService(UrlShortenerContext context)
        {
            _context = context;
        }

        private const string AllowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random random = new Random();
        public string UrlShortener(int length)
        {
            var generator = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int randomIndex = random.Next(AllowedCharacters.Length);
                char randomChar = AllowedCharacters[randomIndex];
                generator.Append(randomChar);
            }
            return generator.ToString();
        }
        public void Create(CreateAndUpdateUrlDto dto, int userId)
        {
            Url newUrl = new Url();
            {
                newUrl.NormalUrl = dto.Url;
                newUrl.ShortUrl = UrlShortener(6);
                newUrl.CategoryId = dto.CategoryId;
                newUrl.UserId = userId;
            }
            _context.Urls.Add(newUrl);
            _context.SaveChanges();
        }
        public void Update(CreateAndUpdateUrlDto dto, int urlId)
        {
            Url urlToUpdate = _context.Urls.First(u => u.Id == urlId);
            urlToUpdate.NormalUrl = dto.Url;
            urlToUpdate.ShortUrl = UrlShortener(6);
            urlToUpdate.CategoryId = dto.CategoryId;
            _context.SaveChanges();
        }
        public Url? UrlRedirector(string url)
        {
            Url searchedUrl = _context.Urls.SingleOrDefault(u => u.ShortUrl == url);
            searchedUrl.Click_Counter++;
            _context.SaveChanges();
            return searchedUrl;
        }
        public Url? GetUrl(int id)
        {
            return _context.Urls.SingleOrDefault(u => u.Id == id);
        }
        public List<Url> GetAllByUser(int id)
        {

            return _context.Urls.Include(c => c.User).Where(c => c.User.Id == id).ToList();
        }
        public bool DeleteUrl(int id)
        {
            Url? UrlToDelete = GetUrl(id);
            if (UrlToDelete != null)
            {
                _context.Urls.Remove(GetUrl(id));
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}