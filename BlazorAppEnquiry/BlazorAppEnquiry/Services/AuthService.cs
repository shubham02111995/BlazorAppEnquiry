using BlazorAppEnquiry.Data;
using BlazorAppEnquiry.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorAppEnquiry.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly ProtectedSessionStorage _sessionStorage;
        private const string SessionKey = "LoggedInUser";

        public AuthService(AppDbContext context, ProtectedSessionStorage sessionStorage)
        {
            _context = context;
            _sessionStorage = sessionStorage;
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

                await _sessionStorage.SetAsync(SessionKey, userSession);
                return userSession;
            }

            return null;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            try
            {
                var result = await _sessionStorage.GetAsync<User>(SessionKey);
                return result.Success ? result.Value : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync(SessionKey);
        }
    }
}