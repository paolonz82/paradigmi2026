using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public interface IVeicolo
    {
        string Tipo { get; }
        int Velocita { get; set; }
        int NumeroRuote { get;  }
        void Accendi();
        void Spegni();
        VeicoloStatoEnum GetStato();
        void Accelera(int kmOra);
        void Frena(int kmOra);
    }
}
