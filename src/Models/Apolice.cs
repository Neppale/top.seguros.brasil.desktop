namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Apolice
    {
        public int? id_apolice { get; set; }
        public string? nome { get; set; }
        public string? data_inicio { get; set; }
        public string? data_fim { get; set; }
        public string? premio { get; set; }
        public string? indenizacao { get; set; }
        public int? id_cobertura { get; set; }
        public int? id_usuario { get; set; }
        public int? id_cliente { get; set; }
        public int? id_veiculo { get; set; }
        public string? message { get; set; }
        public string? status { get; set; }
    }
}
