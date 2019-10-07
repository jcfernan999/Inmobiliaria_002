using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{

    public class RepositorioInmueble : RepositorioBase, IRepositorioInmueble
    {
         
		public RepositorioInmueble(IConfiguration configuration) : base(configuration)
		{
			
		}

		public int Alta(Inmueble p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Inmuebles (Direccion, Ambientes, Superficie, Latitud, Longitud,PropietarioId,EstaPublicado,EstaHabilitado) " +
					$"VALUES ('{p.Direccion}', '{p.Ambientes}','{p.Superficie}','{p.Latitud}','{p.Longitud}','10','1','1')";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.InmuebleId = Convert.ToInt32(id);
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
				string sql = $"DELETE FROM Inmuebles WHERE InmuebleId = {id}";
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
		public int Modificacion(Inmueble p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{ 

 string sql = $"UPDATE Inmuebles SET Direccion='{p.Direccion}', Ambientes='{p.Ambientes}', Superficie='{p.Superficie}', Latitud='{p.Latitud}', Longitud='{p.Longitud}', PropietarioId='1' , EstaPublicado='{p.EstaPublicado}', EstaHabilitado='{p.EstaHabilitado}'" +
					$" WHERE inmuebleId = {p.InmuebleId}";
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

		public IList<Inmueble> ObtenerTodos()
		{
			IList<Inmueble> res = new List<Inmueble>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT inmuebleId,Direccion, Ambientes, Superficie, Latitud, Longitud,PropietarioId,EstaPublicado,EstaHabilitado" +
                    $" FROM Inmuebles";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
                        Inmueble p = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(0),
							Direccion = reader.GetString(1),
							Ambientes = reader.GetInt32(2),
							Superficie = reader.GetInt32(3),
							Latitud = reader.GetString(4),
							Longitud = reader.GetString(5),
                            PropietarioId = reader.GetInt32(6),
                            EstaPublicado = reader.GetBoolean(7),
                            EstaHabilitado = reader.GetBoolean(8),
                        };
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Inmueble ObtenerPorId(int id)
		{
			Inmueble p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT inmuebleId,Direccion, Ambientes, Superficie, Latitud, Longitud,PropietarioId,EstaPublicado,EstaHabilitado" +
					$" FROM Inmuebles WHERE inmuebleId=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
                        p = new Inmueble
                        {
                            InmuebleId = reader.GetInt32(0),
                            Direccion = reader.GetString(1),
                            Ambientes = reader.GetInt32(2),
                            Superficie = reader.GetInt32(3),
                            Latitud = reader.GetString(4),
                            Longitud = reader.GetString(5),
                            PropietarioId = reader.GetInt32(6),
                            EstaPublicado = reader.GetBoolean(7),
                            EstaHabilitado = reader.GetBoolean(8),
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
