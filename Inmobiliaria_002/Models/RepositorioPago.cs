using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{


    public class RepositorioPago : RepositorioBase, IRepositorioPago
    	{
		public RepositorioPago(IConfiguration configuration) : base(configuration)
		{
			
		}
 

        public int Alta(Pago p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"INSERT INTO Pagos (Numero, Fecha, Importe, AlquilerId) " +
					$"VALUES ('{p.Numero}', '{p.Fecha}','{p.Importe}','{p.AlquilerId}')";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.PagoId = Convert.ToInt32(id);
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
				string sql = $"DELETE FROM Pagos WHERE PagoId = {id}";
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
		public int Modificacion(Pago p)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Pagos SET Numero='{p.Numero}', Fecha='{p.Fecha}', Importe='{p.Importe}', AlquilerId='{p.AlquilerId}'" +
					$" WHERE PagoId = {p.PagoId}";
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

        public IList<Pago> ObtenerTodos()
        {
            IList<Pago> res = new List<Pago>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT p.PagoId, p.Numero, p.Fecha, p.Importe, p.AlquilerId" +
                    $" FROM Pagos p INNER JOIN Alquileres a ON p.AlquilerId = a.AlquilerId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago entidad = new Pago
                        {
                            PagoId = reader.GetInt32(0),
                            Numero = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Importe = reader.GetString(3),
                            AlquilerId = reader.GetInt32(4),
                            //miAlquiler = new Alquiler
                            //{
                            //    AlquilerId = reader.GetInt32(0),
                            //    Descripcion = reader.GetString(1),
                            //    FechaAlta = reader.GetDateTime(2),
                            //    FechaBaja = reader.GetDateTime(3),
                            //    Monto = reader.GetString(4),
                            //    InmuebleId = reader.GetInt32(5),
                            //    InquilinoId = reader.GetInt32(6),
                            //    GaranteId = reader.GetInt32(7)
                            //}
                        };
                        res.Add(entidad);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        //public IList<Pago> ObtenerTodos()
        //{
        //	IList<Pago> res = new List<Pago>();
        //	using (SqlConnection connection = new SqlConnection(connectionString))
        //	{
        //		string sql = $"SELECT PagoId, Numero, Fecha, Importe, AlquilerId" +
        //                  $" FROM Pagos";
        //		using (SqlCommand command = new SqlCommand(sql, connection))
        //		{
        //			command.CommandType = CommandType.Text;
        //			connection.Open();
        //			var reader = command.ExecuteReader();
        //			while (reader.Read())
        //			{
        //                      Pago p = new Pago
        //                      {
        //                          PagoId = reader.GetInt32(0),
        //					Numero = reader.GetString(1),
        //					Fecha = reader.GetDateTime(2),
        //					Importe = reader.GetString(3),
        //					AlquilerId = reader.GetInt32(4),
        //                      };
        //				res.Add(p);
        //			}
        //			connection.Close();
        //		}
        //	}
        //	return res;
        //}

        public Pago ObtenerPorId(int id)
		{
			Pago p = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT PagoId, Numero, Fecha, Importe, AlquilerId FROM Pagos" +
					$" WHERE PagoId=@id";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						p = new Pago
						{
                            PagoId = reader.GetInt32(0),
                            Numero = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Importe = reader.GetString(3),
                            AlquilerId = reader.GetInt32(4),
                        };
						return p;
					}
					connection.Close();
				}
			}
			return p;
		}

        public IList<Pago> BuscarPorAlquileres(int PagoId)
        {
            List<Pago> res = new List<Pago>();
            Pago entidad = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT p.PagoId, p.Numero, p.Fecha, p.Importe, p.AlquilerId" +
                    $" FROM Pagos p INNER JOIN Alquileres a ON p.AlquilerId = a.AlquilerId" +
                    $" WHERE p.PagoId=@PagoId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@Pago", SqlDbType.Int).Value = PagoId;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        entidad = new Pago
                        {
                            PagoId = reader.GetInt32(0),
                            Numero = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            Importe = reader.GetString(3),
                            AlquilerId = reader.GetInt32(4),
                            miAlquiler = new Alquiler
                            {
                                AlquilerId = reader.GetInt32(0),
                                FechaAlta = reader.GetDateTime(1),
                                FechaBaja = reader.GetDateTime(2),
                                Monto = reader.GetString(3),
                                InmuebleId = reader.GetInt32(4),
                                InquilinoId = reader.GetInt32(5),
                                
                            }
                        };
                        res.Add(entidad);
                    }
                    connection.Close();
                }
            }
            return res;
        }
    }
}
