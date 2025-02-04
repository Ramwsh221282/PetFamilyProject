namespace PetFamily.Infrastructure.Options;

public class VolunteerCleanDbOptions
{
    public const string VolunteerCleanDb = "VolunteerCleanDb";
    public int RepeatEveryHours { get; set; }
    public int MaxLifeLimitDays { get; set; }
}
