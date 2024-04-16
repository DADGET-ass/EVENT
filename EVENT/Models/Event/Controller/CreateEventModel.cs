
using System.ComponentModel.DataAnnotations;

public class CreateEventModel
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Date { get; set; }
    public string? Description {  get; set; }
    public int? CalendarId { get; set; }
}

