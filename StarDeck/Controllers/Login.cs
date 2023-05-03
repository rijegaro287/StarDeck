using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Stardeck.Controllers;



[ApiController]
/*
 * Clase Controladora del componente del Login de la Pagina
 */
public class LoginController : Controller
{
    public class loginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Rol { get; set; }
        public string? Id { get; set; }
    }
    private readonly Stardeck.Models.StardeckContext _context;

    public LoginController(Models.StardeckContext context)
    {
        _context = context;
    }

    /**
     * Metodo donde se realiza la autorizacion de los usuarios cuando se presiona el boton de Sign In
     */
    [AllowAnonymous]
    [HttpPut]
    [Route("/login/Signin")]
    public async Task<ActionResult> Login(loginData data)
    {
        Models.Account? user = AuthenticateUser(data);
        if (user == null) { return Unauthorized(); }
        var rol = "User";
        if (user.Serverconfig.ContainsKey("rol") && user.Serverconfig["rol"] == "Admin")
        {
            rol = "Admin";
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Nickname),
            new(ClaimTypes.Role, rol)
        };
        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        if (rol == "Admin")
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
    new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
    {
        IsPersistent = false,
        RedirectUri = "/home",
        AllowRefresh = true
    });
        }
        else
        {

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    IsPersistent = false,
                    RedirectUri = "/home",
                    AllowRefresh = true
                });
        }
        await Console.Out.WriteAsync(JsonSerializer.Serialize(
            HttpContext.Request.Cookies));
        data.Password = "";
        data.Rol = claims[1].Value;
        data.Id = user.Id;

        var res = Ok(JsonSerializer.Serialize(
            data)
        );
        return res;
    }

    /**
     * Metodo que realiza la autenticacion del usuario
     */
    private Models.Account? AuthenticateUser(loginData user)
    {
        //Debug to access with default user and password delete this in production
        Models.Account? account = _context.Accounts.FirstOrDefault(x => x.Email == user.Email);
        if (account == null) { return null; }
        if (account.Password != user.Password) { return null; }
        return account;
    }


    /**
 * Metodo que determina la accion al presionar el boton de Log Out
 */
    [AllowAnonymous]
    [HttpPut]
    [Route("/login/logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (HttpContext.Request.Cookies.ContainsKey(".AspNetCore.Cookies"))
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");

        await Console.Out.WriteAsync("Log out");
        return NoContent();
    }
}