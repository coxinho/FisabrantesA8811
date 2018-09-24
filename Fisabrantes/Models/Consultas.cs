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
        public int idConsulta { get; set; }   // PK, por exigência da Entity Framework


        [Column(TypeName = "date")]
        public DateTime DataConsulta { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Utentes
        //***********************************************************************
        [ForeignKey("Utente")]
        public int UtenteFK { get; set; }
        public virtual Utentes Utente { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Funcionários
        //***********************************************************************
        [ForeignKey("Fisiatra")]
        public int FisiatraFK { get; set; }
        public virtual Funcionarios Fisiatra { get; set; }
        

        //***********************************************************************
        // Outros funcionários presentes nas consultas dos utentes
        //***********************************************************************
        public virtual Funcionarios Terapeuta { get; set; }


        // sugestao: criar relacionamento N-M (muitas consultas - muitos profissionais)
        // https://github.com/jcnpereira/bd-muitos-para-muitos  (A-B)



        //Lista de Prescriçoes associadas a esta Consulta
        public virtual ICollection<Prescricoes> ListaDePrescricoes { get; set; }
        public IEnumerable<object> ListaDeConsultasAoUtente { get; internal set; }
    }
}