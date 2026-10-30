using System;
using System.Collections.Generic;
using System.Text;
using Unicam.Paradigmi.GestoreMusicale.Application.Abstractions;
using Unicam.Paradigmi.GestoreMusicale.Domain.Entities;


namespace Unicam.Paradigmi.GestoreMusicale.Application.Services
{
    public class AlbumService : IAlbumService
    {
        //private readonly GestoreMusicaleContext _gestoreMusicaleContext;
        public AlbumService()
        {
            
        }
        public void AddAlbum(Album album)
        {
            //Scrittura sul database del nuovo album
            
            //Invio dell'email in base al genere dell'album ai subscriber
        }
    }
}
