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
    public class PrescricoesController : ApiController
    {
        #region Base de Dados
        private FisabrantesDB db = new FisabrantesDB();

        #endregion

        #region     CRUD: "Read" de Prescricoes

        // CRUD: Obter uma lista de Prescricoes
        // GET: Api/Prescricoes
        public IHttpActionResult Get()
        {
            var resultado = db.Prescricao.Select(Prescricoes => new      // new { } permite definir um objeto anónimo (sem class) em .net.
            {
                Prescricoes.idPrescricao,        //Id Prescricao
                Prescricoes.Descricao,           //Descricao efectuada pelo médico
                Prescricoes.ConsultaFK           //Prescricao referente a dada consulta

            })
            .ToList();                  // O ToList() executa a query na base de dados e guarda os resultados numa List<>
            // HTTP 200 OK com o JSON resultante (Array de objetos que representam prescricoes)
            return Ok(resultado);
        }
        // CRUD: Obter uma prescricao, através do seu ID.
        // - Se a prescricao não existe -> 404 (Not Found)
        // GET: api/Prescricao/5
        [ResponseType(typeof(Prescricoes))]
        public IHttpActionResult GetPrescricoes(int id)
        {
            Prescricoes prescricoes = db.Prescricao.Find(id);
            if (prescricoes == null)
            {
                return NotFound();
            }
            return Ok(prescricoes);
        }

        // Uso de Attribute Routing.
        // Attribute Routing é muito mais poderoso
        // e flexível do que o default da Web API, que só
        // permite operações GET/PUT/POST/DELETE no objeto "raíz".
        // Ver WebApiConfig.cs.
        [HttpGet, Route("api/prescricoes/{id}/Utentes")]
        public IHttpActionResult GetPrescricoesByUtente(int id)
        {
            // Este método "restaura" o link removido no "GetUtentes"
            // para podermos ter uma lista de prescricoes de um utente
            // a partir da API.
            var Prescricao = db.Prescricao.Find(id);
            if (Prescricao == null)
            {
                return NotFound();
            }
            var resultado = Prescricao.ListaDePrescricoes.Select(Utentes => new
            {
                Prescricao.Descricao,
                Prescricao.idPrescricao,

                Prescricao = new
                {
                    Prescricao.idPrescricao,
                    Prescricao.Descricao,
                    Prescricao.ConsultaFK
                },
                Funcionario = new
                {
                    //Prescricao.Fisiatra.idFuncionario,
                    //Prescricao.Fisiatra.Nome,
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

        #region     CRUD: Criar Prescricoes
        // CRUD: Criar uma Prescricao
        //POST: api/Prescricoes
        [ResponseType(typeof(Prescricoes))]
        public IHttpActionResult Post([FromBody] Prescricoes model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o funcionario dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Para determinar o ID da proxima prescricao
            var id = db.Prescricao.Select(id => id.idPrescricao).Max() + 1;

            var Prescricao = new Prescricoes
            {
                idPrescricao = id,
                Descricao = model.Descricao,
                ConsultaFK = model.ConsultaFK
            };
            db.Prescricao.Add(Prescricao);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                if (db.Prescricao.Count(e => e.idPrescricao == id) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = Prescricao.idPrescricao }, Prescricao);
        }
        #endregion

        #region     CRUD: Update de Prescricao
        // CRUD: Atualizar (PUT) uma prescricao, através do seu id.
        // PUT: api/Prescricao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Models.Prescricoes model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Verificar se existe referencia para este id
            if (id > db.Prescricao.Select(not => not.idPrescricao).Max())
            {
                return BadRequest("Desculpe, algo está errado.Não foi possível determinar o registro para atualizar");
            }
            // Caso Exista refencia para id fazer update
            var Prescricao = (from not in db.Prescricao
                          where not.idPrescricao == id
                          select not).FirstOrDefault();
            Prescricao.Descricao = model.Descricao;
            Prescricao.ConsultaFK = model.ConsultaFK;

            db.Entry(Prescricao);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exp)
            {
                if (!(db.Prescricao.Count(e => e.idPrescricao == id) > 0))
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

        #region     CRUD: Eliminar Prescricao
        // CRUD: Apagar uma prescricao
        // DELETE: api/Prescricao/5
        [ResponseType(typeof(Prescricoes))]
        public IHttpActionResult DeletePrescricoes(int id)
        {
            Prescricoes prescricoes = db.Prescricao.Find(id);
            if (prescricoes == null)
            {
                return NotFound();
            }
            db.Prescricao.Remove(prescricoes);
            db.SaveChanges();

            return Ok(prescricoes);
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
        private bool PrescricaoExists(int id)
        {
            return db.Prescricao.Count(e => e.idPrescricao == id) > 0;
        }
        #endregion

    }
}

