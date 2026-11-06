using System.ComponentModel.DataAnnotations;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Models.Requests
{
    public class AggiungiAlbumRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public DateTime DataUscita { get; set; }
        public decimal Prezzo { get; set; }
        public int IdArtista { get; set; }
        public Artista Artista { get; set; }

        public Album ToEntity()
        {
            var entity = new Album();
            entity.Nome = Nome;
            entity.Descrizione = Descrizione;
            entity.Isbn = Isbn;
            entity.DataUscita = DataUscita;
            entity.Prezzo = Prezzo;
            entity.IdArtista = IdArtista;
            return entity;
        }
    }
}
