using PFC.Web.Models;

namespace PFC.Web.Services;

/// <summary>
/// Read access to the gym's content. Controllers depend on this interface only,
/// so the in-memory implementation can be replaced by an EF Core repository in
/// Part 2 without touching a controller or a view.
/// </summary>
public interface IGymDataService
{
    IReadOnlyList<GymClass> GetClasses();
    GymClass? GetClass(string slug);
    IReadOnlyList<Coach> GetCoaches();
    IReadOnlyList<MembershipPlan> GetPlans();
    MembershipPlan? GetPlan(int id);
    IReadOnlyList<TimetableSlot> GetTimetable();
    IReadOnlyList<Review> GetReviews();
    IReadOnlyList<OpeningHours> GetOpeningHours();

    OpeningHours GetHoursFor(DayOfWeek day);
    bool IsOpenAt(DateTime moment);
}
