using BlazorAppEnquiry.Data;
using BlazorAppEnquiry.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlazorAppEnquiry.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "LoggedInUser";

        public AuthService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                var userSession = new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role
                };

                SetUserSession(userSession);
                return userSession;
            }

            return null;
        }

        public void SetUserSession(User user)
        {
            var jsonUser = JsonSerializer.Serialize(user);
            _httpContextAccessor.HttpContext?.Session.SetString("LoggedInUser", jsonUser);
        }

        public User? GetUserSession()
        {
            var jsonUser = _httpContextAccessor.HttpContext?.Session.GetString("LoggedInUser");
            return jsonUser != null ? JsonSerializer.Deserialize<User>(jsonUser) : null;
        }


        public User? GetCurrentUser()
        {
            try
            {
                var result = GetUserSession();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public  void LogoutAsync()
        {
            _httpContextAccessor.HttpContext?.Session.Remove("LoggedInUser");
        }
    }
}