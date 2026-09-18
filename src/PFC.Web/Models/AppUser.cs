namespace PFC.Web.Models;

public static class Roles
{
    public const string Member = "Member";
    public const string Coach = "Coach";
    public const string Admin = "Admin";
}

public class AppUser
{
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Role { get; set; } = Roles.Member;

    /// <summary>Null for coaches and admins — they are staff, not members.</summary>
    public int? PlanId { get; set; }

    public string PasswordHash { get; set; } = "";
    public string PasswordSalt { get; set; } = "";

    public string Initials =>
        string.Concat(FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .Take(2).Select(p => p[0]));
}