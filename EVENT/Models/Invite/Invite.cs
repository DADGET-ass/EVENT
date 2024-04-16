using Swashbuckle.AspNetCore.Annotations;

public class Invite
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Date { get; set; }

    [SwaggerSchema("List of user IDs associated with the invite.")]
    public List<int> UserId { get; set; }

    public int EventId { get; set; }
}