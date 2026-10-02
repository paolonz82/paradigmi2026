using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public class Docente : Persona
    {
        public Docente(string nome, string cognome, string materia)
            :base(nome,cognome)
        {
            Materia = materia;
        }

        public string Materia { get; set; }

        public override string Tipo => "Docente";

        public override string OttieniDescrizione()
        {
            return $"{base.OttieniDescrizione()} - {Materia}";
        }
    }
}
