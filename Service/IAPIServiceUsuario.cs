using CRUD_IngenieriaWeb.Models;

namespace CRUD_IngenieriaWeb.Service
{
    public interface IAPIServiceUsuario
    {

        Task<bool> ValidarUsuario(Usuario UserToValidate);

        Task<Usuario> GetSessionUser();

    }
}
