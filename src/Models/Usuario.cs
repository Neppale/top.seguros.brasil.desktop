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
        public bool status { get; set; }
    }
}
