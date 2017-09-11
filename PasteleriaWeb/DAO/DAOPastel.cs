using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using DTO;

namespace DAO
{
    public class DAOPastel
    {
        private SqlConnection conexion;
        

        public DAOPastel()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PasteleriaDBConnectionString"].ConnectionString;

            conexion = new SqlConnection(connectionString);
        }

        public bool InsertarPastel(DTOPastel _DTOPastel)
        {
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("SP_Insert_Pastel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombrePastel", _DTOPastel.Nombre);
                comando.Parameters.AddWithValue("@tamanio", _DTOPastel.Tamanio);
                int resultado = comando.ExecuteNonQuery();

                return (resultado >= 1) ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool CambiarEstadoPastel(DTOPastel _DTOPastel)
        {
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("SP_CambiarEstado_Pastel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPastel", _DTOPastel.IdPastel);
                comando.Parameters.AddWithValue("@estado", !_DTOPastel.Estado);
                int resultado = comando.ExecuteNonQuery();

                return (resultado >= 1) ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool ActualizarPastel(DTOPastel _DTOPastel)
        {
            try
            {
                conexion.Open();

                SqlCommand comando = new SqlCommand("SP_Actualizar_Pastel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@idPastel", _DTOPastel.IdPastel);
                comando.Parameters.AddWithValue("@nombrePastel", _DTOPastel.Nombre);
                comando.Parameters.AddWithValue("@tamanio", _DTOPastel.Tamanio);
                int resultado = comando.ExecuteNonQuery();

                return (resultado >= 1) ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        public DataTable ListarPasteles()
        {
            try
            {

                DataTable listado = new DataTable();

                conexion.Open();

                SqlCommand comando = new SqlCommand("SP_Listar_Pastel", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapatador = new SqlDataAdapter(comando);

                adapatador.Fill(listado);

                return listado;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}
