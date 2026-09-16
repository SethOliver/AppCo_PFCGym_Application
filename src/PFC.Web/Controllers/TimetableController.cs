using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models.ViewModels;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class TimetableController : Controller
{
    private static readonly DayOfWeek[] WeekOrder =
    {
        DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
        DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
    };

    private readonly IGymDataService _data;

    public TimetableController(IGymDataService data) => _data = data;

    public IActionResult Index(DayOfWeek? day)
    {
        var vm = new TimetableViewModel
        {
            SelectedDay = day ?? DateTime.Today.DayOfWeek,
            Days = WeekOrder,
            SlotsByDay = WeekOrder.ToDictionary(
                d => d,
                d => _data.GetTimetable()
                          .Where(s => s.Day == d)
                          .OrderBy(s => s.StartsAt)
                          .ToList())
        };

        return View(vm);
    }

    /// <summary>
    /// Stand-in for the real booking endpoint. Part 2's back end replaces the
    /// TempData message with a database write and a confirmation email.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Book(int id, DayOfWeek day)
    {
        var slot = _data.GetTimetable().FirstOrDefault(s => s.Id == id);

        if (slot is null)
        {
            TempData["Error"] = "That class could not be found.";
        }
        else if (slot.IsFull)
        {
            TempData["Error"] = $"{slot.ClassName} at {slot.StartsAt:HH\\:mm} is fully booked.";
        }
        else
        {
            TempData["Success"] =
                $"Booked {slot.ClassName} at {slot.StartsAt:HH\\:mm} with {slot.CoachName}. " +
                "A confirmation email is on its way.";
        }

        return RedirectToAction(nameof(Index), new { day });
    }
}
