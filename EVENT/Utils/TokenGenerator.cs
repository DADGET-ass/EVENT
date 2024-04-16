using System.Security.Cryptography;

public class TokenGenerator
{
    public static string GenerateToken(int length = 32)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenData = new byte[length];

            rng.GetBytes(tokenData);

            return BitConverter.ToString(tokenData).Replace("-", "").ToLower();
        }
    }
}