using System.ComponentModel.DataAnnotations;

namespace PFC.Web.Models;

public class ContactForm
{
    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Please enter your full name.")]
    [Display(Name = "Full name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address, e.g. you@example.co.za")]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Phone(ErrorMessage = "Enter a valid phone number, or leave it blank.")]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "Tell us a little more — at least 10 characters.")]
    [StringLength(1000, MinimumLength = 10,
        ErrorMessage = "Tell us a little more — at least 10 characters.")]
    [Display(Name = "Message")]
    public string Message { get; set; } = "";
}

public class LoginForm
{
    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [Display(Name = "Email address")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Please enter your password.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";
}

public class RegisterForm
{
    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(80, MinimumLength = 2)]
    [Display(Name = "Full name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [Display(Name = "Email address")]
    public string Email { get; set; } = "";

    [Phone(ErrorMessage = "Enter a valid phone number.")]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    [Display(Name = "Membership plan")]
    public int? PlanId { get; set; }

    [Required(ErrorMessage = "Please choose a password.")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "Password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";
}
