using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

namespace DonChamol.Models.Repository
{
    public class PagoRepository : IPagoRepository<Pago>
    {
        public List<Pago> GetAllPago()
        {
            List<Pago> listPago = new List<Pago>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllPago", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listPago.Add(new Pago()
                        {
                            id_Pago = Convert.ToInt32(dataReader["id_Pago"]),
                            id_Orden = dataReader["id_Orden"].ToString(),
                            Monto = Convert.ToDecimal(dataReader["Monto"]),
                            MetodoPago = dataReader["MetodoPago"].ToString(),
                            Fecha_Pago = Convert.ToDateTime(dataReader["Fecha_Pago"])
                        });
                    }
                    return listPago;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Pago GetPagoById(int id)
        {
            Pago Pago = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetPagoById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Pago", id);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        Pago = new Pago()
                        {
                            id_Pago = Convert.ToInt32(dataReader["id_Pago"]),
                            id_Orden = dataReader["id_Orden"].ToString(),
                            Monto = Convert.ToDecimal(dataReader["Monto"]),
                            MetodoPago = dataReader["MetodoPago"].ToString(),
                            Fecha_Pago = Convert.ToDateTime(dataReader["Fecha_Pago"])
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el pago", ex);
                }
            }

            return Pago;
        }

        public bool InsertNewPago(Pago Pago)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertNewPago", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agrega solo los parámetros necesarios
                    cmd.Parameters.AddWithValue("@id_Orden", Pago.id_Orden);
                    cmd.Parameters.AddWithValue("@Monto", Pago.Monto);
                    cmd.Parameters.AddWithValue("@MetodoPago", Pago.MetodoPago);
                    cmd.Parameters.AddWithValue("@Fecha_Pago", Pago.Fecha_Pago);

                    // Parámetro de salida
                    SqlParameter outputParam = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(outputParam.Value);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error al insertar el pago: " + sqlEx.Message, sqlEx);
                }
            }

            return result;
        }



        public bool UpdatePago(Pago pago)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("UpdatePago", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Asegúrate de que estos son los mismos parámetros que el procedimiento espera
                    cmd.Parameters.AddWithValue("@id_Pago", pago.id_Pago);
                    cmd.Parameters.AddWithValue("@id_Orden", pago.id_Orden);
                    cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                    cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                    cmd.Parameters.AddWithValue("@Fecha_Pago", pago.Fecha_Pago);

                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error al actualizar el pago: " + sqlEx.Message, sqlEx);
                }
            }

            return result;
        }


        public bool DeletePagoById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeletePagoById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Solo agrega el parámetro requerido
                    cmd.Parameters.AddWithValue("@id_Pago", id);

                    cmd.ExecuteNonQuery();
                    result = true;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Error al eliminar el pago: " + sqlEx.Message, sqlEx);
                }
            }

            return result;
        }
    }
}