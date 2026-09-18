using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models;
using PFC.Web.Models.ViewModels;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class MembershipsController : Controller
{
    private readonly IGymDataService _data;
    private readonly IUserService _users;

    public MembershipsController(IGymDataService data, IUserService users)
    {
        _data = data;
        _users = users;
    }

    public IActionResult Index()
    {
        var vm = new MembershipsViewModel
        {
            Plans = _data.GetPlans(),
            IsSignedIn = User.Identity?.IsAuthenticated == true
        };

        if (vm.IsSignedIn)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "";
            var user = _users.FindByEmail(email);
            vm.IsStaff = user?.Role is Roles.Coach or Roles.Admin;
            if (user?.PlanId is int id) vm.CurrentPlan = _data.GetPlan(id);
        }

        return View(vm);
    }
}