using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

namespace DonChamol.Models.Repository
{
    public class MesaRepository : IMesasRepository<Mesas>
    {
        // Método para insertar una nueva mesa
        public bool InsertNewMesa(Mesas mesas)
        {
            bool result = false; // Declara la variable result

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertNewMesa", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros requeridos por el procedimiento almacenado
                    cmd.Parameters.AddWithValue("@Numero_Mesa", mesas.Numero_Mesa);
                    cmd.Parameters.AddWithValue("@Capacidad", mesas.Capacidad);
                    cmd.Parameters.AddWithValue("@Estado", mesas.Estado);

                    SqlParameter outputParam = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(outputParam.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar la mesa", ex);
                }
            }

            return result;
        }

        // Método para obtener todas las mesas
        public List<Mesas> GetAllMesas()
        {
            List<Mesas> mesas = new List<Mesas>(); // Cambia el nombre a 'mesas'

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllMesas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        mesas.Add(new Mesas()
                        {
                            id_Mesa = Convert.ToInt32(dataReader["id_Mesa"]),
                            Numero_Mesa = dataReader["Numero_Mesa"] != DBNull.Value ? Convert.ToInt32(dataReader["Numero_Mesa"]) : 0,
                            Capacidad = Convert.ToInt32(dataReader["Capacidad"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener las mesas", ex);
                }
            }

            return mesas;
        }

        // Método para obtener mesas por nombre
        public List<Mesas> GetByMesaName(string numeroMesa)
        {
            List<Mesas> mesas = new List<Mesas>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetByMesaName", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Numero_Mesa", numeroMesa);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        mesas.Add(new Mesas()
                        {
                            id_Mesa = Convert.ToInt32(dataReader["id_Mesa"]),
                            Numero_Mesa = dataReader["Numero_Mesa"] != DBNull.Value ? Convert.ToInt32(dataReader["Numero_Mesa"]) : 0,
                            Capacidad = Convert.ToInt32(dataReader["Capacidad"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener las mesas por nombre", ex);
                }
            }

            return mesas;
        }

       
        public Mesas GetMesaById(int id)
        {
            Mesas mesas = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMesaById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Mesa", id);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        mesas = new Mesas()
                        {
                            id_Mesa = Convert.ToInt32(dataReader["id_Mesa"]),
                            Numero_Mesa = dataReader["Numero_Mesa"] != DBNull.Value ? Convert.ToInt32(dataReader["Numero_Mesa"]) : 0,
                            Capacidad = Convert.ToInt32(dataReader["Capacidad"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener la mesa", ex);
                }
            }

            return mesas;
        }



        public bool UpdateMesa(Mesas mesa)
        {
            bool result = false;

            if (mesa != null)
            {
                using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateMesa", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_Mesa", mesa.id_Mesa);
                        cmd.Parameters.AddWithValue("@Numero_Mesa", mesa.Numero_Mesa);
                        cmd.Parameters.AddWithValue("@Capacidad", mesa.Capacidad);
                        cmd.Parameters.AddWithValue("@Estado", mesa.Estado);

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
                        throw new Exception("Error al actualizar la mesa", ex);
                    }
                }
            }
            else
            {
                throw new Exception("La mesa con el ID especificado no existe.");
            }

            return result;
        }

        public bool DeleteMesa(int idMesa)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteMesa", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Mesa", idMesa);

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
                    throw new Exception("Error al eliminar la mesa", ex);
                }
            }

            return result;
        }












    }
}
