using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Cobertura
    {
        public int id_cobertura { get; set; }
        public string? nome { get; set; }
        public string? descricao { get; set; }
        public string? valor { get; set; }
        public double? taxa_indenizacao { get; set; }
        public bool? status { get; set; }
        public string? message { get; set; }

        public Cobertura(string nome, string descricao, string valor, double taxaIndenizacao, bool status)
        {
            this.nome = nome;
            this.descricao = descricao;
            this.valor = valor;
            this.taxa_indenizacao = taxaIndenizacao;
            this.status = status;
        }
    }
}
