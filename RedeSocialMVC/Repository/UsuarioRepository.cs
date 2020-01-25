using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RedeSocialMVC.Models;

namespace RedeSocialMVC.Repository
{
    public class UsuarioRepository
    {

        //0. Saber achar o banco 
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RedeSocialMVC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //Métodos para manipulação de banco de dados:

        //Listar
        public IEnumerable<Usuario> ListarUsuarios()
        {

            //1. Onde vamos armazenar o resultado da consulta?
            var usuarios = new List<Usuario>();

            //2. Estabelecer uma conexão com o banco
            using (var connection = new SqlConnection(connectionString))
            {
                //A conexão existe

                //O que queremos acessar no banco?
                var cmdText = "SELECT * FROM Usuario";
                //Vincular esse comando a uma conexão:
                var select = new SqlCommand(cmdText, connection);


                try
                {
                    connection.Open();
                    using (var reader = select.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //A conexão está aberta; O comando SQL foi executado
                        while (reader.Read())
                        {
                            /*Enquanto for possível ler de reader, 
                            significa que temos um novo usuário armazenado no banco*/
                            var usuario = new Usuario();
                            usuario.Id = (int)reader["Id"];
                            usuario.Nome = reader["Nome"].ToString();
                            usuario.Sobrenome = reader["Sobrenome"].ToString();
                            usuario.Email = reader["Email"].ToString();
                            usuario.Telefone = reader["Telefone"].ToString();
                            usuario.DataNascimento = (DateTime)reader["DataNascimento"];

                            usuarios.Add(usuario);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }


            }
            return usuarios;
        }

        //Criar
        public void CriarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "INSERT INTO Usuario (Nome, Sobrenome, Email, Telefone, DataNascimento) Values(@Nome, @Sobrenome, @Email, @Telefone, @DataNascimento)";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                cmd.Parameters.AddWithValue("@DataNascimento", usuario.DataNascimento);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        //Atualizar
        public void AtualizarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "UPDATE Usuario SET Nome=@Nome, Sobrenome=@Sobrenome, Email=@Email, Telefone=@Telefone, DataNascimento=@DataNascimento WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", usuario.Sobrenome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Telefone", usuario.Telefone);
                cmd.Parameters.AddWithValue("@DataNascimento", usuario.DataNascimento);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        //Detalhar
        public Usuario DetalharUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT Id, Nome, Sobrenome, Email, Telefone, DataNascimento FROM Usuario WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                Usuario usuario = null;
                try
                {
                    connection.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                usuario = new Usuario();
                                usuario.Id = (int)reader["Id"];
                                usuario.Nome = reader["Nome"].ToString();
                                usuario.Sobrenome = reader["Sobrenome"].ToString();
                                usuario.Email = reader["Email"].ToString();
                                usuario.Telefone = reader["Telefone"].ToString();
                                usuario.DataNascimento = (DateTime) reader["DataNascimento"];
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return usuario;
            }
        }
        //Excluir
        public void ExcluirUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "DELETE Usuario WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(cmdText, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
