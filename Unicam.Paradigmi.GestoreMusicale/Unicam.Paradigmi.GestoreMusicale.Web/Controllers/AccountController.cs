using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Web.ViewModels;

namespace Unicam.Paradigmi.GestoreMusicale.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            var vm = new LogonViewModel();
            vm.ReturnUrl = returnUrl;
            return View(vm);
        }

        [AllowAnonymous]
        public async Task Microsoft()
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme);
            
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl)
        {
            //Verifichiamo le credenziali
            var utente = _userService.GetUtenteByUsernameAndPassword(username, password);
            //Siamo sicuri che l'utente esiste e la password è corretta

            //Andiamo ad effettuare l'autenticazione vera e propria

            
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme
                ,"email","role");
            identity.AddClaim(new Claim("user_id", utente.IdUtente.ToString()));
            identity.AddClaim(new Claim("email", utente.Email));
            identity.AddClaim(new Claim("fullname", $"{utente.Cognome} {utente.Nome}"));
            identity.AddClaim(new Claim("role",utente.Ruolo.ToString()));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, new AuthenticationProperties()
            {
                
            });

            return Redirect(returnUrl ?? "/");
        }
    }
}
