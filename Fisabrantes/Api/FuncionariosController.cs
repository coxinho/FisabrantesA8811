using Fisabrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace Fisabrantes.Api
{
    // Controller api dos funcionarios
    public class FuncionariosController : ApiController
    {
        #region     Base de Dados
        private FisabrantesDB db = new FisabrantesDB();

        #endregion

        #region    CRUD: "Read" de Funcionarios
        // CRUD: Obter uma lista de Funcionarios
        // GET: Api/Funcionarios
        public IHttpActionResult Get()
        {
            var resultado = db.Funcionario.Select(Funcionario => new      // new { } permite definir um objeto anónimo (sem class) em .net.
            {
                Funcionario.idFuncionario,        //Id Funcionario
                Funcionario.Nome,            //Nome do Funcionario
                Funcionario.DataNasc,        //Data de Nascimento do Funcionario
                Funcionario.Rua,             //Rua do Funcionario
                Funcionario.NumPorta,        //Nº da porta do Funcionario
                Funcionario.Localidade,      //Localidade do Funcionario
                Funcionario.CodPostal,       //Código Postal do Funcionario
                Funcionario.NIF,             //NIF do Funcionario
                Funcionario.DataEntClinica,  //Data de entrada na clinica
                Funcionario.CatProfissional, //Categoria profissional
                Funcionario.UserName         //UserName do Funcionario
            })
            .ToList();                  // O ToList() executa a query na base de dados e guarda os resultados numa List<>
            // HTTP 200 OK com o JSON resultante (Array de objetos que representam funcionarios)
            return Ok(resultado);
        }
        // CRUD: Obter um funcionario, através do seu ID.
        // - Se o funcionario não existe -> 404 (Not Found)
        // GET: api/Funcionarios/5
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult GetFuncionarios(int id)
        {
            Funcionarios funcionarios = db.Funcionario.Find(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return Ok(funcionarios);
        }

        // Uso de Attribute Routing.
        // Attribute Routing é muito mais poderoso
        // e flexível do que o default da Web API, que só
        // permite operações GET/PUT/POST/DELETE no objeto "raíz".
        // Ver WebApiConfig.cs.
        [HttpGet, Route("api/funcionarios/{id}/Consultas")]
        public IHttpActionResult GetConsultasByFuncionarios(int id)
        {
            // Este método "restaura" o link removido no "GetFuncionarios"
            // para podermos ter uma lista de consultas dadas por um funcionario
            // a partir da API.
            var Funcionario = db.Funcionario.Find(id);
            if (Funcionario == null)
            {
                return NotFound();
            }
            var resultado = Funcionario.ListaDeConsultasDoFuncionario.Select(Consulta => new
            {
                //Consulta.DataConsulta,
                //Consulta.idConsulta

                Funcionario = new
                {
                    Funcionario.idFuncionario,
                    Funcionario.Nome,            
                    Funcionario.DataNasc,        
                    Funcionario.Rua,             
                    Funcionario.NumPorta,       
                    Funcionario.Localidade,      
                    Funcionario.CodPostal,       
                    Funcionario.NIF,             
                    Funcionario.DataEntClinica,  
                    Funcionario.CatProfissional, 
                    Funcionario.UserName         
                },
                Prescricoes = new
                {
                    //Consulta.Prescricoes.idPrescricao,
                    //Consulta.Prescricoes.Descricao,
                }
            })
            .ToList();
            return Ok(resultado);
        }

        #endregion

        #region     CRUD: Criar Funcionario
        // CRUD: Criar um Funcionario
        //POST: api/Funcionarios
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult Post([FromBody] Funcionarios model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Para determinar o ID do proximo utente
            var id = db.Funcionario.Select(id => id.idFuncionario).Max() + 1;

            var Funcionario = new Funcionarios
            {
                idFuncionario = id,
                Nome = model.Nome,
                DataNasc = model.DataNasc,
                Rua = model.Rua,
                NumPorta = model.NumPorta,
                Localidade = model.Localidade,
                CodPostal = model.CodPostal,
                NIF = model.NIF,
                DataEntClinica = model.DataEntClinica,
                CatProfissional = model.CatProfissional,
                UserName = model.UserName
            };
            db.Funcionario.Add(Funcionario);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                if (db.Funcionario.Count(e => e.idFuncionario == id) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = Funcionario.idFuncionario }, Funcionario);
        }
        #endregion

        #region     CRUD: Update de Funcionario
        // CRUD: Atualizar (PUT) um funcionario, através do seu id.
        // PUT: api/Funcionarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Models.Funcionarios model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o funcionario dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Verificar se existe referencia para este id
            if (id > db.Funcionario.Select(not => not.idFuncionario).Max())
            {
                return BadRequest("Desculpe, algo está errado.Não foi possível determinar o registro para atualizar");
            }
            // Caso Exista refencia para id fazer update
            var Funcionario = (from not in db.Funcionario
                          where not.idFuncionario == id
                          select not).FirstOrDefault();

            Funcionario.Nome = model.Nome;
            Funcionario.DataNasc = model.DataNasc;
            Funcionario.Rua = model.Rua;
            Funcionario.NumPorta = model.NumPorta;
            Funcionario.Localidade = model.Localidade;
            Funcionario.CodPostal = model.CodPostal;
            Funcionario.NIF = model.NIF;
            Funcionario.DataEntClinica = model.DataEntClinica;
            Funcionario.CatProfissional = model.CatProfissional;
            Funcionario.UserName = model.UserName

            db.Entry(Funcionario);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exp)
            {
                if (!(db.Funcionario.Count(e => e.idFuncionario == id) > 0))
                {
                    return NotFound();
                }
                else
                {
                    throw exp;
                }
            }
            return Ok(model);
        }
        #endregion

        #region     CRUD: Eliminar Funcionario
        // CRUD: Apagar um funcionario
        // DELETE: api/Utentes/5
        [ResponseType(typeof(Funcionarios))]
        public IHttpActionResult DeleteFuncionario(int id)
        {
            Funcionarios funcionarios = db.Funcionario.Find(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            db.Funcionario.Remove(funcionarios);
            db.SaveChanges();

            return Ok(funcionarios);
        }
        #endregion

        #region     CRUD: Métodos utilitários e Dispose
        // Fecha a ligação à base de dados.
        // Este método é chamado pela framework ASP.NET automáticamente,
        // por isso não precisam de o fazer.
        // Nota: quando se criam objetos que usam coisas como BDs, sockets,
        // ou ficheiros, em .net, implementa-se a interface IDisposable.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Função criada pelo template que verifica se já
        // existe um utente com um determinado ID.
        private bool FuncionarioExists(int id)
        {
            return db.Funcionario.Count(e => e.idFuncionario == id) > 0;
        }
        #endregion
    }
}
