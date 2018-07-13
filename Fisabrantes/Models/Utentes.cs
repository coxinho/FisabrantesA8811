using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fisabrantes.Models
{
    public class Utentes
    {

        public Utentes()
        {
            ListaDeConsultasAoUtente = new HashSet<Consultas>();
        }


        [Key]
        public int idUtente { get; set; }

        [Display(Name ="Nome do Utente")]
        public string Nome { get; set; }

        [Display(Name ="Data de Nascimento")]
        public DateTime DataNasc { get; set; }

        [Display(Name ="NIF")]
        public string NIF { get; set; }

        [Display(Name ="Nº de Telefone")]
        public string Telefone { get; set; }

        [Display(Name ="Morada")]
        public string Morada { get; set; }

        [Display(Name ="Código Postal")]
        public string CodPostal { get; set; }

        [Display(Name ="Sistema Nacional Saúde")]
        public string SNS { get; set; }

        //**************************************************************************
        // Lista das Consultas associadas ao Utente
        public virtual ICollection<Consultas> ListaDeConsultasAoUtente { get; set; }

        //public object ID { get; internal set; }
    }
}