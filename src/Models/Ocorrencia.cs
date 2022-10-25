using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Ocorrencia
    {
        public int id_ocorrencia { get; set; }
        public string? local { get; set; }
        public string? uf { get; set; }
        public string? municipio { get; set; }
        public string? descricao { get; set; }
        public int? id_veiculo { get; set; }
        public int? id_cliente { get; set; }
        public int? id_terceirizado { get; set; }
        public string? nome { get; set; }
        public string? tipo { get; set; }
        public string? data { get; set; }
        public string? status { get; set; }

        public Ocorrencia(string? data, string? local, string? uf, string? municipio, string? descricao, int? id_veiculo, int? id_cliente, string? tipo, string? status)
        {
            this.data = data;
            this.local = local;
            this.uf = uf;
            this.municipio = municipio;
            this.descricao = descricao;
            this.id_veiculo = id_veiculo;
            this.id_cliente = id_cliente;
            this.tipo = tipo;
            this.status = status;
        }
    }
}
