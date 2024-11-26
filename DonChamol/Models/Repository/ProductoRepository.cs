
using DonChamol.Models.Data;
using System.Data.SqlClient;
using System.Data;

namespace DonChamol.Models.Repository
{
    public class ProductoRepository : IProducto<Producto>
    {
        public List<Producto> GetAll()
        {
            List<Producto> listProductos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllProducts", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            listProductos.Add(new Producto()
                            {
                                id_producto = Convert.ToInt32(dataReader["id_producto"]),
                                nombre_producto = dataReader["nombre_producto"].ToString(),
                                descripcion = dataReader["descripcion"].ToString(),
                                fecha_vencimiento = dataReader["fecha_vencimiento"] as DateTime?,
                                estado = Convert.ToBoolean(dataReader["estado"])
                            });
                        }
                    }
                    return listProductos;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los productos: " + ex.Message, ex);
                }
            }
        }

        public Producto GetById(int idProducto)
        {
            Producto producto = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetProductById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            producto = new Producto()
                            {
                                id_producto = Convert.ToInt32(dataReader["id_producto"]),
                                id_Categoria = Convert.ToInt32(dataReader["id_Categoria"]),
                                id_proveedor = Convert.ToInt32(dataReader["id_proveedor"]),
                                nombre_producto = dataReader["nombre_producto"].ToString(),
                                descripcion = dataReader["descripcion"].ToString(),
                                fecha_vencimiento = dataReader["fecha_vencimiento"] as DateTime?,
                                estado = Convert.ToBoolean(dataReader["estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el producto: " + ex.Message, ex);
                }
            }
            return producto;
        }


        public bool Update(Producto producto)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_producto", producto.id_producto);
                    cmd.Parameters.AddWithValue("@nombre_producto", producto.nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", producto.descripcion);
                    cmd.Parameters.AddWithValue("@fecha_vencimiento", producto.fecha_vencimiento.HasValue ? (object)producto.fecha_vencimiento.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("estado", producto.estado);
                    cmd.Parameters.AddWithValue("@id_Categoria", producto.id_Categoria);
                    cmd.Parameters.AddWithValue("@id_proveedor", producto.id_proveedor);

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
                    throw new Exception("Error al actualizar el producto", ex);
                }
            }
            return result;
        }


        public bool Delete(Producto model)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_producto", model.id_producto);

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
                    throw new Exception("Error al eliminar el producto", ex);
                }
            }
            return result;
        }

        public bool Insert(Producto producto)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewProduct", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_producto", producto.nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", producto.descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_vencimiento", producto.fecha_vencimiento.HasValue ? (object)producto.fecha_vencimiento.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("estado", producto.estado);
                    cmd.Parameters.AddWithValue("@id_Categoria", producto.id_Categoria);
                    cmd.Parameters.AddWithValue("@id_proveedor", producto.id_proveedor);

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
                    throw new Exception("Error al insertar el producto", ex);
                }
            }
            return result;
        }
    }
}
