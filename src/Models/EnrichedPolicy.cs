using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class EnrichedPolicy
    {
        public int id_apolice { get; set; }
        public string? data_inicio { get; set; }
        public string? data_fim { get; set; }
        public double? premio { get; set; }
        public double? indenizacao { get; set; }
        public Cobertura? cobertura { get; set; }
        public Usuario? usuario { get; set; }
        public Cliente? cliente { get; set; }
        public Veiculo? veiculo { get; set; }
        public string? status { get; set; }

        public string? message { get; set; }
        public EnrichedPolicy(int id_apolice, string? data_inicio, string? data_fim, double? premio, double? indenizacao, Cobertura? cobertura, Usuario? usuario, Cliente? cliente, Veiculo? veiculo, string? status, string? message)
        {
            this.id_apolice = id_apolice;
            this.data_inicio = data_inicio;
            this.data_fim = data_fim;
            this.premio = premio;
            this.indenizacao = indenizacao;
            this.cobertura = cobertura;
            this.usuario = usuario;
            this.cliente = cliente;
            this.veiculo = veiculo;
            this.status = status;
            this.message = message;
        }
    }

    
}
