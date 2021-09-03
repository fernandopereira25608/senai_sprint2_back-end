using senai_rental_webAPI.Domains;
using senai_rental_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private string stringConexao = @"Data source=DESKTOP-PJVB3DK\SQLEXPRESS; initial catalog=rental; integrated security=true";
        public void Atualizar(ClienteDomain novoCliente)
        {
            if (novoCliente.IdCliente != 0 && novoCliente.nomeCliente != null && novoCliente.sobrenomeCliente != null && novoCliente.cpfCliente != null )
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdate = "UPDATE cliente SET nomeCliente = @nomeCLiente, sobrenomeCliente = @sobrenomeCliente," +
                        "cpfCliente = @cpfCliente WHERE IdCliente = @IdCliente";

                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", novoCliente.IdCliente);
                        cmd.Parameters.AddWithValue("@nomeCliente", novoCliente.nomeCliente);
                        cmd.Parameters.AddWithValue("@sobrenomeCliente", novoCliente.sobrenomeCliente);
                        cmd.Parameters.AddWithValue("@cpfCliente", novoCliente.cpfCliente);            

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public ClienteDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string querySelectById = "SELECT IdCliente Id, nomeCliente Nome, sobrenomeCliente Sobrenome, cpfCliente CPF FROM cliente" +
                    "WHERE IdCliente = @IdCliente";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        ClienteDomain cliente = new ClienteDomain()
                        {
                            IdCliente = Convert.ToInt32(rdr[0]),
                            nomeCliente = rdr[1].ToString(),
                            sobrenomeCliente = rdr[2].ToString(),
                            cpfCliente = rdr[3].ToString(),
                        };
                        return cliente;
                    }
                    return null;
                }
            }
        }

        public void Deletar(int idCliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM cliente WHERE IdCliente = @IdCliente";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(ClienteDomain cliente)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO cliente (nomeCliente, sobrenomeCliente, cpfCliente) VALUES (@nomeCliente, @sobrenomeCliente, @cpfCliente);";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@nomeCliente", cliente.nomeCliente);
                    cmd.Parameters.AddWithValue("@sobrenomeCliente", cliente.sobrenomeCliente);
                    cmd.Parameters.AddWithValue("@cpfCliente", cliente.cpfCliente);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ClienteDomain> ListarTodos()
        {
            List<ClienteDomain> listaClientes = new List<ClienteDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdCliente Id, nomeCliente Nome, sobrenomeCliente Sobrenome, cpfCliente CPF FROM cliente";
                SqlDataReader rdr;
                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {             
                        ClienteDomain cliente = new ClienteDomain()
                        {
                            IdCliente = Convert.ToInt32(rdr[0]),                            
                            nomeCliente = rdr[1].ToString(),
                            sobrenomeCliente = rdr[2].ToString(),
                            cpfCliente = rdr[3].ToString(),
                        };

                        listaClientes.Add(cliente);
                    }
                }
                return listaClientes;
            }
        }
    }
}
