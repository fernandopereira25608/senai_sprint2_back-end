using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Domains
{
    public class AluguelDomain
    {
        public int IdAluguel { get; set; }
        public int IdCliente { get; set; }
        public int IdVeiculo { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public Decimal valorAluguel { get; set; }
        public ClienteDomain cliente { get; set; }
        public VeiculoDomain veiculo { get; set; }
    }
}
