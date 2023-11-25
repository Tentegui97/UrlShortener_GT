using UrlShortener_GT.Proyecto.Data;
using UrlShortener_GT.Proyecto.Entities;
using UrlShortener_GT.Proyecto.Models;

namespace UrlShortener_GT.Proyecto.Services
{
    public class UserService
    {
        private UrlShortenerContext _context;
        public UserService(UrlShortenerContext context)
        {
            _context = context;
        }
        public User? ValidateUser(AuthenticationRequestBody authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Username == authRequestBody.Username && p.Password == authRequestBody.Password);
        }
        public List<UserDto> GetAll()
        {
            return _context.Users.Select(user => new UserDto()
            {
                Username = user.Username,
                Email = user.Email,
                Id = user.Id
            }).ToList();
        }
        public User? GetUser(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }
        public void CreateUser(CreateAndUpdateUserDto dto)
        {
            User newUser = new User()
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void Update(CreateAndUpdateUserDto dto, int userId)
        {
            User userToUpdate = _context.Users.First(u => u.Id == userId);
            userToUpdate.Username = dto.Username;
            userToUpdate.Password = dto.Password;
            userToUpdate.Email = dto.Email;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(_context.Users.Single(u => u.Id == id));
            _context.SaveChanges();
        }
    }
}
