# PFC — Professional Fighting Championship

ASP.NET Core 8 MVC front end for Task 2 (Code and Implementation), INSY7315
Work Integrated Learning.

**Client:** Professional Fighting Championship, Bothasig, Cape Town
**Team:** Seth Oliver (desktop), Ruan Cupido (mobile)

---

## Running it

Requires the .NET 8 SDK.

```bash
git clone <repo-url>
cd PFC
dotnet restore
dotnet run --project src/PFC.Web
```

Then open https://localhost:7181

Or open `PFC.sln` in Visual Studio 2022 and press F5.

---

## Structure

```
PFC.sln
└── src/PFC.Web/
    ├── Controllers/        8 controllers
    ├── Models/
    │   ├── DomainModels.cs     GymClass, Coach, MembershipPlan, TimetableSlot, Review, OpeningHours
    │   ├── FormModels.cs       ContactForm, LoginForm, RegisterForm — with validation attributes
    │   └── ViewModels/         Per-page view models
    ├── Services/
    │   ├── IGymDataService.cs        the contract controllers depend on
    │   └── InMemoryGymDataService.cs seeded data, swapped for EF Core in Part 2
    ├── Views/
    │   ├── Shared/_Layout.cshtml     header, nav, footer — defined once
    │   ├── Shared/_ClassCard.cshtml  reusable partials
    │   └── …                         one folder per controller
    └── wwwroot/
        ├── css/style.css   all styling, mobile-first
        ├── js/main.js      progressive enhancement only
        └── images/         16 optimised photos of the client's facility
```

---

## One codebase, both breakpoints

Desktop and mobile are the same views and the same stylesheet. There is no
separate mobile site to keep in sync. `style.css` is mobile-first: base rules
are the phone layout, two media queries add complexity as the viewport grows.

| Breakpoint | Width | What changes |
|---|---|---|
| Base | 0–767px | Single column. Hamburger nav. Card rows become horizontal snap-scrollers. Stats 2-up. Popular plan first. Footer collapses to accordions. Sticky Join CTA. |
| Tablet | ≥768px | Scrollers become 2-column grids. Stats 3-up. Plans 3-across, popular one back in the middle. Footer opens to 3 columns. |
| Desktop | ≥1024px | Inline nav replaces the hamburger. 3-column grids. Dashboard gains its fixed sidebar. Sticky CTA hidden. |

Test by dragging the window, or Chrome DevTools → `Ctrl+Shift+M`.

---

## Architecture notes

**Dependency injection.** Controllers take `IGymDataService` in their
constructor and never touch a concrete class. Part 2 replaces one line in
`Program.cs` with an EF Core implementation and no controller or view changes.

**Validation runs twice, deliberately.** `FormModels.cs` carries
`DataAnnotations`, and every POST action checks `ModelState.IsValid` before
doing anything. `main.js` duplicates the same rules client-side for instant
feedback. If JavaScript is disabled the form still posts, the server still
validates, and errors still render — the site loses polish, never safety.

**Anti-forgery tokens** are applied to every POST action via
`[ValidateAntiForgeryToken]`. Razor's form tag helper emits the matching token
automatically.

**JavaScript is enhancement only.** Timetable day tabs are real links that hit
the controller; JS intercepts them to switch instantly and updates the URL with
`history.replaceState`. Turn JS off and every page still works.

---

## Accessibility

- Semantic landmarks throughout: `header`, `nav`, `main`, `footer`, `article`, `figure`
- Skip-to-content link, visible on keyboard focus
- `:focus-visible` outline in brand red on every interactive element
- Nav overlay is a real modal: `aria-modal`, focus moved in, focus trapped on Tab, Escape closes, focus restored
- Every input has a `<label>` via `asp-for`; errors use `role="alert"` and `aria-invalid`
- Timetable uses the `tablist` / `tab` / `tabpanel` pattern
- All images have `alt`; decorative ones are `alt="" aria-hidden="true"`
- `prefers-reduced-motion` disables transitions and scroll reveals
- Tap targets 44×44px minimum

### Colour contrast (WCAG 2.1 AA)

| Pair | Ratio | Result |
|---|---|---|
| White on black | 21.00:1 | Pass |
| Muted `#9C9C9C` on black | 7.65:1 | Pass |
| Muted on card `#171717` | 6.53:1 | Pass |
| Brand red `#FF0000` on black | 5.25:1 | Pass |
| White on button red `#E60000` | 4.81:1 | Pass |

Red is split in two on purpose. `--red` `#FF0000` is the brand accent for text,
borders and icons on black. `--red-btn` `#E60000` fills buttons and badges,
because white on pure `#FF0000` measures 4.00:1 and fails AA at body size. The
two are indistinguishable side by side but the button text becomes compliant.

---

## Images

The 16 files in `wwwroot/images/` come from 29 photographs supplied by the
client, cropped per slot, resized, contrast-lifted (the gym is dimly lit) and
saved as progressive JPEG at quality 80. Roughly 1.7 MB total. Below-the-fold
images carry `loading="lazy"`; the hero carries `fetchpriority="high"`.

Every photograph is of the empty facility — the client supplied no photos of
people. Coach cards therefore render a monogram from the coach's name
(`Coach.Initials`) rather than a portrait. If portraits arrive, add an
`ImagePath` property to `Coach` and swap the div for an `<img>` in
`_CoachCard.cshtml`.

---

## Still to do for Part 2

- **Database.** `InMemoryGymDataService` is seeded in code. Replace with EF Core, a `DbContext` and migrations.
- **Authentication.** `AccountController.Login` redirects without checking credentials. Add ASP.NET Core Identity.
- **Dashboard** reads a hard-coded member name. Needs the signed-in user.
- **Booking** writes a TempData message. Needs a `Booking` table and a capacity check in a transaction.
- **Contact form** logs and discards. Needs persistence plus an email send.
- **Admin, Coach and Fighter role screens** from the prototype are not built — they depend on auth and roles.
- **Hosting and CI/CD.** Azure App Service plus a GitHub Actions workflow.
