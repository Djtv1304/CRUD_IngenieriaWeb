using CRUD_IngenieriaWeb.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace CRUD_IngenieriaWeb.Service
{
    public class APIServiceProfesor : IAPIServiceProfesor
    {

        private static string _baseURL;
        private readonly IActionContextAccessor _actionContextAccessor;

        HttpClient httpClient = new HttpClient();

        public APIServiceProfesor(IActionContextAccessor actionContextAccessor)
        {

            // Añadir el archivo JSON al contenedor
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _baseURL = builder.GetSection("ApiSettings:BaseProfesorUrl").Value;

            // Establecer la dirección base del API
            httpClient.BaseAddress = new Uri(_baseURL);

            // Inyectar el contexto de la acción
            _actionContextAccessor = actionContextAccessor;

        }

        public Task<Profesor> ActualizarProfesor(Profesor ProfesorToUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task<Profesor> CrearProfesor(Profesor ProfesorToCreate)
        {
            try
            {
                // Eliminar el ID del profesor a crear
                ProfesorToCreate.Id = "";

                // Serializar el objeto ProfesorToCreate a JSON
                string jsonContent = JsonConvert.SerializeObject(ProfesorToCreate);

                // Crear el contenido de la solicitud HTTP
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST al endpoint /contratacionPersonal/profesores
                HttpResponseMessage response = await httpClient.PostAsync(_baseURL + "contratacionPersonal/profesores", content);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta como una cadena JSON
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON en un objeto Profesor
                    Profesor profesorCreado = JsonConvert.DeserializeObject<Profesor>(jsonResponse);

                    return profesorCreado;
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

        public Task<string> EliminarProfesor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            try
            {
                // Realizar la solicitud GET al endpoint /profesores/all
                HttpResponseMessage response = await httpClient.GetAsync(_baseURL + "contratacionPersonal/profesores/all");

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta como una cadena JSON
                    string jsonContent = await response.Content.ReadAsStringAsync();

                    // Deserializar la cadena JSON en una lista de objetos Profesor
                    List<Profesor> profesores = JsonConvert.DeserializeObject<List<Profesor>>(jsonContent);

                    return profesores;
                }
                else
                {
                    // Manejar el caso de error de la solicitud
                    // Puedes lanzar una excepción personalizada o devolver una lista vacía, dependiendo de tus necesidades
                    return new List<Profesor>();
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
