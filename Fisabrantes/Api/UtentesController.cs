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
    // Controller api dos utentes
    // Para informação sobre routing, ver o App_Start/WebApiConfig.cs
    //[RoutePrefix("api/utentes/{id}/Utentes")]
    public class UtentesController : ApiController
    {
        #region Base de Dados
        private FisabrantesDB db = new FisabrantesDB();

        #endregion

        #region     CRUD: "Read" de Utentes

        // CRUD: Obter uma lista de Utentes
        // GET: Api/Utentes
        public IHttpActionResult Get()
        {
            var resultado = db.Utente.Select(Utente => new      // new { } permite definir um objeto anónimo (sem class) em .net.
            {
                Utente.idUtente,        //Id Utente
                Utente.Nome,            //Nome do Utente
                Utente.DataNasc,        //Data de Nascimento do Utente
                Utente.NIF,             //NIF do Utente
                Utente.Telefone,        //Telefone do Utente
                Utente.Morada,          //Morado do Utente
                Utente.CodPostal,       //Código Postal do Utente
                Utente.SNS              //SNS do Utente
            })
            .ToList();                  // O ToList() executa a query na base de dados e guarda os resultados numa List<>
            // HTTP 200 OK com o JSON resultante (Array de objetos que representam utentes)
            return Ok(resultado);
        }
        // CRUD: Obter um utente, através do seu ID.
        // - Se o utente não existe -> 404 (Not Found)
        // GET: api/Utentes/5
        [ResponseType(typeof(Utentes))]
        public IHttpActionResult GetUtentes(int id)
        {
            Utentes utentes = db.Utente.Find(id);
            if (utentes == null)
            {
                return NotFound();
            }
            return Ok(utentes);
        }

        // Uso de Attribute Routing.
        // Attribute Routing é muito mais poderoso
        // e flexível do que o default da Web API, que só
        // permite operações GET/PUT/POST/DELETE no objeto "raíz".
        // Ver WebApiConfig.cs.
        [HttpGet, Route("api/utentes/{id}/Consultas")]
        public IHttpActionResult GetConsultasByUtente(int id)
        {
            // Este método "restaura" o link removido no "GetAgentes"
            // para podermos ter uma lista de consultas de um utente
            // a partir da API.
            var Utente = db.Utente.Find(id);
            if (Utente == null)
            {
                return NotFound();
            }
            var resultado = Utente.ListaDeConsultasAoUtente.Select(Consulta => new
            {
                Consulta.DataConsulta,
                Consulta.idConsulta,

                Utente = new
                {
                    Utente.idUtente,
                    Utente.Nome,
                    Utente.DataNasc,
                    Utente.NIF,
                    Utente.Telefone,
                    Utente.Morada,
                    Utente.CodPostal,
                    Utente.SNS
                },
                Funcionario = new
                {
                    Consulta.Fisiatra.idFuncionario,
                    Consulta.Fisiatra.Nome,
                },
                Prescricoes = new
                {
                    //Consulta.Prescricao.idPrescricao,
                    //Consulta.Prescricao.Descricao,
                }
            })
            .ToList();
            return Ok(resultado);
        }

        #endregion

        #region     CRUD: Criar Utente
        // CRUD: Criar um Utente
        //POST: api/Utentes
        [ResponseType(typeof(Utentes))]
        public IHttpActionResult Post([FromBody] Utentes model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Para determinar o ID do proximo utente
            var id = db.Utente.Select(id => id.idUtente).Max() + 1;

            var Utente = new Utentes
            {
                idUtente = id,
                Nome = model.Nome,
                DataNasc = model.DataNasc,
                NIF = model.NIF,
                Telefone = model.Telefone,
                Morada = model.Morada,
                CodPostal = model.CodPostal,
                SNS = model.SNS
            };
            db.Utente.Add(Utente);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                if (db.Utente.Count(e => e.idUtente == id) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = Utente.idUtente }, Utente);
        }
        #endregion

        #region     CRUD: Update de Utente
        // CRUD: Atualizar (PUT) um utente, através do seu id.
        // PUT: api/Utentes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Models.Utentes model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Verificar se existe referencia para este id
            if (id > db.Utente.Select(not => not.idUtente).Max())
            {
                return BadRequest("Desculpe, algo está errado.Não foi possível determinar o registro para atualizar");
            }
            // Caso Exista refencia para id fazer update
            var Utente = (from not in db.Utente
                          where not.idUtente == id
                          select not).FirstOrDefault();
            Utente.Nome = model.Nome;
            Utente.DataNasc = model.DataNasc;
            Utente.NIF = model.NIF;
            Utente.Telefone = model.Telefone;
            Utente.Morada = model.Morada;
            Utente.CodPostal = model.CodPostal;
            Utente.SNS = model.SNS;

            db.Entry(Utente);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exp)
            {
                if (!(db.Utente.Count(e => e.idUtente == id) > 0))
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

        #region     CRUD: Eliminar Utente
        // CRUD: Apagar um utente
        // DELETE: api/Utentes/5
        [ResponseType(typeof(Utentes))]
        public IHttpActionResult DeleteUtente(int id)
        {
            Utentes utentes = db.Utente.Find(id);
            if (utentes == null)
            {
                return NotFound();
            }
            db.Utente.Remove(utentes);
            db.SaveChanges();

            return Ok(utentes);
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
        private bool UtenteExists(int id)
        {
            return db.Utente.Count(e => e.idUtente == id) > 0;
        }
        #endregion

    }
}
