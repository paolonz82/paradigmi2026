using System.Collections;
using Unicam.Paradigmi.FirstConsole;

var efService = new EfCoreService();
efService.Exec();
//var adoNetService = new AdoNetService();
//adoNetService.Exec();
//var fileService = new FileService();
//fileService.Exec();
/*var list = new ArrayList();
list.Add(new Bicicletta());
list.Add(new Automobile());

foreach(IVeicolo veicolo in list)
{
    veicolo.Accendi();
    Console.WriteLine(veicolo.Tipo);
}
*/
var list = new List<IVeicolo>();
list.Add(new Bicicletta());
list.Add(new Automobile());

foreach(var veicolo in list)
{
    
    if (veicolo is Automobile test)
    {
        test.Accelera(2);
    }
    if (veicolo is Bicicletta)
    {

    }
}

var scatola3 = new Scatola<Automobile>(new Automobile());
scatola3.OttientiContenuto();

var studente = new Studente("Marco", "Rossi", "123456");
var docente = new Docente("Federico", "Paoloni", "Paradigmi");

Console.WriteLine(studente.OttieniDescrizione());
Console.WriteLine(docente.OttieniDescrizione());