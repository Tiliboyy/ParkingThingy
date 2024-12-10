namespace ParkingThingy.Users;

using System.Security.Cryptography;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;

public class User 
{

    [BsonId]
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public string PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public bool IsAdmin { get; set; } = false;

    public User(string nickname, string password)
    {
        Id = System.Guid.NewGuid();
        PasswordSalt = GenerateSalt();
        string hash = HashPasswordWithSalt(password, PasswordSalt);
        PasswordHash = hash;
        Nickname = nickname;
    }
    
    private static string HashPasswordWithSalt(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, passwordWithSalt, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

            byte[] hashBytes = sha256.ComputeHash(passwordWithSalt);

            return Convert.ToBase64String(hashBytes);
        }
    }
    private static byte[] GenerateSalt()
    {
        var salt = new byte[16]; // 16 bytes = 128 bits
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(salt);
        return salt;
    }

    private bool VerifyHash(string input, string hash)
    {
        string hashOfInput = HashPasswordWithSalt(input, PasswordSalt);
        var comparer = StringComparer.OrdinalIgnoreCase;
        return comparer.Compare(hashOfInput, hash) == 0;
    }
    public void Login(string password)
    {
        
    }

    public bool IsPasswordValid(string password)
    {
        string hash = HashPasswordWithSalt(password, PasswordSalt);
        return hash == PasswordHash;
    }
    public void RequestParkingSlot()
    {
        
    }
}