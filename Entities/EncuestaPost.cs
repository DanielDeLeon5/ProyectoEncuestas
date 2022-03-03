namespace Encuestas.Entities
{
    public class EncuestaPost
    {
        public Encuesta Encuesta { get; set; }
        public List<CampoEncuesta> CampoEncuestas { get; set; }
    }
}
