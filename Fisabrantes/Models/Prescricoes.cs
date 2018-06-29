using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fisabrantes.Models
{
    public class Prescricoes
    {
        [Key]
        public int idPrescricao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [ForeignKey("Consulta")]
        public int ConsultaFK { get; set; }
        public virtual Consultas Consulta { get; set; }

    }
}