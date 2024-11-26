using DonChamol.Models.Data;
using System.Data.SqlClient;
using System.Data;

namespace DonChamol.Models.Repository
{
    public class ProveedorRepository:IProveedor<Proveedor>
    {
        public List<Proveedor> GetAll()
        {
            var proveedores = new List<Proveedor>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetAllProveedores", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var proveedor = new Proveedor
                        {
                            id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                            nombre = reader["nombre"].ToString(),
                            apellido = reader["apellido"].ToString(),
                            direccion = reader["direccion"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            correo = reader["correo"].ToString(),
                            estado = Convert.ToBoolean(reader["estado"])
                        };
                        proveedores.Add(proveedor);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener los proveedores", ex);
                }
            }
            return proveedores;
        }

        public Proveedor GetById(int id)
        {
            Proveedor proveedor = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_GetProveedorById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        proveedor = new Proveedor
                        {
                            id_proveedor = Convert.ToInt32(reader["id_proveedor"]),
                            nombre = reader["nombre"].ToString(),
                            apellido = reader["apellido"].ToString(),
                            direccion = reader["direccion"].ToString(),
                            telefono = reader["telefono"].ToString(),
                            correo = reader["correo"].ToString(),
                            estado = Convert.ToBoolean(reader["estado"])
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el proveedor", ex);
                }
            }
            return proveedor;
        }

        public bool Insert(Proveedor proveedor)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_InsertNewProveedor", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre", proveedor.nombre);
                    cmd.Parameters.AddWithValue("@apellido", proveedor.apellido);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    cmd.Parameters.AddWithValue("@correo", proveedor.correo);
                    cmd.Parameters.AddWithValue("@estado", proveedor.estado);

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
                    throw new Exception("Error al insertar el proveedor", ex);
                }
            }
            return result;
        }

        public bool Update(Proveedor proveedor)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_UpdateProveedor", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_proveedor", proveedor.id_proveedor);
                    cmd.Parameters.AddWithValue("@nombre", proveedor.nombre);
                    cmd.Parameters.AddWithValue("@apellido", proveedor.apellido);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    cmd.Parameters.AddWithValue("@correo", proveedor.correo);
                    cmd.Parameters.AddWithValue("@estado", proveedor.estado);

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
                    throw new Exception("Error al actualizar el proveedor", ex);
                }
            }
            return result;
        }

        public bool Delete(Proveedor proveedor)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("USP_DeleteProveedor", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", proveedor.id_proveedor);

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
                    throw new Exception("Error al eliminar el proveedor", ex);
                }
            }
            return result;
        }
    }
}
