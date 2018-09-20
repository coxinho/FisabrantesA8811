using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Fisabrantes.Models
{
    public class FisabrantesDB
    {
        public FisabrantesDB(): base("DataBaseFisio") { }

        // Instrucoes para criar a tabela dentro da base de dados
        public virtual DbSet<Funcionarios> Funcionario { get; set; }
        public virtual DbSet<Utentes> Utente { get; set; }
        public virtual DbSet<Consultas> Consulta { get; set; }
        public virtual DbSet<Prescricoes> Prescricao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}