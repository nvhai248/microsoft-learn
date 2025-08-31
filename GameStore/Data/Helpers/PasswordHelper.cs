using System.Security.Cryptography;
using System.Text;

namespace GameStore.Helpers
{
    public static class PasswordHelper
    {
        // Hash a password with a random salt
        public static string HashPassword(string password, int iterations = 100_000)
        {
            // generate a random salt
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // derive a key
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // store: iterations:salt:hash (all base64)
            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        // Verify a password against a stored hash
        public static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                var parts = storedHash.Split('.');
                if (parts.Length != 3)
                    return false;

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] stored = Convert.FromBase64String(parts[2]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                byte[] computed = pbkdf2.GetBytes(32);

                return CryptographicOperations.FixedTimeEquals(stored, computed);
            }
            catch
            {
                return false;
            }
        }
    }
}
