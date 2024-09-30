using CRUD_IngenieriaWeb.Models;

namespace CRUD_IngenieriaWeb.Service
{
    public interface IAPIServiceProfesor
    {

        Task<Profesor> CrearProfesor(Profesor ProfesorToCreate);

        Task<List<Profesor>> ObtenerProfesores();

        Task<Profesor> ActualizarProfesor(Profesor ProfesorToUpdate);

        Task<string> EliminarProfesor(int id);

    }
}
