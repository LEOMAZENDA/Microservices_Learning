using Microsoft.AspNetCore.Identity;

namespace GreekShoping.IdentityServer.Model;

public class ApplicationUser : IdentityUser
{
    private string FirstName { get; set; }
    private string LastName { get; set; }
}
