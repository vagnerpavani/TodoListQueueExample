namespace firstproject.Services;

public class PasswordService
{
    private readonly int SALT = 13;

    public string generateHash(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password, SALT);
    }

    public bool verifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}