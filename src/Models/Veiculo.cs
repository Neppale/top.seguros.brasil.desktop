using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Veiculo
    {
        public int id_veiculo { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public string? ano { get; set; }
        public string? uso { get; set; }
        public string? placa { get; set; }
        public string? renavam { get; set; }
        public bool? sinistrado { get; set; }
        public int id_cliente { get; set; }
        

        public Veiculo(string? marca, string? modelo, string? ano, string? uso, string? placa, string? renavam, bool? sinistrado, int idCliente)
        {
            this.marca = marca;
            this.modelo = modelo;
            this.ano = ano;
            this.renavam = renavam;
            this.placa = placa;
            this.uso = uso;
            this.sinistrado = sinistrado;
            this.id_cliente = idCliente;
        }

    }
}
