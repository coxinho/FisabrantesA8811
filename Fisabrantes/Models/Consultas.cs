using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fisabrantes.Models
{
    public class Consultas
    {

        public Consultas()
        {
            ListaDePrescricoes = new HashSet<Prescricoes>();
        }

        [Key]
        public int idConsulta { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataConsulta { get; set; }

        [ForeignKey("Utente")]
        public int UtenteFK { get; set; }
        public virtual Utentes Utente { get; set; }

        [ForeignKey("Fisiatra")]
        public int FisiatraFK { get; set; }
        public virtual Funcionarios Fisiatra { get; set; }


        //Lista de Prescriçoes associadas a esta Consulta
        public virtual ICollection<Prescricoes> ListaDePrescricoes { get; set; }
    }
}