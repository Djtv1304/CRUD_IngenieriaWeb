using CRUD_IngenieriaWeb.Models;

namespace CRUD_IngenieriaWeb.Service
{
    public class APIServiceUsuario
    {

        public async Task<bool> ValidarUsuario(Usuario UserToValidate)
        {

            List<Usuario> listaUsuarios = await GetUsuarios();

            Usuario ValidUser = listaUsuarios.SingleOrDefault(data => data.username.Equals(UserToValidate.username) && data.password.Equals(UserToValidate.password));

            if (ValidUser != null)
            {

                return true;

            }

            return false;

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
