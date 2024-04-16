public class CreateInviteModel
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public List<int> UserId { get; set; }

    public int EventId { get; set; }

    public string Date { get; set; }
}
