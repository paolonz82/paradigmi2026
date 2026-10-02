using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Unicam.Paradigmi.FirstConsole
{
    public class FileService
    {
        public IEnumerable<string> GetLines(string file)
        {
            /*
            var lines = File.ReadAllLines(file);
            return lines.ToList();*/
            
            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                yield return line;
            }
            
        }
        public void Exec()
        {
            string directoryPath = Path.Combine("D:", "Paradigmi", "Data");// "D:\\Paradigmi\\Data";
            var di = new DirectoryInfo(directoryPath);
            //bool directoryExists = Directory.Exists(directoryPath);
            if (di.Exists)
            {
                Console.WriteLine($"Directory {directoryPath} Esiste");
            }
            else
            {
                Console.WriteLine($"Directory {directoryPath} Non Esiste");
                di.Create();
            }
            List<Studente> studenti = new();
            //Andiamo a leggere tutti i file presenti all'interno della cartella
            var files = di.GetFiles("*.csv");
            int i = 0;
            foreach(var file in files)
            {
                //string content = System.IO.File.ReadAllText(file.FullName);
                //Console.WriteLine(content);

                foreach(var line in GetLines(file.FullName))
                {
                    i++;
                    if (i == 1)
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var record = new Studente(line);
                    studenti.Add(record);
                }
                /*
              
                /*
               
                Console.WriteLine(lines.Count());
                foreach (var line in lines)
                {
                    i++;
                    if (i == 1)
                    {
                        continue;
                    }
                    studenti.Add(new Studente(line));
                }*/
            }
            Console.WriteLine($"Numero Studenti : {studenti.Count}");
            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new DateConverter("dd/MM/yyyy"));
            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            var json = System.Text.Json.JsonSerializer.Serialize(studenti, jsonOptions);

            var exported = System.IO.File.ReadAllText(Path.Combine("D:", "Paradigmi", "Data", "export.json"));
            var listaDaExportazione = JsonSerializer.Deserialize<List<Studente>>(exported, jsonOptions);
            Console.WriteLine(listaDaExportazione);
        }
    }
}
