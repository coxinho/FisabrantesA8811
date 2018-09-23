using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fisabrantes.ApiViewModels
{
    public class CreateConsultasViewModel : IValidatableObject
    {
        [Required]
        public DateTime DataConsulta { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Utentes
        //***********************************************************************
        [Required]
        public int UtenteFK { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Funcionários
        //***********************************************************************
        [Required]
        public int FisiatraFK { get; set; }


        //***********************************************************************
        // Outros funcionários presentes nas consultas dos utentes
        //***********************************************************************
        public int TerapeutaFK { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}