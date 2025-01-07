using System.Security.Cryptography;
using System.Text;

namespace proto_blog_api.Utils
{
    public static class PasswordUtils
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key; // Genera un salt único
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Genera el hash

        }
        // Método de verificacion
        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash); // Compara los hashes
        }
    }
}
