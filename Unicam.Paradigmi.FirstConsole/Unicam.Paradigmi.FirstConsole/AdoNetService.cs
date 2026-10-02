using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unicam.Paradigmi.FirstConsole
{
    public class AdoNetService
    {
        public void Exec()
        {
            //Instaurare una connessione
            using var sqlConnection = new SqlConnection();
            
            sqlConnection.ConnectionString = "Server=LOCALHOST;Database=GestoreMusicale;Trusted_Connection=True;Trust Server Certificate=true";
            sqlConnection.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = @"SELECT * FROM Albums alb
                            join Artisti art on alb.IdArtista = art.IdArtista
                            where Prezzo>12";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string nomeAlbum = (string)reader["NomeAlbum"];
                decimal prezzo = (decimal)reader["Prezzo"];
                Console.WriteLine($"Album : {nomeAlbum} - Prezo : {prezzo}");
            }
            Console.ReadLine();
        }
    }
}
