using CRUD_IngenieriaWeb.Models.ProfesorAttributes;

namespace CRUD_IngenieriaWeb.Models
{
    public class Profesor
    {

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
