using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public abstract class Persona
    {
        protected Persona()
        {
            
        }
        protected Persona(string nome,string cognome)
        {
            Nome = nome;
            Cognome = cognome;
        }

        public string Nome { get; set; }
        public string Cognome { get; set; }

        public abstract string Tipo { get; }

        public virtual string OttieniDescrizione()
        {
            return $"{Tipo} - {Nome} {Cognome}";
        }
    }
}
