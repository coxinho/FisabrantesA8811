using Fisabrantes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fisabrantes.Controllers
{
    public class HomeController : Controller
    {
        // referencia a BD
        ApplicationDbContext db = new ApplicationDbContext();
        private object listaFuncionarios;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A fisioterapia atua em três bases principais:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fisabrantes";

            return View();
        }

        public ActionResult Tratamentos()
        {
            ViewBag.Message = "Tratamentos";

            return View();
        }
        public ActionResult Acordos()
        {
            ViewBag.Message = "Acordos";

            return View();
        }

        public ActionResult ListaFuncionarios()
        {
            // pesquisar a lista de funcionários que exixtem na BD
            var listaDeFuncionarios = db.Funcionarios
                                        .OrderBy(f => f.Nome)
                                        .ToList();

            return View(listaDeFuncionarios);
        }
    }
}
