using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class AccountController : Controller
{
    private readonly IGymDataService _data;

    public AccountController(IGymDataService data) => _data = data;

    [HttpGet]
    public IActionResult Login() => View(new LoginForm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        // No authentication yet — Part 2's back end adds identity and a real
        // credential check here. The redirect is wired so the flow is testable.
        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register()
    {
        ViewBag.Plans = _data.GetPlans();
        return View(new RegisterForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterForm form)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Plans = _data.GetPlans();
            return View(form);
        }

        TempData["Success"] =
            "Account created. Check your email to confirm your membership.";

        return RedirectToAction(nameof(Login));
    }

    public IActionResult SignOut() => RedirectToAction("Index", "Home");
}
