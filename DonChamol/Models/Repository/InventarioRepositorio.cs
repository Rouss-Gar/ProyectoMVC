using DonChamol.Models.Data;
using System.Data.SqlClient;
using System.Data;

namespace DonChamol.Models.Repository
{
    public class InventarioRepositorio : IInventarioRepositorio<Inventario>
    {
        public List<Inventario> GetAll()
        {
            List<Inventario> inventarios = new List<Inventario>();

            using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("USP_ObtenerInventario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        inventarios.Add(new Inventario
                        {
                            InventarioID = Convert.ToInt32(reader["InventarioID"]),
                            id_producto = Convert.ToInt32(reader["id_producto"]),
                            nombre_producto = reader["nombre_producto"].ToString(),
                            UnidadesEnStock = Convert.ToInt32(reader["UnidadesEnStock"]),
                            PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"])
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener l", ex);
                }
                return inventarios;
            }
        }

        public decimal? ObtenerPrecioProducto(int idProducto)
        {
            using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("USP_ObtenerPrecioProducto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetro para el procedimiento almacenado
                cmd.Parameters.AddWithValue("@idProducto", idProducto);

                try
                {
                    conexion.Open();
                    var result = cmd.ExecuteScalar();

                    // Si el resultado no es nulo, convertirlo a decimal
                    return result != null ? Convert.ToDecimal(result) : (decimal?)null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el precio del producto", ex);
                }
            }
        }
    }
}
