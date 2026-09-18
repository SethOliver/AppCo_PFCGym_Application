using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models;
using PFC.Web.Services;

namespace PFC.Web.Controllers;

public class AccountController : Controller
{
    private readonly IGymDataService _data;
    private readonly IUserService _users;

    public AccountController(IGymDataService data, IUserService users)
    {
        _data = data;
        _users = users;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginForm form, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(form);

        var user = _users.Validate(form.Email, form.Password);

        if (user is null)
        {
            // Deliberately vague: naming which field was wrong tells an attacker
            // which email addresses exist.
            ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
            return View(form);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        // Only follow a local return URL — an absolute one would be an open redirect.
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

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

        if (_users.FindByEmail(form.Email) is not null)
        {
            ModelState.AddModelError(nameof(form.Email), "That email is already registered.");
            ViewBag.Plans = _data.GetPlans();
            return View(form);
        }

        TempData["Success"] = "Account created. Sign in to view your membership.";
        return RedirectToAction(nameof(Login));
    }

    // Named Logout, not SignOut — Controller already has a SignOut method and
    // the names would collide.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["Success"] = "You have been signed out.";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Denied() => View();
}