public class Event
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required string Date { get; set; }

    public int? CalendarId { get; set; }
}