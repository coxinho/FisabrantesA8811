using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fisabrantes.ApiViewModels
{
    public class CreatePrescricoesViewModel : IValidatableObject
    {
        [Required]
        public string Descricao { get; set; }

        [Required]
        public int ConsultaFK { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}