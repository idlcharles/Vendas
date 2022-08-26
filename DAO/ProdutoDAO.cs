using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vendas.Models;

namespace vendas.DAO
{
    public class ProdutoDAO : ICRUD
    {

        public ProdutoModel Criar(ProdutoModel model)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                string query = $"insert into produto (nome, marca, valor, categoria) values (@nome, @marca, @valor, @categoria)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nome", model.Nome);
                command.Parameters.AddWithValue("@marca", model.Marca);
                command.Parameters.AddWithValue("@valor", model.Valor);
                command.Parameters.AddWithValue("@categoria", model.Categoria);
                connection.Open();
                model.Id = Convert.ToInt32(command.ExecuteScalar());
            }
            return model;
        }

        public void Atualizar(ProdutoModel model)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                string query = $"update produto set nome = @nome, marca = @marca, valor = @valor, categoria = @categoria where id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nome", model.Nome);
                command.Parameters.AddWithValue("@marca", model.Marca);
                command.Parameters.AddWithValue("@valor", model.Valor);
                command.Parameters.AddWithValue("@categoria", model.Categoria);
                command.Parameters.AddWithValue("@id", model.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                string query = $"delete from produto where id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ProdutoModel Buscar(int id)
        {
            var produto = new ProdutoModel();
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                string query = $"select * from produto where id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    produto.Id = Convert.ToInt32(reader["id"]);
                    produto.Nome = reader["nome"].ToString();
                    produto.Marca = reader["marca"].ToString();
                    produto.Valor = Convert.ToDecimal(reader["valor"]);
                    produto.Categoria = reader["categoria"].ToString();
                }

            }
            return produto;
        }

        public List<ProdutoModel> Listar()
        {
            var listaProdutos = new List<ProdutoModel>();
            using (SqlConnection connection = new SqlConnection(Constants.ConnectionString))
            {
                string query = $"select * from produto";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var produto = new ProdutoModel();
                    produto.Id = Convert.ToInt32(reader["id"]);
                    produto.Nome = reader["nome"].ToString();
                    produto.Marca = reader["marca"].ToString();
                    produto.Valor = Convert.ToDecimal(reader["valor"]);
                    produto.Categoria = reader["categoria"].ToString();

                    listaProdutos.Add(produto);
                }
                return listaProdutos;
            }
        }

    }
}
