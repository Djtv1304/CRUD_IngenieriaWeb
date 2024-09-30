using CRUD_IngenieriaWeb.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace CRUD_IngenieriaWeb.Service
{
    public class APIServiceVacante : IAPIServiceVacante
    {

        private static string _baseURL;
        private readonly IActionContextAccessor _actionContextAccessor;

        HttpClient httpClient = new HttpClient();

        public APIServiceVacante(IActionContextAccessor actionContextAccessor)
        {

            // Añadir el archivo JSON al contenedor
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseURL = builder.GetSection("ApiSettings:BaseVacanteUrl").Value + "contratacionPersonal";

            // Establecer la dirección base del API
            httpClient.BaseAddress = new Uri(_baseURL);

            // Inyectar el contexto de la acción
            _actionContextAccessor = actionContextAccessor;

        }

        public Task<Vacante> ActualizarVacante(Vacante VacanteToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<Vacante> CrearVacante(Vacante VacanteToCreate)
        {
            try
            {
                // Eliminar el ID del Vacante a crear
                VacanteToCreate.id = null;

                // Serializar el objeto VacanteToCreate a JSON
                string jsonContent = JsonConvert.SerializeObject(VacanteToCreate);

                // Crear el contenido de la solicitud HTTP
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint /contratacionPersonal/Vacantees
                HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "/vacante", content);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta como una cadena JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON en un objeto Vacante
                    Vacante VacanteCreado = JsonConvert.DeserializeObject<Vacante>(jsonResponse);

                    return VacanteCreado;
                }
                else
                {
                    // Manejar el caso de error de la solicitud
                    // Puedes lanzar una excepción personalizada o devolver null, dependiendo de tus necesidades
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la solicitud
                // Puedes lanzar una excepción personalizada o devolver null, dependiendo de tus necesidades
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public Task<string> EliminarVacanteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Vacante>> ObtenerVacantes()
        {
            try
            {
                // Realizar la solicitud GET al endpoint /Vacantes/all
                HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "/vacante/all");

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta como una cadena JSON
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON en una lista de objetos Vacante
                    List<Vacante> Vacantes = JsonConvert.DeserializeObject<List<Vacante>>(jsonContent);

                    return Vacantes;
                }
                else
                {
                    // Manejar el caso de error de la solicitud
                    // Puedes lanzar una excepción personalizada o devolver una lista vacía, dependiendo de tus necesidades
                    return new List<Vacante>();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la solicitud
                // Puedes lanzar una excepción personalizada o devolver una lista vacía, dependiendo de tus necesidades
                throw new Exception($"Error: {ex.Message}");

            }
        }

    }
}
