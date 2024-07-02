using System;
using System.Security.Cryptography;

namespace EmployeeManagementSystem.Helpers
{
    public static class RandomStringGenerator
    {
        public static string GenerateRandomBase64String(int keySizeInBytes = 32)
        {
            // Create a random number generator
            using (var rng = RandomNumberGenerator.Create())
            {
                // Generate random bytes
                var bytes = new byte[keySizeInBytes];
                rng.GetBytes(bytes);

                // Return as a base64-encoded string
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
