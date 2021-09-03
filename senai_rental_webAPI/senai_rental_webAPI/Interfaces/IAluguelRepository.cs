using senai_rental_webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Interfaces
{
    interface IAluguelRepository
    {
        /// <summary>
        /// Lista todos os aluguéis efetuados
        /// </summary>
        /// <returns>Uma lista de aluguéis</returns>
        List<AluguelDomain> ListarTodos();

        /// <summary>
        /// Busca um único aluguel em específico
        /// </summary>
        /// <param name="id">ID do aluguel a ser buscado</param>
        /// <returns>Objeto AluguelDomain que foi buscado</returns>
        AluguelDomain BuscarPorId(int id);

        /// <summary>
        /// Deleta um aluguel em específico
        /// </summary>
        /// <param name="idAluguel">ID do aluguel a ser excluído</param>
        void Deletar(int idAluguel);

        /// <summary>
        /// Atualiza as informações de um determinado aluguel
        /// </summary>
        /// <param name="novoAluguel">Objeto AluguelDomain com as novas informações</param>
        void Atualizar(AluguelDomain novoAluguel);

        /// <summary>
        /// Cadastra um novo aluguel
        /// </summary>
        /// <param name="aluguel">Objeto AluguelDomain com as informações a serem cadastradas</param>
        void Inserir(AluguelDomain aluguel);
    }
}
