using Microsoft.AspNetCore.Mvc;
using PFC.Web.Models;

namespace PFC.Web.Controllers;

public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;

    public ContactController(ILogger<ContactController> logger) => _logger = logger;

    [HttpGet]
    public IActionResult Index() => View(new ContactForm());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ContactForm form)
    {
        // Server-side validation runs regardless of what the browser did, so the
        // form is still safe if JavaScript is disabled or bypassed.
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        // Part 2's back end replaces this with persistence plus an email send.
        _logger.LogInformation("Contact enquiry from {Email}", form.Email);

        TempData["Success"] =
            "Thanks — your message is on its way. We usually reply within one working day.";

        return RedirectToAction(nameof(Index));
    }
}
