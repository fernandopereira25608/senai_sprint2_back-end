using senai_rental_webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Interfaces
{
    interface IVeiculoRepository
    {
        /// <summary>
        /// Lista todos os veículos cadastrados
        /// </summary>
        /// <returns>Uma lista de veículos</returns>
        List<VeiculoDomain> ListarTodos();

        /// <summary>
        /// Busca um único veículo em específico
        /// </summary>
        /// <param name="id">ID do objeto a ser procurado</param>
        /// <returns>Objeto VeiculoDomain que foi buscado</returns>
        VeiculoDomain BuscarPorId(int id);

        /// <summary>
        /// Deleta um veículo específico
        /// </summary>
        /// <param name="id">ID do veículo a ser deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza os dados de um veículo específico
        /// </summary>
        /// <param name="veiculo"></param>
        void Atualizar(VeiculoDomain novoVeiculo);

        /// <summary>
        /// Cadastra um novo veículo
        /// </summary>
        /// <param name="veiculo">Objeto VeiculoDomain com as informações que serão cadastradas</param>
        void Inserir(VeiculoDomain veiculo);
    }
}
