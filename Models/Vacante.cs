namespace CRUD_IngenieriaWeb.Models
{
    public class Vacante
    {

        public string id { get; set; }

        public string titulo { get; set; }

        public string descripcion { get; set; }

        public List<string> requisitos { get; set; }

        public string fecha_publicacion { get; set; }

        public string estado { get; set; }


    }
}
