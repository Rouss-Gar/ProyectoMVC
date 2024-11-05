using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace DonChamol.Models.Repository
{
    public class MeserosRepository : IMeserosRepository<Meseros>
    {
        public List<Meseros> GetAllMeseros()
        {
            List<Meseros> listMeseros = new List<Meseros>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllMeseros", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listMeseros.Add(new Meseros()
                        {
                            id_Mesero = Convert.ToInt32(dataReader["id_Mesero"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString(),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                    return listMeseros;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Meseros GetMeseroById(int id)
        {
            Meseros meseros = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMeseroById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Mesero", id);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        meseros = new Meseros()
                        {
                            id_Mesero = Convert.ToInt32(dataReader["id_Mesero"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el mesero", ex);
                }
            }
            return meseros;
        }

        public bool InsertNewMesero(Meseros mesero)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertNewMesero", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Solo añade los parámetros que el procedimiento almacenado espera
                    cmd.Parameters.AddWithValue("@Nombre", mesero.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", mesero.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", mesero.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion", mesero.Direccion);
                    cmd.Parameters.AddWithValue("@Estado", mesero.Estado);

                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("Error al insertar el mesero: " + sqlEx.Message, sqlEx);
                }
            }

            return result;
        }


        public bool UpdateMesero(Meseros mesero)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UpdateMesero", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id_Mesero", mesero.id_Mesero);
                    cmd.Parameters.AddWithValue("@Nombre", mesero.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", mesero.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", mesero.Telefono);
                    cmd.Parameters.AddWithValue("@Direccion", mesero.Direccion);
                    cmd.Parameters.AddWithValue("@Estado", mesero.Estado);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    result = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar el mesero", ex);
                }
            }
            return result;
        }



        public bool DeleteMeseroById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteMeseroById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@Id_Mesero", id);

                    // Ejecutar el comando
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Verificar si se eliminó al menos una fila
                    result = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el mesero", ex);
                }
            }

            return result;
        }



        public bool ToggleEstado(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ToggleMeseroEstado", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Mesero", id);

                    SqlParameter outputResult = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputResult);

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(outputResult.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cambiar el estado del mesero", ex);
                }
            }

            return result;
        }

        public string? GetMeseroByName(string nombre)
        {
            throw new NotImplementedException();
        }
    }
}
