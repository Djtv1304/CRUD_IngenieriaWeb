using CRUD_IngenieriaWeb.Models;
using CRUD_IngenieriaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace CRUD_IngenieriaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAPIServiceVacante _apiServiceVacante;

        public HomeController(IAPIServiceVacante apiServiceVacante)
        {
            _apiServiceVacante = apiServiceVacante;
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
            List<Vacante> vacantes = await _apiServiceVacante.ObtenerVacantes();

            return View(vacantes);
        }

        public IActionResult Crear()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Crear(Vacante nuevaVacante)
        {

            Vacante vacanteCreada = await _apiServiceVacante.CrearVacante(nuevaVacante);

            return RedirectToAction("Crud");

        }

        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Editar(string idVacante)
        {
            try
            {
                // Invoco a la API y traigo mi lista de vacantes
                List<Vacante> listaVacantes = await _apiServiceVacante.ObtenerVacantes();

                // Verifica que la lista de vacantes no sea null o vacía
                if (listaVacantes != null && listaVacantes.Any())
                {
                    // Busca la vacante por su ID
                    Vacante vacanteToEdit = listaVacantes.FirstOrDefault(v => v.id == idVacante);

                    if (vacanteToEdit != null)
                    {
                        // Retorno la vacante a la vista si la encuentro
                        return View(vacanteToEdit);
                    }
                }

                // Si no encuentra la vacante o la lista está vacía, redirige al índice
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                // Registra el error (opcionalmente puedes agregar un logger aquí)
                Console.WriteLine(error.Message);

                // En caso de error, redirige al índice
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Editar(Vacante nuevaVacante)
        {

            try
            {

                if (nuevaVacante != null)
                {

                    // Envío a la API el nuevo producto y el ID del mismo
                    await _apiServiceVacante.ActualizarVacante(nuevaVacante);
                    return RedirectToAction("Crud");

                }

                // Retorno el miembro a la vista
                return View(nuevaVacante);

            }
            catch (Exception error)
            {

                return View();

            }

        }


        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(string idVacante)
        {

            try
            {

                if (!idVacante.IsNullOrEmpty())
                {

                    await _apiServiceVacante.EliminarVacanteById(idVacante);
                    return RedirectToAction("Crud");

                }

            }
            catch (Exception error)
            {

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");

        }
    }
}
