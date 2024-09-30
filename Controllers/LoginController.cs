using CRUD_IngenieriaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRUD_IngenieriaWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public IActionResult Index()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetUser()
        {

            // Recuperar el objeto Usuario de la sesión
            var userJson = HttpContext.Session.GetString("User");

            if (userJson != null)
            {

                // Deserializar el objeto Usuario
                Usuario SessionUser = JsonConvert.DeserializeObject<Usuario>(userJson);

                // Devolver el objeto Usuario completo en formato JSON
                return Json(SessionUser);

            }

            // Aquí manejas el caso en que no hay un usuario en la sesión
            // Creas un nuevo objeto Usuario con atributos vacíos y isAuthenticated en false
            Usuario EmptyUser = new Usuario
            {

                Id = 1,
                Nombre = string.Empty,
                Clave = string.Empty,
                isAuthenticated = false

            };

            return Json(EmptyUser);

        }

    }
}
