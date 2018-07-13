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
        // sugestao: criar relacionamento N-M (muitas consultas - muitos profissionais)
        // https://github.com/jcnpereira/bd-muitos-para-muitos  (A-B)

        [ForeignKey("Terapeuta")]
        public int TerapeutaFK { get; set; }
        public virtual Funcionarios Terapeuta { get; set; }


        //Lista de Prescriçoes associadas a esta Consulta
        public virtual ICollection<Prescricoes> ListaDePrescricoes { get; set; }
    }
}