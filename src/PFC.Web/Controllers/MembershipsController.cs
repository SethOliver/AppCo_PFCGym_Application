using Microsoft.AspNetCore.Mvc;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class MembershipsController : Controller
{
    private readonly IGymDataService _data;

    public MembershipsController(IGymDataService data) => _data = data;

    public IActionResult Index() => View(_data.GetPlans());
}
