namespace PFC.Web.Models;

/// <summary>A training class offered by the gym.</summary>
public class GymClass
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Slug { get; set; } = "";
    public string Description { get; set; } = "";
    public string Level { get; set; } = "";
    public int DurationMinutes { get; set; }

    public string ImagePath => $"~/images/class-{Slug}.jpg";
}

/// <summary>A member of the coaching team.</summary>
public class Coach
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Role { get; set; } = "";
    public string Bio { get; set; } = "";

    /// <summary>
    /// The client supplied no photographs of people, so coach cards show a
    /// monogram. Replace with an image path if portraits arrive later.
    /// </summary>
    public string Initials =>
        string.Concat(Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                          .Take(2)
                          .Select(part => part[0]));
}

/// <summary>A monthly membership tier.</summary>
public class MembershipPlan
{
    public int Id { get; set; }
    public decimal PricePerMonth { get; set; }
    public bool IsMostPopular { get; set; }
    public List<string> Features { get; set; } = new();
}

/// <summary>A single scheduled class on the weekly timetable.</summary>
public class TimetableSlot
{
    public int Id { get; set; }
    public DayOfWeek Day { get; set; }
    public TimeOnly StartsAt { get; set; }
    public int DurationMinutes { get; set; }
    public string ClassName { get; set; } = "";
    public string CoachName { get; set; } = "";
    public int Capacity { get; set; }
    public int Booked { get; set; }

    public bool IsFull => Booked >= Capacity;
    public int SpacesLeft => Math.Max(0, Capacity - Booked);
}

/// <summary>A published member review.</summary>
public class Review
{
    public int Id { get; set; }
    public string MemberName { get; set; } = "";
    public string Body { get; set; } = "";
    public int Rating { get; set; } = 5;
    public DateOnly PostedOn { get; set; }

    public string Initials =>
        string.Concat(MemberName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Take(2)
                                .Select(part => part[0]));

    public string TimeAgo
    {
        get
        {
            var months = (DateTime.Today.Year - PostedOn.Year) * 12
                       + DateTime.Today.Month - PostedOn.Month;
            if (months < 1) return "This month";
            if (months < 12) return $"{months} months ago";
            var years = months / 12;
            return years == 1 ? "1 year ago" : $"{years} years ago";
        }
    }
}

/// <summary>One day's opening hours.</summary>
public class OpeningHours
{
    public DayOfWeek Day { get; set; }
    public TimeOnly Opens { get; set; }
    public TimeOnly Closes { get; set; }

    public bool IsOpenAt(TimeOnly time) => time >= Opens && time < Closes;
    public string Display => $"{Opens:HH:mm} – {Closes:HH:mm}";
}
