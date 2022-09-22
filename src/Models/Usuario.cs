using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nome_completo { get; set; }
        public string email { get; set; }
        public string tipo { get; set; }
        
        public string senha { get; set; }
        public bool status { get; set; }
        
        public Usuario()
        {

        }

        public Usuario(string nomeCompleto, string email, string tipo, string senha)
        {
            this.nome_completo = nomeCompleto;
            this.email = email;
            this.tipo = tipo;
            this.senha = senha; 
        }

        public Usuario(string nomeCompleto, string email, string senha, string tipo, bool status)
        {

            this.nome_completo = nomeCompleto;
            this.email = email;
            this.senha = senha;
            this.tipo = tipo;
            this.status = status;
        }
    }

    

}
