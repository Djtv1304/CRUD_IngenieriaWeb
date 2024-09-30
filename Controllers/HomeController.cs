using CRUD_IngenieriaWeb.Models;
using CRUD_IngenieriaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                // Invoco a la API y traigo mi producto en base al ID
                List<Vacante> listaVacantes = await _apiServiceVacante.ObtenerVacantes();

                Vacante vacanteToEdit = listaVacantes.FirstOrDefault(listaVacantes => listaVacantes.id == idVacante);

                if (vacanteToEdit != null)
                {

                    // Retorno el producto a la vista
                    return View(vacanteToEdit);

                }
            }
            catch (Exception error)
            {

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Editar(Miembro nuevoMiembro)
        {

            try
            {

                if (nuevoMiembro != null)
                {

                    // Envío a la API el nuevo producto y el ID del mismo
                    await _apiService.UpdateMiembro(nuevoMiembro, nuevoMiembro.idMiembro);
                    return RedirectToAction("Index");

                }

                // Retorno el miembro a la vista
                return View(nuevoMiembro);

            }
            catch (Exception error)
            {

                return View();

            }

        }


        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int idMiembro)
        {

            try
            {

                if (idMiembro != 0)
                {

                    await _apiService.DeleteMiembro(idMiembro);
                    return RedirectToAction("Index");

                }

            }
            catch (Exception error)
            {

                return RedirectToAction();

            }

            return RedirectToAction("Index");

        }
    }
}
