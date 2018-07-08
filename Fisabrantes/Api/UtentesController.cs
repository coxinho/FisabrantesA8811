using Fisabrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Fisabrantes.Api
{
    // Controller API dos utentes.
    [RoutePrefix("api/utentes")]
    public class UtentesController
    {
        #region Base de dados

        // Referência para a base de dados.
        private ApplicationDbContext db = new ApplicationDbContext();

        #endregion

        #region CRUD: "Read" de utentes

        // CRUD: Obter uma lista de Utentes
        // GET: api/Utentes
        public IHttpActionResult GetUtentes()
        {

            var resultado = db.Utentes
                .Select(utente => new // new { } permite definir um objeto anónimo (sem class) em .net.
                {
                    utente.ID, // ID = idUtente,
                    utente.Nome, // Nome = utente.Nome,
                    utente.Morada, // Morada = utente.Morada,
                })
                .ToList(); // O ToList() executa a query na base de dados e guarda os resultados numa List<>.

            // HTTP 200 OK com o JSON resultante (Array de objetos que representam utentes)
            return Ok(resultado);
        }

        // CRUD: Obter um utente, através do seu ID.
        // - Se o utente não existe -> 404 (Not Found)
        // GET: api/Utentes/5
        [ResponseType(typeof(Utentes))]
        public IHttpActionResult GetAgentes(int id)
        {
            Utentes utentes = db.Utentes.Find(id);
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
        [HttpGet, Route("{id}/consultas")]
        public IHttpActionResult GetMultasByAgente(int id)
        {
            // Este método "restaura" o link removido no "GetUtentes"
            // para podermos ter uma lista de consultas de um utente
            // a partir da API.

            var utente = db.Utentes.Find(id);

            if (utente == null)
            {
                return NotFound();
            }

            var resultado = utente.ListaDeConsultas
                .Select(consulta => new
                {
                    consulta.DataDaConsulta,
                    consulta.ID,

                    Utente = new
                    {
                        utente.ID,
                        utente.Nome
                    }
                })
                .ToList();

            return Ok(resultado);
        }

        #endregion


    }
}