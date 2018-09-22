using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fisabrantes.ApiViewModels
{


    public class CreateUtentesViewModel : IValidatableObject
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNasc { get; set; }

        [Required]
        public string NIF { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Morada { get; set; }

        [Required]
        public string CodPostal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }

}