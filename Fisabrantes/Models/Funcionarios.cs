using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fisabrantes.Models
{
    public class Funcionarios
    {

        public Funcionarios()
        {
            ListaDeConsultasDoFuncionario = new HashSet<Consultas>();
        }

        [Key]
        public int idFuncionario { get; set; }

        [Display(Name = "Nome do Funcionário")]
        public string Nome { get; set; }

        [Display(Name = "Data do Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNasc { get; set; }

        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Display(Name = "Nº Porta")]
        public string NumPorta { get; set; }

        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        [Display(Name = "NIF")]
        public string NIF { get; set; }

        [Display(Name = "Data de Entrada na Clínica")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DataEntClinica { get; set; }

        [Display(Name = "Situação Profissional")]
        public string SituacaoProfissional { get; set; }

        [Display(Name = "Categoria Profissional")]
        public string CatProfissional { get; set; }

        //********************************************************************************     
        // atributo para relacionar os 'funcionários' com os dados da Autenticaçao
        //[Required]
        public string UserName { get; set; }

        //********************************************************************************
        // lista das Consultas associadas ao Funcionário
        public virtual ICollection<Consultas> ListaDeConsultasDoFuncionario { get; set; }

    }
}