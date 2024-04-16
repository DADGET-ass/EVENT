using System.ComponentModel.DataAnnotations;

public class RefreshEventModel
{
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Date { get; set; }
    public string? Description { get; set; }
}

