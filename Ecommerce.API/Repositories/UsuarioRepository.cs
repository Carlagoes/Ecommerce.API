﻿using Ecommerce.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private IDbConnection _connection;

        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EcommerceAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public List<Usuario> Get()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * FROM Usuarios";
                command.Connection = (SqlConnection)_connection;
                
                _connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

                //Dapper depois(ORM)
                while (dataReader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = dataReader.GetInt32("Id");
                    usuario.Nome = dataReader.GetString("Nome");
                    usuario.Email = dataReader.GetString("Email");
                    usuario.Sexo = dataReader.GetString("Sexo");
                    usuario.RG = dataReader.GetString("RG");
                    usuario.CPF = dataReader.GetString("CPF");
                    usuario.NomeMae = dataReader.GetString("NomeMae");
                    usuario.SituacaoCadastro = dataReader.GetString("SituacaoCadastro");
                    usuario.DataCadastro = dataReader.GetDateTimeOffset(8);

                    usuarios.Add(usuario);

                }
            }
            finally 
            {
                _connection.Close();
            }

            return usuarios;
        }
        public Usuario Get(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = $"SELECT * FROM Usuarios u " +
                    $"LEFT JOIN Contatos c " +
                    $"ON c.UsuarioId = u.Id WHERE u.Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                command.Connection = (SqlConnection)_connection;

                _connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = dataReader.GetInt32(0);
                    usuario.Nome = dataReader.GetString("Nome");
                    usuario.Email = dataReader.GetString("Email");
                    usuario.Sexo = dataReader.GetString("Sexo");
                    usuario.RG = dataReader.GetString("RG");
                    usuario.CPF = dataReader.GetString("CPF");
                    usuario.NomeMae = dataReader.GetString("NomeMae");
                    usuario.SituacaoCadastro = dataReader.GetString("SituacaoCadastro");
                    usuario.DataCadastro = dataReader.GetDateTimeOffset(8);

                    Contato contato = new Contato();
                    contato.Id = dataReader.GetInt32(9);
                    contato.UsuarioId = usuario.Id;
                    contato.Telefone = dataReader.GetString("Telefone");
                    contato.Celular = dataReader.GetString("Celular");

                    usuario.Contato = contato;

                    return usuario;
                }

            }
            finally 
            {
                _connection.Close();
                
            }
            return null;
        }
        public void Insert(Usuario usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "INSERT INTO Usuarios(Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) " +
                    "VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro);" +
                    "SELECT CAST(scope_identity() AS int)";
                command.Connection = (SqlConnection)_connection;

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@RG", usuario.RG);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@NomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@SituacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@DataCadastro", usuario.DataCadastro);

                _connection.Open();
                usuario.Id = (int)command.ExecuteScalar();
            }
            finally 
            {
                _connection.Close();
            }

 
        }
        public void Update(Usuario usuario)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "UPDATE Usuarios SET " +
                    "Nome = @Nome, Email = @Email, Sexo = @Sexo, " +
                    "RG = @RG, CPF = @CPF, NomeMae = @NomeMae, " +
                    "SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro " +
                    "WHERE Id = @Id";
                command.Connection = (SqlConnection)_connection;

                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                command.Parameters.AddWithValue("@RG", usuario.RG);
                command.Parameters.AddWithValue("@CPF", usuario.CPF);
                command.Parameters.AddWithValue("@NomeMae", usuario.NomeMae);
                command.Parameters.AddWithValue("@SituacaoCadastro", usuario.SituacaoCadastro);
                command.Parameters.AddWithValue("@DataCadastro", usuario.DataCadastro);

                command.Parameters.AddWithValue("@Id", usuario.Id);

                _connection.Open();
                command.ExecuteNonQuery();
            }
            finally 
            {
                _connection.Close();
            }
        }
        public void Delete(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "DELETE FROM Usuarios WHERE Id = @Id";
                command.Connection = (SqlConnection)_connection;
                command.Parameters.AddWithValue("@Id", id);

                _connection.Open();
                command.ExecuteNonQuery();
            }
            finally 
            {
                _connection.Close();
               
            }
        }

       
    }
}

