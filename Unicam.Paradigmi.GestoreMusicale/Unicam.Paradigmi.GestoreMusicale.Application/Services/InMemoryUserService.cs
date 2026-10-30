using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Services
{
    public class InMemoryUserService : IUserService
    {

        public Utente GetUtenteByUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("USername non fornita");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password non fornita");
            }
            if (username.Equals("admin") && password.Equals("admin"))
            {
                return new Utente()
                {
                    IdUtente = 1,
                    Cognome = "Admin",
                    Nome = "Admin",
                    Email = "admin@musicale.it",
                    Ruolo = RuoloEnum.ADMIN
                };
            }
            if (username.Equals("customer") && password.Equals("customer"))
            {
                return new Utente()
                {
                    IdUtente = 2,
                    Cognome = "01",
                    Nome = "Customer",
                    Email = "customer01@musicale.it",
                    Ruolo = RuoloEnum.CUSTOMER
                };
            }
            throw new Exception("Utente non esistente");
        }
    }
}
