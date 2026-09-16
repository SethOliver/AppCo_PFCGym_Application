using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models.ViewModels;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class HomeController : Controller
{
    private readonly IGymDataService _data;

    public HomeController(IGymDataService data) => _data = data;

    public IActionResult Index()
    {
        var now = DateTime.Now;
        var hours = _data.GetHoursFor(now.DayOfWeek);

        var vm = new HomeViewModel
        {
            FeaturedClasses = _data.GetClasses().Take(3).ToList(),
            FeaturedCoaches = _data.GetCoaches().Take(3).ToList(),
            Plans = _data.GetPlans(),
            Reviews = _data.GetReviews(),
            IsOpenNow = _data.IsOpenAt(now),
            TodayHours = hours.Display,
            CheapestPlan = _data.GetPlans().Min(p => p.PricePerMonth),
            ActiveMembers = 367,
            CoachCount = _data.GetCoaches().Count,
            WeeklyClasses = _data.GetTimetable().Count,
            AmateurChampions = 67,
            YearsOperating = 7
        };

        return View(vm);
    }

    public IActionResult Promo() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
