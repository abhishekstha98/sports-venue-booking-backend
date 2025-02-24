using BCrypt.Net;

public static class PasswordHasher
{
    /// <summary>
    /// Hashes a plain text password using BCrypt.
    /// </summary>
    /// <param name="plainTextPassword">The plain text password to hash.</param>
    /// <returns>Hashed password string.</returns>
    public static string HashPassword(string plainTextPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainTextPassword, workFactor: 12); // 12 is a good security balance
    }

    /// <summary>
    /// Verifies if the given plain text password matches the hashed password.
    /// </summary>
    /// <param name="plainTextPassword">The plain text password entered by the user.</param>
    /// <param name="hashedPassword">The hashed password stored in the database.</param>
    /// <returns>True if the password is valid; otherwise, false.</returns>
    public static bool VerifyPassword(string plainTextPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
    }
}
