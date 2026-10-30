using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Application.Services;

namespace Unicam.Paradigmi.GestoreMusicale.Web.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        public IActionResult Add()
        {
            /*var albumService = new AlbumService(
                new    Infrastructure.Persistence.Contexts.GestoreMusicaleContext()
                );*/
            
            _albumService.AddAlbum(new Domain.Entities.Album());
            return View();
        }
        
        public async Task<IActionResult> Index()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");
            return View();
        }
    }
}
