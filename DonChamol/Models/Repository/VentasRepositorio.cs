using DonChamol.Models.Data;
using DonChamol.Models.Dto;
using System.Data.SqlClient;
using System.Data;

namespace DonChamol.Models.Repository
{
    public class VentasRepositorio : IVentasRepositorio
    {
        // Obtener productos
        public List<ProductoDto> ObtenerProductos()
        {
            List<ProductoDto> productos = new List<ProductoDto>();

            using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("USP_GetAllProducts", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        productos.Add(new ProductoDto
                        {
                            id_producto = Convert.ToInt32(dataReader["id_producto"].ToString()),
                            nombre_producto = dataReader["nombre_producto"].ToString(),

                        });
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener productos", ex);
                }

                return productos;
            }
        }

        // Obtener clientes
        public List<ClienteDto> ObtenerClientes()
        {
            List<ClienteDto> listaClientes = new List<ClienteDto>();

            using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("GetAllClientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conexion.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listaClientes.Add(new ClienteDto
                        {
                            id_Cliente = Convert.ToInt32(dataReader["id_cliente"].ToString()),
                            Nombre = dataReader["nombre"].ToString(),
                        });
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener clientes", ex);
                }
            }

            return listaClientes;
        }


        // Insertar venta
        public bool InsertarVenta(Venta venta, List<DetalleVenta> detallesVenta)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
                {
                    SqlCommand cmd = new SqlCommand("USP_InsertarVenta", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Cliente", venta.id_Cliente);
                    cmd.Parameters.AddWithValue("@FechaVenta", venta.FechaVenta);
                    cmd.Parameters.AddWithValue("@id_Mesero", venta.id_Mesero);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    DataTable tablaDetalles = new DataTable();
                    tablaDetalles.Columns.Add("id_producto", typeof(int));
                    tablaDetalles.Columns.Add("Cantidad", typeof(int));
                    tablaDetalles.Columns.Add("PrecioUnitario", typeof(decimal));

                    foreach (var detalle in detallesVenta)
                    {
                        tablaDetalles.Rows.Add(detalle.id_producto, detalle.Cantidad, detalle.PrecioUnitario);
                    }

                    SqlParameter detallesParam = cmd.Parameters.AddWithValue("@DetallesVenta", tablaDetalles);
                    detallesParam.SqlDbType = SqlDbType.Structured;
                    detallesParam.TypeName = "dbo.DetallesVentaType";

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la venta", ex);
            }
        }

        // Obtener ventas
        public IEnumerable<RegistroVenta> ObtenerVentas()
        {
            var ventas = new Dictionary<int, RegistroVenta>();

            using (SqlConnection conexion = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand comando = new SqlCommand("USP_ObtenerVentas", conexion);
                comando.CommandType = CommandType.StoredProcedure;

                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        int ventaID = (int)lector["VentaID"];
                        if (!ventas.ContainsKey(ventaID))
                        {
                            var venta = new RegistroVenta
                            {
                                VentaID = ventaID,
                                id_Cliente = (int)lector["id_cliente"],
                                Nombre = lector["ClienteNombre"].ToString(),
                                FechaVenta = (DateTime)lector["FechaVenta"],
                                MontoTotal = (decimal)lector["MontoTotal"],
                                DetallesVenta = new List<RegistroDetalleVenta>()
                            };
                            ventas[ventaID] = venta;
                        }

                        var detalle = new RegistroDetalleVenta
                        {
                            DetalleVentaID = (int)lector["DetalleVentaID"],
                            id_producto = (int)lector["id_producto"],
                            nombre_producto = lector["nombre_producto"].ToString(),
                            Cantidad = (int)lector["Cantidad"],
                            PrecioUnitario = (decimal)lector["PrecioUnitario"],
                            TotalLinea = (decimal)lector["TotalLinea"]
                        };
                        ventas[ventaID].DetallesVenta.Add(detalle);
                    }
                }
            }

            return ventas.Values;
        }
    }
}
