using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Domains
{
    public class ClienteDomain
    {
        public int IdCliente { get; set; }
        public string nomeCliente { get; set; }
        public string sobrenomeCliente { get; set; }
        public string cpfCliente { get; set; }
    }
}
