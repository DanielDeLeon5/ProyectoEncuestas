namespace Encuestas.Entities
{
    public class ValorCampoEncuesta
    {
        public int idEncuesta { get; set; }
        //public List<CampoEncuesta> campoEncuesta { get; set; }
        public List<ValorCampo> valorCampo{ get; set; }
    }
}
