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
    public class ConsultasController : ApiController
    {
        #region Base de Dados
        private FisabrantesDB db = new FisabrantesDB();

        #endregion

        #region     CRUD: "Read" de Consultas

        // CRUD: Obter uma lista de Consultas
        // GET: Api/Consultas
        public IHttpActionResult Get()
        {
            var resultado = db.Consulta.Select(Consulta => new      // new { } permite definir um objeto anónimo (sem class) em .net.
            {
                Consulta.idConsulta,        //Id Consulta
                Consulta.DataConsulta,      //Data da consulta
                Consulta.UtenteFK,
                Consulta.FisiatraFK
            })
            .ToList();                  // O ToList() executa a query na base de dados e guarda os resultados numa List<>
            // HTTP 200 OK com o JSON resultante (Array de objetos que representam utentes)
            return Ok(resultado);
        }
        // CRUD: Obter uma consulta, através do seu ID.
        // - Se a consulta não existe -> 404 (Not Found)
        // GET: api/Consultas/5
        [ResponseType(typeof(Consultas))]
        public IHttpActionResult GetConsultas(int id)
        {
            Consultas consulta = db.Consulta.Find(id);
            if (consulta == null)
            {
                return NotFound();
            }
            return Ok(consulta);
        }

        // Uso de Attribute Routing.
        // Attribute Routing é muito mais poderoso
        // e flexível do que o default da Web API, que só
        // permite operações GET/PUT/POST/DELETE no objeto "raíz".
        // Ver WebApiConfig.cs.
        [HttpGet, Route("api/utentes/{id}/Consultas")]
        public IHttpActionResult GetConsultasByUtente(int id)
        {
            // Este método "restaura" o link removido no "GetUtentes"
            // para podermos ter uma lista de consultas de um utente
            // a partir da API.
            var Consulta = db.Consulta.Find(id);
            if (Consulta == null)
            {
                return NotFound();
            }
            var resultado = Consulta.ListaDeConsultasAoUtente.Select(Utentes => new
            {
                Consulta.DataConsulta,
                Consulta.idConsulta,

                Consulta = new
                {
                    Consulta.idConsulta,
                    Consulta.DataConsulta,
                    Consulta.UtenteFK,
                    Consulta.FisiatraFK,
                },
                Funcionario = new
                {
                    Consulta.Fisiatra.idFuncionario,
                    Consulta.Fisiatra.Nome,
                },
                Prescricoes = Consulta.ListaDePrescricoes.Select(p => new
                {
                    p.idPrescricao,
                    p.Descricao,
                }).ToList()
            })
            .ToList();
            return Ok(resultado);
        }

        #endregion

        #region     CRUD: Marcar Consulta
        // CRUD: Marcar uma consulta
        //POST: api/Consulta
        [ResponseType(typeof(Consultas))]
        public IHttpActionResult Post([FromBody] Consultas model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Para determinar o ID do proximo utente
            var id = db.Consulta.Select(c => c.idConsulta).Max() + 1;

            var Consulta = new Consultas
            {
                idConsulta = id,
                DataConsulta = model.DataConsulta,

            };
            db.Consulta.Add(Consulta);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException exp)
            {
                if (db.Consulta.Count(e => e.idConsulta == id) > 0)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = Consulta.idConsulta }, Consulta);
        }
        #endregion

        #region     CRUD: Update de Consulta
        // CRUD: Atualizar (PUT) uma consulta, através do seu id.
        // PUT: api/Consultas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, [FromBody] Models.Consultas model)
        {
            if (!ModelState.IsValid)
            {
                // O BadRequest permite usar o ModelState
                // para informar o utente dos erros de validação
                // tal como no MVC.
                return BadRequest(ModelState);
            }
            // Verificar se existe referencia para este id
            if (id > db.Consulta.Select(not => not.idConsulta).Max())
            {
                return BadRequest("Desculpe, algo está errado.Não foi possível determinar o registro para atualizar");
            }
            // Caso Exista refencia para id fazer update
            var Consulta = (from not in db.Consulta
                          where not.idConsulta == id
                          select not).FirstOrDefault();
            Consulta.DataConsulta = model.DataConsulta;
            Consulta.UtenteFK = model.UtenteFK;
            Consulta.FisiatraFK = model.FisiatraFK;

            db.Entry(Consulta);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exp)
            {
                if (!(db.Consulta.Count(e => e.idConsulta == id) > 0))
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

        #region     CRUD: Eliminar Consulta
        // CRUD: Apagar uma consulta
        // DELETE: api/Consultas/5
        [ResponseType(typeof(Consultas))]
        public IHttpActionResult DeleteConsulta(int id)
        {
            Consultas consultas = db.Consulta.Find(id);
            if (consultas == null)
            {
                return NotFound();
            }
            db.Consulta.Remove(consultas);
            db.SaveChanges();

            return Ok(consultas);
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
        private bool ConsultaExists(int id)
        {
            return db.Consulta.Count(e => e.idConsulta == id) > 0;
        }
        #endregion

    }
}
