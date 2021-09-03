using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Domains
{
    public class VeiculoDomain
    {
        public int IdVeiculo { get; set; }
        public int IdEmpresa { get; set; }
        public int IdModelo { get; set; }
        public string placaVeiculo { get; set; }
        public EmpresaDomain empresa { get; set; }
        public ModeloDomain modelo { get; set; }
    }
}
