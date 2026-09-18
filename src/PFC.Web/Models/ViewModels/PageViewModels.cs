using PFC.Web.Models;

namespace PFC.Web.Models.ViewModels;

public class HomeViewModel
{
    public IReadOnlyList<GymClass> FeaturedClasses { get; set; } = Array.Empty<GymClass>();
    public IReadOnlyList<Coach> FeaturedCoaches { get; set; } = Array.Empty<Coach>();
    public IReadOnlyList<MembershipPlan> Plans { get; set; } = Array.Empty<MembershipPlan>();
    public IReadOnlyList<Review> Reviews { get; set; } = Array.Empty<Review>();

    public bool IsOpenNow { get; set; }
    public string TodayHours { get; set; } = "";
    public decimal CheapestPlan { get; set; }

    public int ActiveMembers { get; set; }
    public int CoachCount { get; set; }
    public int WeeklyClasses { get; set; }
    public int AmateurChampions { get; set; }
    public int YearsOperating { get; set; }
}

public class TimetableViewModel
{
    public DayOfWeek SelectedDay { get; set; }
    public IReadOnlyList<DayOfWeek> Days { get; set; } = Array.Empty<DayOfWeek>();
    public IReadOnlyDictionary<DayOfWeek, List<TimetableSlot>> SlotsByDay { get; set; }
        = new Dictionary<DayOfWeek, List<TimetableSlot>>();
}

public class DashboardViewModel
{
    public string MemberName { get; set; } = "";
    public string Initials { get; set; } = "";
    public MembershipPlan? CurrentPlan { get; set; }
    public TimetableSlot? NextClass { get; set; }
    public int ClassesThisMonth { get; set; }
    public DateOnly NextPaymentDate { get; set; }
}

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