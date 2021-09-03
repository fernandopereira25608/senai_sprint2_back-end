using senai_rental_webAPI.Domains;
using senai_rental_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = @"Data source=DESKTOP-PJVB3DK\SQLEXPRESS; initial catalog=rental; integrated security=true";
        public void Atualizar(VeiculoDomain novoVeiculo)
        {
            if (novoVeiculo.IdEmpresa != 0 && novoVeiculo.IdModelo != 0 && novoVeiculo.placaVeiculo != null)
            {
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    string queryUpdate = "UPDATE veiculo SET IdEmpresa = @IdEmpresa, IdModelo = @IdModelo, placaVeiculo = @placaVeiculo" +
                        "WHERE IdVeiculo = @IdVeiculo";

                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                    {
                        cmd.Parameters.AddWithValue("@IdEmpresa", novoVeiculo.IdEmpresa);
                        cmd.Parameters.AddWithValue("@IdModelo", novoVeiculo.IdModelo);
                        cmd.Parameters.AddWithValue("@placaVeiculo", novoVeiculo.placaVeiculo);
                        cmd.Parameters.AddWithValue("@IdVeiculo", novoVeiculo.IdVeiculo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public VeiculoDomain BuscarPorId(int id)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                
                string querySelectById = "SELECT IdVeiculo, IdEmpresa, IdModelo, placaVeiculo Placa, end_empresa Unidade, nomeModelo Modelo FROM veiculo " +
                    "INNER JOIN empresa " +
                    "ON empresa.IdEmpresa = veiculo.IdEmpresa" +
                    "INNER JOIN modelo" +
                    "ON veiculo.IdModelo = modelo.IdModelo" +
                    "WHERE veiculo.IdVeiculo = @Idveiculo";

                SqlDataReader rdr;

                using(SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@IdVeiculo", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        EmpresaDomain unidadeEmpresa = new EmpresaDomain()
                        {
                            IdEmpresa = Convert.ToInt32(rdr[1]),
                            end_empresa = rdr[4].ToString(),
                        };

                        ModeloDomain modeloCarro = new ModeloDomain();
                        modeloCarro.nomeModelo = rdr[5].ToString();

                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            IdVeiculo = Convert.ToInt32(rdr[0]),
                            IdEmpresa = Convert.ToInt32(rdr[1]),
                            IdModelo = Convert.ToInt32(rdr[2]),
                            placaVeiculo = rdr[3].ToString(),
                            empresa = unidadeEmpresa,
                            modelo = modeloCarro
                        };
                        return veiculo;
                    }
                    return null;
                }
            }
        }

        public void Deletar(int id)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM veiculo WHERE IdVeiculo = @IdVeiculo";

                using(SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@IdVeiculo", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inserir(VeiculoDomain veiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO veiculo (IdEmpresa, IdModelo, placaVeiculo) VALUES (@IdEmpresa, @IdModelo, @placaVeiculo);";

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@IdEmpresa", veiculo.IdEmpresa);
                    cmd.Parameters.AddWithValue("@IdModelo", veiculo.IdModelo);
                    cmd.Parameters.AddWithValue("@placaVeiculo", veiculo.placaVeiculo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdVeiculo, veiculo.IdEmpresa, veiculo.IdModelo, placaVeiculo Placa, end_empresa Unidade, nomeModelo Modelo FROM veiculo INNER JOIN empresa ON empresa.IdEmpresa = veiculo.IdEmpresa INNER JOIN modelo ON veiculo.IdModelo = modelo.IdModelo";
                SqlDataReader rdr;
                con.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EmpresaDomain unidadeEmpresa = new EmpresaDomain()
                        {
                            IdEmpresa = Convert.ToInt32(rdr[1]),
                            end_empresa = rdr[4].ToString(),           
                        };

                        ModeloDomain modeloCarro = new ModeloDomain();
                        modeloCarro.nomeModelo = rdr[5].ToString();

                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            IdVeiculo = Convert.ToInt32(rdr[0]),
                            IdEmpresa = Convert.ToInt32(rdr[1]),
                            IdModelo = Convert.ToInt32(rdr[2]),
                            placaVeiculo = rdr[3].ToString(),
                            empresa = unidadeEmpresa,
                            modelo = modeloCarro
                        };

                        listaVeiculos.Add(veiculo);
                    }
                }
                return listaVeiculos;
            }         
        }
    }
}
