using Microsoft.AspNetCore.Mvc;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class ClassesController : Controller
{
    private readonly IGymDataService _data;

    public ClassesController(IGymDataService data) => _data = data;

    public IActionResult Index() => View(_data.GetClasses());
}
