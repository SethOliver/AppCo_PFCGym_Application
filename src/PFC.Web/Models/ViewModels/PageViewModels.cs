using PFC.Web.Models;

public class CoachDashboardViewModel
{
    public string CoachName { get; set; } = "";
    public string Initials { get; set; } = "";
    public IReadOnlyList<TimetableSlot> TodaysClasses { get; set; } = Array.Empty<TimetableSlot>();
    public IReadOnlyList<TimetableSlot> WeeklyClasses { get; set; } = Array.Empty<TimetableSlot>();
}

public class AdminDashboardViewModel
{
    public string AdminName { get; set; } = "";
    public string Initials { get; set; } = "";
    public int TotalMembers { get; set; }
    public int TotalCoaches { get; set; }
    public int TotalClasses { get; set; }
    public int WeeklySlots { get; set; }
    public int FullSlots { get; set; }
    public IReadOnlyList<AppUser> Users { get; set; } = Array.Empty<AppUser>();
}

public class MembershipsViewModel
{
    public IReadOnlyList<MembershipPlan> Plans { get; set; } = Array.Empty<MembershipPlan>();
    public MembershipPlan? CurrentPlan { get; set; }
    public bool IsSignedIn { get; set; }
    public bool IsStaff { get; set; }
}