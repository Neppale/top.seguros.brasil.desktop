using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top_Seguros_Brasil_Desktop.src.Models
{
    public class uf
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? sigla { get; set; }

        public regiao? regiao { get; set; }

    }

    public class regiao
    {
        public int id { get; set; }
        public string? nome { get; set; }
        public string? sigla { get; set; }
    }

    public class municipio
    {
        public int id { get; set; }
        public string? nome { get; set; }
    }
}

