using System.Security.Cryptography;
using System.Text;
using PFC.Web.Models;

namespace PFC.Web.Services;

public class InMemoryUserService : IUserService
{
    private readonly List<AppUser> _users = new();

    public InMemoryUserService()
    {
        Seed(1, "member@pfc.co.za", "John Wick", Roles.Member, "Member123!", planId: 2);
        Seed(2, "sofia@pfc.co.za", "Sofia Erasmus", Roles.Coach, "Coach123!");
        Seed(3, "marcus@pfc.co.za", "Marcus Thompson", Roles.Coach, "Coach123!");
        Seed(4, "admin@pfc.co.za", "Ruan Cupido", Roles.Admin, "Admin123!");
    }

    private void Seed(int id, string email, string name, string role, string password, int? planId = null)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        _users.Add(new AppUser
        {
            Id = id,
            Email = email,
            FullName = name,
            Role = role,
            PlanId = planId,
            PasswordSalt = Convert.ToBase64String(salt),
            PasswordHash = Hash(password, salt)
        });
    }

    // Interim only. Part 2 should replace this with ASP.NET Core Identity's
    // PasswordHasher, which uses PBKDF2 with a work factor. A single SHA-256
    // round is too fast to resist offline brute force.
    private static string Hash(string password, byte[] salt)
    {
        var bytes = SHA256.HashData(salt.Concat(Encoding.UTF8.GetBytes(password)).ToArray());
        return Convert.ToBase64String(bytes);
    }

    public AppUser? Validate(string email, string password)
    {
        var user = FindByEmail(email);
        if (user is null) return null;

        var expected = Hash(password, Convert.FromBase64String(user.PasswordSalt));

        // Fixed-time comparison so response timing can't leak whether the hash
        // was close — a standard defence against timing attacks.
        var ok = CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(expected),
            Encoding.UTF8.GetBytes(user.PasswordHash));

        return ok ? user : null;
    }

    public AppUser? FindByEmail(string email) =>
        _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public AppUser? FindById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public IReadOnlyList<AppUser> GetAll() => _users;
}