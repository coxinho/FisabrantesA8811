using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fisabrantes.ApiViewModels
{
    public class CreateFuncionariosViewModel : IValidatableObject
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [Required]
        public string Rua { get; set; }

        [Required]
        public string NumPorta { get; set; }

        [Required]
        public string Localidade { get; set; }

        [Required]
        public string CodPostal { get; set; }

        [Required]
        public string NIF { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataEntClinica { get; set; }

        [Required]
        public string SituacaoProfissional { get; set; }

        [Required]
        public string CatProfissional { get; set; }

        //********************************************************************************     
        // atributo para relacionar os 'funcionários' com os dados da Autenticaçao
        [Required]
        public string UserName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}