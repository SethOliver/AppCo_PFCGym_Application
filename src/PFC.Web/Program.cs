using PFC.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Content currently comes from an in-memory service. When the Part 2 back end
// lands, swap this single registration for the EF Core-backed implementation —
// nothing in the controllers or views has to change.
builder.Services.AddSingleton<IGymDataService, InMemoryGymDataService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
