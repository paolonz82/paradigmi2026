using FluentValidation;
using Unicam.Paradigmi.GestoreMusicale.Api.Models.Requests;

namespace Unicam.Paradigmi.GestoreMusicale.Api.Validators
{
    public class AggiungiAlbumRequestValidator : AbstractValidator<AggiungiAlbumRequest>
    {
        public AggiungiAlbumRequestValidator()
        {
            RuleFor(r => r.Nome)
                .NotEmpty()
                .WithMessage("Il campo nome è obbligatorio");

            RuleFor(r => r.Descrizione)
                .NotEmpty()
                .WithMessage("Il campo descrizione è obbligatorio")
                .MaximumLength(100)
                .WithMessage("Il campo descrizione non pò essere lungo più di 100 caratteri");

            RuleFor(r => r.Isbn)
                .NotEmpty()
                .WithMessage("Il campo isbn è obbligtorio")
                .Length(10)
                .WithMessage("Il campo isbn deve essere lungo 10 caratteri");

            When(r => r.Prezzo > 2, () =>
            {
                RuleFor(r => r.DataUscita)
                    .GreaterThan(new DateTime(2020, 1, 1))
                    .WithMessage("Per i prodotti che hanno un prezzo maggiore di 2 euro, la data di uscita deve essere superiore al 01/01/2020");
                
                
            });

            RuleFor(r => r.IdArtista)
                .Must(BePresentInsideDatabase);


        }

        private bool BePresentInsideDatabase(int idArtista)
        {
            //TODO : Andare a vedere se è presente o meno all'interno del database
            return true;
        }
    }
}
