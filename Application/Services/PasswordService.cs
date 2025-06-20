using System.Security.Cryptography;
using System.Text;

namespace Appointment_Management.Application.Services
{
    public class PasswordService
    {
        public static string HashPassword(string password, out string salt)
        {
            using var hmac = new HMACSHA256();
            salt = Convert.ToBase64String(hmac.Key);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash) == storedHash;
        }
    }
}
