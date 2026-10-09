using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public partial class Album
    {
        public string NomeCompleto
        {
            get
            {
                return $"{NomeAlbum} - {DescrizioneAlbum}" ;
            }
        }
    }
}
