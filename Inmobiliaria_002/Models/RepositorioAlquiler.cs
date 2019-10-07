using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria_002.Models
{
    public class RepositorioAlquiler : RepositorioBase, IRepositorioAlquiler
    {
        public RepositorioAlquiler(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Alquiler entidad)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                 
        string sql = $"INSERT INTO Alquileres ( Descripcion,FechaAlta , FechaBaja , Monto , InmuebleId , InquilinoId) " +
                    $"VALUES ('{entidad.Descripcion}','{entidad.FechaAlta}', '{entidad.FechaBaja}','{entidad.Monto}','{entidad.InmuebleId}','{entidad.InquilinoId}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    entidad.AlquilerId = Convert.ToInt32(id);
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
                string sql = $"DELETE FROM Alquileres WHERE AlquilerId = {id}";
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
        public int Modificacion(Alquiler alquiler)
        { 
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Alquileres SET Descripcion=@Descripcion, FechaAlta=@FechaAlta, FechaBaja=@FechaBaja, Monto=@Monto, InmuebleId=@InmuebleId, InquilinoId=@InquilinoId " +
                    $"WHERE Id = {alquiler.AlquilerId}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = alquiler.Descripcion;
                    command.Parameters.Add("@FechaAlta", SqlDbType.DateTime).Value = alquiler.FechaAlta;
                    command.Parameters.Add("@FechaBaja", SqlDbType.DateTime).Value = alquiler.FechaBaja;
                    command.Parameters.Add("@Monto", SqlDbType.VarChar).Value = alquiler.Monto;
                    command.Parameters.Add("@InmuebleId", SqlDbType.Int).Value = alquiler.InmuebleId;
                    command.Parameters.Add("@InquilinoId", SqlDbType.Int).Value = alquiler.InquilinoId;
                
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Alquiler> ObtenerTodos()
        {
            IList<Alquiler> res = new List<Alquiler>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT AlquilerId, Descripcion, FechaAlta , FechaBaja , Monto , InmuebleId , InquilinoId FROM Alquileres";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Alquiler entidad = new Alquiler
                        {
                            AlquilerId = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            FechaAlta = reader.GetDateTime(2),
                            FechaBaja = reader.GetDateTime(3),
                            Monto = reader.GetString(4),
                            InmuebleId = reader.GetInt32(5),
                            InquilinoId = reader.GetInt32(6),
                             
                        };
                        res.Add(entidad);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Alquiler ObtenerPorId(int id)
        {
            Alquiler entidad = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"AlquilerId, Descripcion, FechaAlta , FechaBaja , Monto , InmuebleId , InquilinoId FROM Alquileres" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        entidad = new Alquiler
                        {
                            AlquilerId = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            FechaAlta = reader.GetDateTime(2),
                            FechaBaja = reader.GetDateTime(3),
                            Monto = reader.GetString(4),
                            InmuebleId = reader.GetInt32(5),
                            InquilinoId = reader.GetInt32(6),
                            
                        };
                    }
                    connection.Close();
                }
            }
            return entidad;
        }

     
    }
}
