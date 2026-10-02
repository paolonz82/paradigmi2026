using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public class Scatola<T> where T : IVeicolo
    {
        private T _contenuto;
        public Scatola(T contenuto)
        {
            _contenuto = contenuto;
        }

        public T OttientiContenuto()
        {
            return _contenuto;
        }

        public void SostituisciContenuto(T contenuto)
        {
            _contenuto = contenuto;
        }


    }


}
