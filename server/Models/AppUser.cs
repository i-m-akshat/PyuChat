using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? ProfileImage { get; set; }
}