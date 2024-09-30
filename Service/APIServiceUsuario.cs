using CRUD_IngenieriaWeb.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace CRUD_IngenieriaWeb.Service
{
    public class APIServiceUsuario : IAPIServiceUsuario
    {

        private static string _baseURL;
        private readonly IActionContextAccessor _actionContextAccessor;

        HttpClient httpClient = new HttpClient();

        public APIServiceUsuario(IActionContextAccessor actionContextAccessor) 
        {

            // Añadir el archivo JSON al contenedor
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseURL = builder.GetSection("ApiSettings:BaseLoginUrl").Value;
            httpClient.BaseAddress = new Uri(_baseURL);

            _actionContextAccessor = actionContextAccessor;

        }

        public async Task<bool> ValidarUsuario(Usuario UserToValidate)
        {
            bool ValidUser;

            var json = JsonConvert.SerializeObject(UserToValidate);

            var newUserJSON = new StringContent(json, Encoding.UTF8, "application/json");

            // Send a GET request to the API
            HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "login", newUserJSON);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {

                // Read the response content as a string
                string content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON string to a list of Producto objects
                ValidUser = JsonConvert.DeserializeObject<bool>(content);

                if (ValidUser)
                {

                    return true;

                }

                return false;

            }
            else
            {

                throw new Exception($"Error: {response.StatusCode}");


            }

        }

        public async Task<Usuario> GetSessionUser()
        {

            // Obtener el HttpContext
            var httpContext = _actionContextAccessor.ActionContext.HttpContext;

            // Hacer una solicitud GET a GetUser
            var response = await httpClient.GetAsync(httpContext.Request.Scheme + "://" + httpContext.Request.Host + "/Usuario/GetUser");

            // Leer la respuesta como una cadena
            var userJson = await response.Content.ReadAsStringAsync();

            // Deserializar la respuesta en un objeto Usuario
            Usuario SessionUser = JsonConvert.DeserializeObject<Usuario>(userJson);

            return SessionUser;

        }

    }
}
