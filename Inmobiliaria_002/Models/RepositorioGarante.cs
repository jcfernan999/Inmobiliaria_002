using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{
	public class RepositorioGarante : RepositorioBase, IRepositorio<Garante>
	{
		public RepositorioGarante(IConfiguration configuration) : base(configuration)
		{
			
		}

		public int Alta(Garante p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Garantes (Nombre, Apellido, Dni, Telefono, Email, EstaPublicado,EstaHabilitado) " +
					$"VALUES ('{p.Nombre}', '{p.Apellido}','{p.Dni}','{p.Telefono}','{p.Email}' ,'1','1')";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.GaranteId = Convert.ToInt32(id);
                    connection.Close();
                }
			}
			return res;
		}
		public int Baja(int id)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Garantes WHERE GaranteId = {id}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(Garante p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Garantes SET Nombre='{p.Nombre}', Apellido='{p.Apellido}', Dni='{p.Dni}', Telefono='{p.Telefono}', Email='{p.Email}' , EstaPublicado='{p.EstaPublicado}', EstaHabilitado='{p.EstaHabilitado}'" +
					$" WHERE GaranteId = {p.GaranteId}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

		public IList<Garante> ObtenerTodos()
		{
			IList<Garante> res = new List<Garante>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT GaranteId, Nombre, Apellido, Dni, Telefono, Email, EstaPublicado, EstaHabilitado" +
                    $" FROM Garantes";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
                        Garante p = new Garante
                        {
                            GaranteId = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
                            EstaPublicado = reader.GetBoolean(6),
                            EstaHabilitado = reader.GetBoolean(7),
                        };
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Garante ObtenerPorId(int id)
		{
			Garante p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT GaranteId, Nombre, Apellido, Dni, Telefono, Email, EstaPublicado, EstaHabilitado FROM Garantes" +
					$" WHERE GaranteId=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						p = new Garante
						{
                            GaranteId = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
                            EstaPublicado = reader.GetBoolean(6),
                            EstaHabilitado = reader.GetBoolean(7),
                        };
						return p;
					}
					connection.Close();
				}
			}
			return p;
		}
	}
}
