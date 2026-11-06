using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Options
{
    public class JwtOption
    {
        public string Issuer { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string SignatureKey { get; set; } = string.Empty;
    }
}
