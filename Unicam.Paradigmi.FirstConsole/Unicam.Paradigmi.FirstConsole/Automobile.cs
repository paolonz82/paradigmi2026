using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public class Automobile : IVeicolo
    {
        public VeicoloStatoEnum Stato { get; set; }
        public int Velocita { get; set; }

        public string Tipo { get => "Automobile"; }
        public int NumeroRuote { get
            {
                return 4;
            }
        }

        public void Accelera(int kmOra)
        {
            Velocita += kmOra;
        }

        public void Accendi()
        {
            Stato = VeicoloStatoEnum.ACCESO;
        }

        public void Frena(int kmOra)
        {
            Velocita-=kmOra;
            if (Velocita < 0)
            {
                Velocita = 0;
            }
        }

        public VeicoloStatoEnum GetStato()
        {
            return Stato;
        }

        public void Spegni()
        {
            Stato = VeicoloStatoEnum.SPENTO;
        }
    }
}
