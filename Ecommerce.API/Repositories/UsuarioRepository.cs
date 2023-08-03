using Ecommerce.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
                command.CommandText = "SELECT * FROM Usuario";
                command.Connection = (SqlConnection)_connection;
                
                _connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();

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
            return _db.FirstOrDefault(u => u.Id == id);
        }
        public void Insert(Usuario usuario)
        {
            var ultimoUsuario = _db.LastOrDefault();
            if(ultimoUsuario == null)
            {
                usuario.Id = 1;
            }
            else
            {
                usuario.Id = ultimoUsuario.Id;
                usuario.Id++;
            }
            _db.Add(usuario);   
        }
        public void Update(Usuario usuario)
        {
            _db.Remove(_db.FirstOrDefault(u => u.Id == usuario.Id));
            _db.Add(usuario);
        }
        public void Delete(int id)
        {
            _db.Remove(_db.FirstOrDefault(u => u.Id == id));
        }

        //bc
        private static List<Usuario> _db = new List<Usuario>()
        {
            new Usuario(){ Id=1, Nome="Filipe", Email="filipe@gmail.com"},
            new Usuario(){ Id=2, Nome="Marcelo", Email="marcelo@gmail.com"},
            new Usuario(){ Id=3, Nome="Jessica", Email="jessica@gmail.com"},
        
        };
    }
}

