using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Abstractions
{
    public interface IUserService
    {
        Utente GetUtenteByUsernameAndPassword(string username, string password);
    }
}
