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
