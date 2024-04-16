using Microsoft.AspNetCore.Mvc;

public class DeleteInviteModel
{
    [FromHeader]
    public int InviteId { get; set; }
}
