using System;
using System.Collections.Generic;

namespace Unicam.Paradigmi.FirstConsole;

public partial class Album
{
    public int IdAlbum { get; set; }

    public string NomeAlbum { get; set; } = null!;

    public string DescrizioneAlbum { get; set; } = null!;

    public int IdArtista { get; set; }

    public DateTime DataUscita { get; set; }

    public decimal Prezzo { get; set; }

    public string Isbn { get; set; } = null!;

    public virtual ICollection<AlbumsGeneri> AlbumsGeneris { get; set; } = new List<AlbumsGeneri>();

    public virtual Artisti IdArtistaNavigation { get; set; } = null!;
}
