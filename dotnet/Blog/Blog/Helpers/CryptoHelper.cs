using BCryptNet = BCrypt.Net.BCrypt;


namespace Blog.Helpers;

public static class CryptoHelper
{
    private static readonly string SaltSecret = BCryptNet.GenerateSalt(12);
    
    public static string HashPassword(string password) => BCryptNet.HashPassword(password, SaltSecret);
    
    public static async Task<string> HashPasswordAsync(string password)
    {
        return await Task.Run(() => HashPassword(password));
    }
    
    public static bool VerifyPassword(string password, string hash) => BCryptNet.Verify(password, hash);
} 