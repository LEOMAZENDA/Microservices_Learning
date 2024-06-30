using GreekShoping.IdentityServer.Configuration;
using GreekShoping.IdentityServer.Context;
using GreekShoping.IdentityServer.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GreekShoping.IdentityServer.Inicializer;

public class DbInitializer : IDbInitializer
{
    private readonly MySqlContext _context;
    private readonly UserManager<ApplicationUser> _user;
    private readonly RoleManager<IdentityRole> _role;

    public DbInitializer(
        MySqlContext context, 
        UserManager<ApplicationUser> user, 
        RoleManager<IdentityRole> role)
    {
        _context = context;
        _user = user;
        _role = role;
    }

    public void Inicialize()
    {
        //Verificando a role e criando 
        if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;
        _role.CreateAsync(new IdentityRole(
            IdentityConfiguration.Admin)).GetAwaiter().GetResult(); 
        _role.CreateAsync(new IdentityRole(
            IdentityConfiguration.Client)).GetAwaiter().GetResult();

        //A Criar um user Admin 
        ApplicationUser addmin = new ApplicationUser()
        {
            UserName = "leonildo.admin",
            Email = "leonildo.admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+244923684849",
            FirstName = "Leonildo",
            LastName = "Admin"
        };
        _user.CreateAsync(addmin, "AAaa123#").GetAwaiter().GetResult();// passando user admin criado e a senha default (AAaa123#)
        _user.AddToRoleAsync(addmin, IdentityConfiguration.Admin).GetAwaiter().GetResult(); //Passando ao user admin a role admim


        var adminClaims = _user.AddClaimsAsync(addmin, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, $"{addmin.FirstName} {addmin.LastName}"),
            new Claim(JwtClaimTypes.GivenName, addmin.FirstName),
            new Claim(JwtClaimTypes.FamilyName, addmin.LastName),
            new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin),
        }).Result;

        //A Criar um user Cliente 
        ApplicationUser client = new ApplicationUser()
        {
            UserName = "leonildo.client",
            Email = "leonildo.client@gmail.com",
            EmailConfirmed = true,
            PhoneNumber = "+244923684849",
            FirstName = "Leonildo",
            LastName = "Client"
        };
        _user.CreateAsync(client, "AAaa123#").GetAwaiter().GetResult();// passando user client criado e a senha default (AAaa123#)
        _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult(); //Passando ao user client a role client


        var clientClaims = _user.AddClaimsAsync(client, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
            new Claim(JwtClaimTypes.GivenName, client.FirstName),
            new Claim(JwtClaimTypes.FamilyName, client.LastName),
            new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client),
        }).Result;
    }
}
