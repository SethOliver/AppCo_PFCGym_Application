using Microsoft.AspNetCore.Mvc;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class CoachesController : Controller
{
    private readonly IGymDataService _data;

    public CoachesController(IGymDataService data) => _data = data;

    public IActionResult Index() => View(_data.GetCoaches());
}
