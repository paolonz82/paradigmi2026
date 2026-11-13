using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Services
{
    public class JwtService : IJwtService
    {
        public string CreateJwt(string username, string password)
        {
            //Verifichiamo che username e password siano valide
            if (username=="user" && password == "password")
            {
                //Otteniamo la chiave
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiaChiaveMiaChiaveMiaChiaveMiaChiave"));
                //Otteniamo le credenziali di firma
                var credentials = new SigningCredentials(securityKey,
                    SecurityAlgorithms.HmacSha256
                    );

                //Otteniamo l'elenco delle claims per quel determinato utente
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub,username));
                claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, "Pinco"));
                claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, "Pallo"));
                //.... possiamo aggiungere tutte le claim che vogliamo

                //Creiamo una istanza di token jwt
                var token = new JwtSecurityToken(
                    issuer: "https://gestoremusicale.unicam.it/idp",
                    audience : "https://gestoremusicale.unicam.it",
                    claims : claims,
                    expires : DateTime.UtcNow.AddMinutes(60),
                    signingCredentials : credentials
                    );

                //lo andiamo effettivamente a creare

                return new JwtSecurityTokenHandler()
                    .WriteToken(token);
            }
            throw new Exception("Username o password non validi");
        }
    }
}
