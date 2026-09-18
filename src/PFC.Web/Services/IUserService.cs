using PFC.Web.Models;

namespace PFC.Web.Services;

public interface IUserService
{
    AppUser? Validate(string email, string password);
    AppUser? FindByEmail(string email);
    AppUser? FindById(int id);
    IReadOnlyList<AppUser> GetAll();
}