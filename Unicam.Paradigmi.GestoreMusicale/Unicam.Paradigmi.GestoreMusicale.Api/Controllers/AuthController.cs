using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Paradigmi.GestoreMusicale.Api.Models.Requests;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        [Route("login")]
        public IActionResult Login(AuthLoginRequest request)
        {
            //TODO : CREIAMO IL TOKEN JWT
            string token = _jwtService.CreateJwt(request.Username, request.Password);
            //LO RESTITUIAMO
            return Ok(token);
        }
    }
}
