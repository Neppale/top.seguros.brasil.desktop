namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Apolice
    {
        public int id_apolice { get; set; }
        public string? nome { get; set; }
        public string? tipo { get; set; }
        public string? veiculo { get; set; }
        public string? data_inicio { get; set; }
        public string? data_fim { get; set; }
        public double? premio { get; set; }
        public double? indenizacao { get; set; }
        public int? id_cobertura { get; set; }
        public int? id_usuario { get; set; }
        public int? id_cliente { get; set; }
        public int? id_veiculo { get; set; }
        public string? message { get; set; }
        public string? status { get; set; }


        public Apolice(string dataInicio, string dataFim, double premio, double indenizacao, int idCobertura, int idUsuario, int idCliente, int idVeiculo, string status)
        {
            this.data_inicio = dataInicio;
            this.data_fim = dataFim;
            this.premio = premio;
            this.indenizacao = indenizacao;
            this.id_cobertura = idCobertura;
            this.id_usuario = idUsuario;
            this.id_cliente = idCliente;
            this.id_veiculo = idVeiculo;
            this.status = status;
        }

    }

}
