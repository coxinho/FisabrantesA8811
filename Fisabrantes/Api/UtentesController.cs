
using Fisabrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Fisabrantes.Api
{
    public class UtentesController : ApiController
    {
        #region Base de Dados
        private FisabrantesDB db = new FisabrantesDB();

        #endregion

        #region     CRUD: "Read" de Utentes
        // GET: Api/Utentes
        public IHttpActionResult Get()
        {
            var resultado = db.Utente.Select(Utente => new
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
            return Ok(resultado);
        }
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
        [HttpGet, Route("api/utentes/{id}/Consultas")]
        public IHttpActionResult GetConsultasByUtente(int id)
        {
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
                    Utente.SNS,

                }

            })
            .ToList();
            return Ok(resultado);
        }

        #endregion

        #region     CRUD: Criar Utente


        #endregion

        #region     CRUD: Update de Utente

        #endregion

        #region     CRUD: Eliminar Utente

        #endregion


    }
}







//using Fisabrantes.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;

//namespace Fisabrantes.Api
//{
//    // Controller API dos utentes.
//    // Para informação sobre routing, ver o App_Start/WebApiConfig.cs
//    [RoutePrefix("api/utentes")]
//    public class UtentesController : ApiController
//    {
//        #region Base de dados
//        //Referência para a base de dados
//        private Models. FisabrantesDB db = new Models.FisabrantesDB();

//        #endregion

//        #region CRUD: "Read" de utentes
//        // CRUD: Obter uma lista de Utentes
//        // GET: api/Ugentes
//        public IHttpActionResult GetUtentes()
//        {
//            var resultado = db.Utente.Select(utente => new { utente.id, utente.nome, utente.morada }).ToList();
//            return Ok(resultado);

//        }

//        // CRUD: Obter um agente, através do seu ID.
//        // - Se o agente não existe -> 404 (Not Found)
//        // GET: api/Agentes/5
//        //[ResponseType(typeof(Utentes))]
//        //public IHttpActionResult GetUtentes(int id)
//        //{
//        //    Agentes agentes = db.Agentes.Find(id);
//        //    if (agentes == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    return Ok(agentes);
//        //}
//        //[HttpGet, Route("{id}/utentes")]
//        //public IHttpActionResult GetConsultasByUtente(int id)
//        //{
//        //    var utente = db.Utente.Find(id);
//        //    if (utente == null)
//        //    {
//        //        return NotFound();
//        //    }
//        //    var resultado = utente.ListaDeConsultas
//        //        .Select(consulta => new
//        //        {
//        //            consulta.DataDaConsulta,
//        //            consulta.ID,

//        //            Utente = new
//        //            {
//        //                utente.ID,
//        //                utente.Nome,
//        //        })
//        //        .ToList();

//        //    return Ok(resultado);
//        //}
//        //#endregion

//    }
//}
