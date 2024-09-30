using CRUD_IngenieriaWeb.Models;
using CRUD_IngenieriaWeb.Models.ProfesorAttributes;
using CRUD_IngenieriaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRUD_IngenieriaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIServiceProfesor _apiServiceProfesor;

        public HomeController(IAPIServiceProfesor apiServiceProfesor)
        {
            _apiServiceProfesor = apiServiceProfesor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Crud()
        {
            List<Profesor> profesores = await _apiServiceProfesor.ObtenerProfesores();

            return View(profesores);
        }

        public IActionResult Crear()
        {

            var profesor = new Profesor
            {
                experiencia = new List<Experiencia> { new Experiencia() },
                certificaciones = new List<Certificacion> { new Certificacion() },
                documentos = new List<Documento> { new Documento() }
            };

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Crear(Profesor nuevoProfesor)
        {

            Profesor profesorCreado = await _apiServiceProfesor.CrearProfesor(nuevoProfesor);

            return RedirectToAction("Crud");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
