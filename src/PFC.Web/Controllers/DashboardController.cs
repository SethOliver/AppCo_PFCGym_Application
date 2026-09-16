using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models.ViewModels;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IGymDataService _data;

    public DashboardController(IGymDataService data) => _data = data;

    public IActionResult Index()
    {
        // Hard-coded member until authentication exists. Once Part 2 adds identity,
        // read the signed-in user here instead.
        const string memberName = "John Wick";

        var today = DateTime.Today;
        var next = _data.GetTimetable()
                        .Where(s => !s.IsFull)
                        .OrderBy(s => ((int)s.Day - (int)today.DayOfWeek + 7) % 7)
                        .ThenBy(s => s.StartsAt)
                        .FirstOrDefault();

        var vm = new DashboardViewModel
        {
            MemberName = memberName,
            Initials = string.Concat(memberName.Split(' ').Select(p => p[0])),
            CurrentPlan = _data.GetPlans().FirstOrDefault(p => p.IsMostPopular),
            NextClass = next,
            ClassesThisMonth = 12,
            NextPaymentDate = DateOnly.FromDateTime(
                new DateTime(today.Year, today.Month, 1).AddMonths(1))
        };

        return View(vm);
    }
}
