using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Unicam.Paradigmi.FirstConsole
{
    public class Studente : Persona
    {
        public Studente()
        {
            
        }
        public Studente(string line)
        {
            var values = line.Split(";");
            NumeroMatricola = values[0];
            Nome = values[1];
            Cognome = values[2];
            DataNascita = DateTime.ParseExact(values[3], "yyyyMMdd", null);
        }

        public Studente(string nome, string cognome, string numeroMatricola) 
            : base(nome,cognome)
        {
            NumeroMatricola = numeroMatricola;
        }
        [JsonPropertyName("matricola")]
        public string NumeroMatricola { get; set; }

        //[JsonConverter(typeof(DateConverter))]
        public DateTime DataNascita { get; set; }

        [JsonIgnore]
        public override string Tipo => "Studente";

        public override string OttieniDescrizione()
        {
            return $"{base.OttieniDescrizione()} - {NumeroMatricola}";
        }
    }
}
