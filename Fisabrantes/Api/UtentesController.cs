using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fisabrantes.Api
{
    // Controller API dos utentes.
    // Para informação sobre routing, ver o App_Start/WebApiConfig.cs
    [RoutePrefix("api/utentes")]
    public class UtentesController : ApiController
    {
        #region Base de dados
        //Referência para a base de dados
        private DataBaseFisioDB db = new DataBaseFisioDB();

        #endregion

        #region CRUD: "Read" de utentes
        // CRUD: Obter uma lista de Utentes
        // GET: api/Ugentes
        public IHttpActionResult GetUtentes()
        {
            var resultado = db.Utentes.Select(utente => new { utente.id, utente.nome, utente.morada }).ToList();
            return Ok(resultado);

        }

        // CRUD: Obter um agente, através do seu ID.
        // - Se o agente não existe -> 404 (Not Found)
        // GET: api/Agentes/5


    }
}
