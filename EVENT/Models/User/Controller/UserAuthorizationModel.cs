using System.ComponentModel.DataAnnotations;

public class UserAuthorizationModel
{
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }

}