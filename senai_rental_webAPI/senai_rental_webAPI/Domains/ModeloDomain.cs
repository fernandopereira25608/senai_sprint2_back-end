using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Domains
{
    public class ModeloDomain
    {
        public int IdModelo { get; set; }
        public int IdMarca { get; set; }
        public string nomeModelo { get; set; }
        public MarcaDomain marca { get; set; }
    }
}
