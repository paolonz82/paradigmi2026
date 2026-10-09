using System;
using System.Collections.Generic;

namespace Unicam.Paradigmi.FirstConsole;

public partial class Generi
{
    public int IdGenere { get; set; }

    public string NomeGenere { get; set; } = null!;

    public virtual ICollection<AlbumsGeneri> AlbumsGeneris { get; set; } = new List<AlbumsGeneri>();
}
