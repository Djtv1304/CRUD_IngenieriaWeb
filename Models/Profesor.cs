using CRUD_IngenieriaWeb.Models.ProfesorAttributes;

namespace CRUD_IngenieriaWeb.Models
{
    public class Profesor
    {

        public Profesor()
        {
            experiencia = new List<Experiencia> { new Experiencia() }; // Inicializa la lista con un elemento
            certificaciones = new List<Certificacion> { new Certificacion() }; // Inicializa la lista con un elemento
            documentos = new List<Documento> { new Documento() }; // Inicializa la lista con un elemento
        }

        public string Id { get; set; }

        public string nombre { get; set; }

        public string correo { get; set; }

        public string telefono { get; set; }

        public string direccion { get; set; }

        public List<Experiencia> experiencia;

        public List<Certificacion> certificaciones;

        public List<string> habilidades { get; set; }

        public string estado { get; set; }

        public List<Documento> documentos;

        public bool esTutor { get; set; }


    }
}
