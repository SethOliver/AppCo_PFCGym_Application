using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models;
using PFC.Web.Models.ViewModels;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

[Authorize]   // everything here requires a signed-in user
public class DashboardController : Controller
{
    private readonly IGymDataService _data;
    private readonly IUserService _users;

    public DashboardController(IGymDataService data, IUserService users)
    {
        _data = data;
        _users = users;
    }

    public IActionResult Index()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "";
        var user = _users.FindByEmail(email);
        if (user is null) return RedirectToAction("Login", "Account");

        if (User.IsInRole(Roles.Admin)) return View("Admin", BuildAdmin(user));
        if (User.IsInRole(Roles.Coach)) return View("Coach", BuildCoach(user));

        return View(BuildMember(user));
    }

    private DashboardViewModel BuildMember(AppUser user)
    {
        var today = DateTime.Today;
        var next = _data.GetTimetable()
                        .Where(s => !s.IsFull)
                        .OrderBy(s => ((int)s.Day - (int)today.DayOfWeek + 7) % 7)
                        .ThenBy(s => s.StartsAt)
                        .FirstOrDefault();

        return new DashboardViewModel
        {
            MemberName = user.FullName,
            Initials = user.Initials,
            CurrentPlan = user.PlanId is null ? null : _data.GetPlan(user.PlanId.Value),
            NextClass = next,
            ClassesThisMonth = 12,
            NextPaymentDate = DateOnly.FromDateTime(
                new DateTime(today.Year, today.Month, 1).AddMonths(1))
        };
    }

    private CoachDashboardViewModel BuildCoach(AppUser user) => new()
    {
        CoachName = user.FullName,
        Initials = user.Initials,
        TodaysClasses = _data.GetTimetable()
            .Where(s => s.CoachName == user.FullName && s.Day == DateTime.Today.DayOfWeek)
            .OrderBy(s => s.StartsAt).ToList(),
        WeeklyClasses = _data.GetTimetable()
            .Where(s => s.CoachName == user.FullName)
            .OrderBy(s => s.Day).ThenBy(s => s.StartsAt).ToList()
    };

    private AdminDashboardViewModel BuildAdmin(AppUser user) => new()
    {
        AdminName = user.FullName,
        Initials = user.Initials,
        TotalMembers = _users.GetAll().Count(u => u.Role == Roles.Member),
        TotalCoaches = _users.GetAll().Count(u => u.Role == Roles.Coach),
        TotalClasses = _data.GetClasses().Count,
        WeeklySlots = _data.GetTimetable().Count,
        FullSlots = _data.GetTimetable().Count(s => s.IsFull),
        Users = _users.GetAll()
    };
}