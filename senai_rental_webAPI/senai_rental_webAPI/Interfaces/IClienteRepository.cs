using senai_rental_webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Interfaces
{
    interface IClienteRepository
    {
        /// <summary>
        /// Lista todos os clintes cadastrados
        /// </summary>
        /// <returns>Uma lista de clientes</returns>
        List<ClienteDomain> ListarTodos();

        /// <summary>
        /// Busca um único cliente em específico
        /// </summary>
        /// <param name="id">ID do cliente a ser buscado</param>
        /// <returns>Objeto CLienteDomain que foi buscado</returns>
        ClienteDomain BuscarPorId(int id);

        /// <summary>
        /// Deleta um cliente específico
        /// </summary>
        /// <param name="idCliente">ID do cliente a ser deletado</param>
        void Deletar(int idCliente);

        /// <summary>
        /// Atualiza as informações de um determinado cliente
        /// </summary>
        /// <param name="novoCliente">Objeto ClienteDomain com as novas informações</param>
        void Atualizar(ClienteDomain novoCliente);

       /// <summary>
       /// Cadastra um novo cliente
       /// </summary>
       /// <param name="cliente">Objeto ClienteDomain que será cadastrado</param>
        void Inserir(ClienteDomain cliente);
    }
}
