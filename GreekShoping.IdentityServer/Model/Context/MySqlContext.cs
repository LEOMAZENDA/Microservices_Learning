using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.IdentityServer.Model.Context;

public class MySqlContext : IdentityDbContext<ApplicationUser>
{
    public MySqlContext(DbContextOptions<MySqlContext> options)
        : base(options) { }
}
