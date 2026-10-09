using System;
using System.Collections.Generic;

namespace Unicam.Paradigmi.FirstConsole;

public partial class AlbumsGeneri
{
    public int IdAlbum { get; set; }

    public int IdGenere { get; set; }

    public virtual Generi IdAlbum1 { get; set; } = null!;

    public virtual Album IdAlbumNavigation { get; set; } = null!;
}
