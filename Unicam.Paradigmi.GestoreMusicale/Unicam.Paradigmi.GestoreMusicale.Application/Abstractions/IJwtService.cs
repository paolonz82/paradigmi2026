using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.GestoreMusicale.Application.Abstractions
{
    public interface IJwtService
    {
        string CreateJwt(string username, string password);
    }
}
