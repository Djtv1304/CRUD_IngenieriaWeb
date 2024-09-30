using CRUD_IngenieriaWeb.Models;

namespace CRUD_IngenieriaWeb.Service
{
    public interface IAPIServiceVacante
    {

        Task<Vacante> CrearVacante(Vacante VacanteToCreate);

        Task<List<Vacante>> ObtenerVacantes();

        Task<Vacante> ActualizarVacante(Vacante VacanteToUpdate);

        Task<string> EliminarVacanteById(int id);

    }
}
