namespace Encuestas.Entities
{
    public class Encuesta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string? Link { get; set; }
    }
}
