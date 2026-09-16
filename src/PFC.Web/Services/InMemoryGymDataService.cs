using PFC.Web.Models;

namespace PFC.Web.Services;

/// <summary>
/// Seeded content for the front-end deliverable. Mirrors the shape the database
/// will take, so the swap to EF Core is a one-line change in Program.cs.
/// </summary>
public class InMemoryGymDataService : IGymDataService
{
    private readonly List<GymClass> _classes = new()
    {
        new() { Id = 1, Name = "Boxing", Slug = "boxing", Level = "All levels", DurationMinutes = 60,
                Description = "Championship level boxing training focusing on technique, power combinations and ring strategy." },
        new() { Id = 2, Name = "MMA Fundamentals", Slug = "mma", Level = "Beginner", DurationMinutes = 60,
                Description = "Introduction to mixed martial arts covering striking, takedowns and ground control." },
        new() { Id = 3, Name = "Strength & Power", Slug = "strength", Level = "Intermediate", DurationMinutes = 45,
                Description = "Performance focused strength training designed for combat sports athletes." },
        new() { Id = 4, Name = "Muay Thai", Slug = "muaythai", Level = "All levels", DurationMinutes = 60,
                Description = "Traditional Thai boxing with all eight limbs: fists, elbows, knees and shins." },
        new() { Id = 5, Name = "Core Conditioning", Slug = "core", Level = "All levels", DurationMinutes = 45,
                Description = "High-intensity core and conditioning circuits built for combat sport performance." },
        new() { Id = 6, Name = "Submission Grappling", Slug = "grappling", Level = "Advanced", DurationMinutes = 90,
                Description = "Advanced submission wrestling and Brazilian Jiu-Jitsu ground game mastery." }
    };

    private readonly List<Coach> _coaches = new()
    {
        new() { Id = 1, Name = "Marcus Thompson", Role = "Head boxing coach",
                Bio = "Former professional boxer with 3 championship titles. Has trained over 50 amateur champions." },
        new() { Id = 2, Name = "Sofia Erasmus", Role = "Muay Thai & MMA",
                Bio = "Two-time national Muay Thai champion and certified MMA instructor with international fight experience." },
        new() { Id = 3, Name = "Jake Morrison", Role = "Strength & conditioning",
                Bio = "Former Olympic athlete. Has helped hundreds of fighters reach peak physical condition." },
        new() { Id = 4, Name = "Priya Nakamura", Role = "Recovery & mobility",
                Bio = "500-hour RYT with expertise in athletic recovery protocols and movement optimisation." },
        new() { Id = 5, Name = "Leon Baptiste", Role = "Brazilian Jiu-Jitsu",
                Bio = "Black belt under Roger Gracie. Multiple Worlds and Pan-American championship titles." },
        new() { Id = 6, Name = "Maurice Joseph", Role = "Nutrition",
                Bio = "Registered dietitian specialising in combat sports nutrition and body composition." }
    };

    private readonly List<MembershipPlan> _plans = new()
    {
        new() { Id = 1, PricePerMonth = 750m, Features = new()
              { "All group classes", "Locker room & showers" } },
        new() { Id = 2, PricePerMonth = 900m, IsMostPopular = true, Features = new()
              { "All group classes", "Locker room & showers", "Priority class booking", "Nutrition consultation" } },
        new() { Id = 3, PricePerMonth = 1050m, Features = new()
              { "All group classes", "Locker room & showers", "Priority class booking",
                "Nutrition consultation", "Monthly body composition analysis" } }
    };

    private readonly List<Review> _reviews = new()
    {
        new() { Id = 1, MemberName = "Ryan O'Brian", PostedOn = new DateOnly(2024, 9, 1),
                Body = "PFC completely transformed my fitness. The boxing programme under Marcus is world-class. I went from zero experience to competing in my first amateur bout in 8 months." },
        new() { Id = 2, MemberName = "Keisha Williams", PostedOn = new DateOnly(2023, 9, 1),
                Body = "The coaches here are exceptional. Sofia's Muay Thai classes are intense, technical and incredibly rewarding. The community keeps you accountable every single day." },
        new() { Id = 3, MemberName = "Daniel Park", PostedOn = new DateOnly(2025, 9, 1),
                Body = "Best investment I've ever made. The facility is pristine, the equipment is top-tier and the atmosphere is electric. Leon's BJJ coaching alone is worth the membership price." },
        new() { Id = 4, MemberName = "Amara Santos", PostedOn = new DateOnly(2025, 10, 1),
                Body = "I lost 24kg and gained confidence I never knew I had. Nutrition coaching alongside the training programme changed my life." }
    };

    private readonly List<OpeningHours> _hours = new()
    {
        new() { Day = DayOfWeek.Monday,    Opens = new(8, 0),  Closes = new(21, 0) },
        new() { Day = DayOfWeek.Tuesday,   Opens = new(8, 0),  Closes = new(21, 0) },
        new() { Day = DayOfWeek.Wednesday, Opens = new(8, 0),  Closes = new(21, 0) },
        new() { Day = DayOfWeek.Thursday,  Opens = new(8, 0),  Closes = new(21, 0) },
        new() { Day = DayOfWeek.Friday,    Opens = new(8, 0),  Closes = new(20, 0) },
        new() { Day = DayOfWeek.Saturday,  Opens = new(9, 0),  Closes = new(20, 0) },
        new() { Day = DayOfWeek.Sunday,    Opens = new(10, 0), Closes = new(19, 0) }
    };

    private readonly List<TimetableSlot> _timetable = new()
    {
        new() { Id = 1,  Day = DayOfWeek.Monday,    StartsAt = new(6, 30),  DurationMinutes = 60, ClassName = "Elite Boxing",         CoachName = "Marcus Thompson", Capacity = 20, Booked = 12 },
        new() { Id = 2,  Day = DayOfWeek.Monday,    StartsAt = new(12, 0),  DurationMinutes = 45, ClassName = "Core Conditioning",    CoachName = "Jake Morrison",   Capacity = 24, Booked = 9 },
        new() { Id = 3,  Day = DayOfWeek.Monday,    StartsAt = new(17, 30), DurationMinutes = 60, ClassName = "MMA Fundamentals",     CoachName = "Sofia Erasmus",   Capacity = 18, Booked = 15 },
        new() { Id = 4,  Day = DayOfWeek.Monday,    StartsAt = new(19, 0),  DurationMinutes = 90, ClassName = "Submission Grappling", CoachName = "Leon Baptiste",   Capacity = 16, Booked = 16 },
        new() { Id = 5,  Day = DayOfWeek.Tuesday,   StartsAt = new(9, 30),  DurationMinutes = 60, ClassName = "Muay Thai",            CoachName = "Sofia Erasmus",   Capacity = 20, Booked = 7 },
        new() { Id = 6,  Day = DayOfWeek.Tuesday,   StartsAt = new(13, 0),  DurationMinutes = 45, ClassName = "Strength & Power",     CoachName = "Jake Morrison",   Capacity = 16, Booked = 10 },
        new() { Id = 7,  Day = DayOfWeek.Tuesday,   StartsAt = new(18, 0),  DurationMinutes = 60, ClassName = "Boxing",               CoachName = "Marcus Thompson", Capacity = 20, Booked = 14 },
        new() { Id = 8,  Day = DayOfWeek.Wednesday, StartsAt = new(9, 30),  DurationMinutes = 60, ClassName = "Muay Thai",            CoachName = "Sofia Erasmus",   Capacity = 20, Booked = 6 },
        new() { Id = 9,  Day = DayOfWeek.Wednesday, StartsAt = new(12, 0),  DurationMinutes = 45, ClassName = "Core Conditioning",    CoachName = "Jake Morrison",   Capacity = 24, Booked = 11 },
        new() { Id = 10, Day = DayOfWeek.Wednesday, StartsAt = new(17, 30), DurationMinutes = 90, ClassName = "BJJ Open Mat",         CoachName = "Leon Baptiste",   Capacity = 16, Booked = 8 },
        new() { Id = 11, Day = DayOfWeek.Wednesday, StartsAt = new(20, 0),  DurationMinutes = 60, ClassName = "Elite Boxing",         CoachName = "Marcus Thompson", Capacity = 20, Booked = 13 },
        new() { Id = 12, Day = DayOfWeek.Thursday,  StartsAt = new(6, 30),  DurationMinutes = 60, ClassName = "Boxing",               CoachName = "Marcus Thompson", Capacity = 20, Booked = 9 },
        new() { Id = 13, Day = DayOfWeek.Thursday,  StartsAt = new(17, 0),  DurationMinutes = 45, ClassName = "Mobility & Recovery",  CoachName = "Priya Nakamura",  Capacity = 22, Booked = 5 },
        new() { Id = 14, Day = DayOfWeek.Thursday,  StartsAt = new(18, 30), DurationMinutes = 60, ClassName = "MMA Fundamentals",     CoachName = "Sofia Erasmus",   Capacity = 18, Booked = 12 },
        new() { Id = 15, Day = DayOfWeek.Friday,    StartsAt = new(12, 0),  DurationMinutes = 45, ClassName = "Strength & Power",     CoachName = "Jake Morrison",   Capacity = 16, Booked = 8 },
        new() { Id = 16, Day = DayOfWeek.Friday,    StartsAt = new(17, 30), DurationMinutes = 60, ClassName = "Muay Thai",            CoachName = "Sofia Erasmus",   Capacity = 20, Booked = 16 },
        new() { Id = 17, Day = DayOfWeek.Friday,    StartsAt = new(19, 0),  DurationMinutes = 90, ClassName = "Submission Grappling", CoachName = "Leon Baptiste",   Capacity = 16, Booked = 7 },
        new() { Id = 18, Day = DayOfWeek.Saturday,  StartsAt = new(9, 0),   DurationMinutes = 90, ClassName = "Open Mat",             CoachName = "Leon Baptiste",   Capacity = 24, Booked = 10 },
        new() { Id = 19, Day = DayOfWeek.Saturday,  StartsAt = new(11, 0),  DurationMinutes = 60, ClassName = "Boxing",               CoachName = "Marcus Thompson", Capacity = 20, Booked = 6 },
        new() { Id = 20, Day = DayOfWeek.Sunday,    StartsAt = new(10, 30), DurationMinutes = 45, ClassName = "Mobility & Recovery",  CoachName = "Priya Nakamura",  Capacity = 22, Booked = 4 }
    };

    public IReadOnlyList<GymClass> GetClasses() => _classes;

    public GymClass? GetClass(string slug) =>
        _classes.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Coach> GetCoaches() => _coaches;

    public IReadOnlyList<MembershipPlan> GetPlans() => _plans;

    public MembershipPlan? GetPlan(int id) => _plans.FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<TimetableSlot> GetTimetable() => _timetable;

    public IReadOnlyList<Review> GetReviews() => _reviews;

    public IReadOnlyList<OpeningHours> GetOpeningHours() => _hours;

    public OpeningHours GetHoursFor(DayOfWeek day) => _hours.First(h => h.Day == day);

    public bool IsOpenAt(DateTime moment) =>
        GetHoursFor(moment.DayOfWeek).IsOpenAt(TimeOnly.FromDateTime(moment));
}
