using DonChamol.Models.Data;
using DonChamol.Models;
using System.Data;
using System.Data.SqlClient;

public class GetAllOrdenRepository
{
    // Obtener todas las órdenes
    public List<Orden> GetAllOrden()
    {
        List<Orden> listaOrdenes = new List<Orden>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllOrden", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    listaOrdenes.Add(new Orden
                    {
                        id_Orden = Convert.ToInt32(dataReader["id_Orden"]),
                        id_Cliente = Convert.ToInt32(dataReader["id_Cliente"]),
                        id_Mesero = Convert.ToInt32(dataReader["id_Mesero"]),
                        id_Mesa = Convert.ToInt32(dataReader["id_Mesa"]),
                        Fecha_Orden = Convert.ToDateTime(dataReader["Fecha_Orden"]),
                        Total = dataReader["Total"] != DBNull.Value ? Convert.ToDecimal(dataReader["Total"]) : 0m
                    });
                }
                dataReader.Close();
            }
            catch
            {
                listaOrdenes = null;
            }
        }

        return listaOrdenes;
    }

    // Insertar una nueva orden con detalles
    public bool InsertOrden(Orden orden, List<OrdenDetalle> ordenDetalles)
    {
        bool resultado = false;

        try
        {
            using (SqlConnection conn = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("InsertOrden", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Cliente", orden.id_Cliente);
                cmd.Parameters.AddWithValue("@id_Mesero", orden.id_Mesero);
                cmd.Parameters.AddWithValue("@id_Mesa", orden.id_Mesa);
                cmd.Parameters.AddWithValue("@Fecha_Orden", orden.Fecha_Orden);
                cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                // Crear DataTable para detalles de la orden
                DataTable detailsTable = new DataTable();
                detailsTable.Columns.Add("id_Menu", typeof(int));
                detailsTable.Columns.Add("Cantidad", typeof(int));
                detailsTable.Columns.Add("Precio", typeof(decimal));

                foreach (var detalle in ordenDetalles)
                {
                    detailsTable.Rows.Add(detalle.id_Menu, detalle.Cantidad, detalle.Precio);
                }

                SqlParameter detailsParam = cmd.Parameters.AddWithValue("@OrdenDetalles", detailsTable);
                detailsParam.SqlDbType = SqlDbType.Structured;
                detailsParam.TypeName = "dbo.OrdenDetallesType";

                conn.Open();
                cmd.ExecuteNonQuery();

                resultado = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
            }
        }
        catch
        {
            resultado = false;
        }

        return resultado;
    }
}
