using CRUD_IngenieriaWeb.Models;
using CRUD_IngenieriaWeb.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CRUD_IngenieriaWeb.Controllers
{
    public class LoginController : Controller
    {

        private readonly IAPIServiceUsuario _apiServiceUsuario;

        public LoginController(IAPIServiceUsuario apiServiceUsuario)
        {

            _apiServiceUsuario = apiServiceUsuario;

        }

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

        // POST: LoginController/SignIn
        [HttpPost]
        public async Task<IActionResult> Index(Usuario UserToLogin)
        {
            try
            {
                UserToLogin.isAuthenticated = await _apiServiceUsuario.ValidarUsuario(UserToLogin);

                if (UserToLogin.isAuthenticated == true && !UserToLogin.Nombre.IsNullOrEmpty())
                {
                    // Guardar información del usuario en la sesión
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(UserToLogin));
 
                    // Crear una lista de Claims que representan la identidad del usuario
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserToLogin.Nombre),
                    };

                    // Crear un objeto ClaimsIdentity que sirve para representar la identidad del usuario
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Creo las cookies de autenticación
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                    // Redirigir a la página de inicio
                    return RedirectToAction("Index", "Home");
                }

                // Aquí asignamos el mensaje de error
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";

                return View();
            }
            catch(Exception error)
            {
                // Aquí también podrías asignar un mensaje de error si lo deseas
                ViewBag.ErrorMessage = error.Message;
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
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
