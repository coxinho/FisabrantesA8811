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
        private object listaAdministrativos;

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

        public ActionResult ListaMedicos()
        {
            // pesquisar a lista de médicos que exixtem na BD
            var listaDeMedicos = db.Funcionarios
                                    .Where(f => f.CatProfissional.Contains("Médic"))
                                    .OrderBy(f => f.Nome)
                                    .ToList();

            return View(listaDeMedicos);
        }

        public ActionResult ListaTerapeutas()
        {
            // pesquisar a lista de terapeutas que exixtem na BD
            var listaDeTerapeutas = db.Funcionarios
                                    .Where(f => f.CatProfissional.Contains("Terapeuta"))
                                    .OrderBy(f => f.Nome)
                                    .ToList();

            return View(listaDeTerapeutas);
        }
        public ActionResult ListaAdministrativos()
        {
            // pesquisar a lista de administrativos que exixtem na BD
            var listaDeAdministrativos = db.Funcionarios
                                    .Where(f => f.CatProfissional.Contains("Administrat"))
                                    .OrderBy(f => f.Nome)
                                    .ToList();

            return View(listaDeAdministrativos);
        }
    }
}
