using Encuestas.Entities.Attributes;

namespace Encuestas.Entities
{
    public class CampoEncuesta
    {
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public bool Requerido { get; set; }
        public CamposTipos Tipo { get; set; }
    }
}
