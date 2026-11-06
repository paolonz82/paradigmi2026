using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Unicam.Paradigmi.GestoreMusicale.Api.Models.Requests;
using Unicam.Paradigmi.GestoreMusicale.Api.Validators;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Application.Options;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;
using Unicam.Paradigmi.GestoreMusicale.Infrastructure.Persistence.Contexts;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    /* L'attributo Api Controller gestisce le validazioni in automatico, 
     * senza la necessità di effettuare in maniera manuale il controllo sul Model State
       Inoltre gestisce automaticamente la fonte del Model Binding, quindi non c'è bisogno di andare 
       a specificare il [FromBody] prima dell'argomento della API
     */
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        public AlbumsController(IAlbumService albumService
            ,IConfiguration configuration
            , GestoreMusicaleContext ctx
            , IOptions<JwtOption> jwtOption
            )
        {
            _albumService = albumService;
            string nomeApplicazione = configuration.GetValue<string>("NomeApplicazione");
            string soloDev = configuration.GetValue<string>("ChiaveSoloInDev");
            string jwtIssuer = configuration.GetValue<string>("Jwt:Issuer");
            string jwtKey = configuration.GetValue<string>("Jwt:SignatureKey");
            int duration = configuration.GetValue<int>("Jwt:Duration");
            var jwtOptionLetto = new JwtOption();
            configuration.GetSection("Jwt").Bind(jwtOptionLetto);
        }
        /*
         [FromBody] permette di leggere i dati dal body ed effettuare la deserializzazione
        [FromHeader] permette di leggere i dati dagli header http della richiesta
        [FromQuery] permette di leggere i dati dalla query string della richiesta
         */
        [HttpPost]
        public IActionResult Inserimento(AggiungiAlbumRequest dto)
        {
            var validator = new AggiungiAlbumRequestValidator();
            var result = validator.Validate(dto);
            if (result.IsValid)
            //if (ModelState.IsValid)
            {
                var album = _albumService.AddAlbum(dto.ToEntity());
                //Restituiamo che l'operazione è andata a buon fine
                return Ok(album);
            }
            return BadRequest(result.Errors);

            var errors = ModelState.Values
                .Where(w=>w.Errors.Count()>0)
                .SelectMany(s => s.Errors.Select(k =>
                {
                    return new
                    {
                        k.ErrorMessage
                    };
                }))
                .ToList();
                
            return BadRequest(errors);
            
        }
    }
}
