using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Domain.Entities
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public RuoloEnum Ruolo { get; set; }
    }
}
