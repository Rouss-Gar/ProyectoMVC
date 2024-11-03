using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

namespace DonChamol.Models.Repository
{
    public class ClienteRepository : IClienteRepository<Cliente>
    {
        public List<Cliente> GetAllCliente()
        {
            List<Cliente> listClientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllClientes", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listClientes.Add(new Cliente()
                        {
                            id_Cliente = Convert.ToInt32(dataReader["id_Cliente"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            FechaRegistro = Convert.ToDateTime(dataReader["FechaRegistro"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                    return listClientes;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Cliente GetClienteById(int id)
{
    Cliente cliente = null;

    using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
    {
        try
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("GetClienteById", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            // Cambia el nombre del parámetro a @id_Cliente
            cmd.Parameters.AddWithValue("@id_Cliente", id);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                cliente = new Cliente()
                {
                    id_Cliente = Convert.ToInt32(dataReader["id_Cliente"]),
                    Nombre = dataReader["Nombre"].ToString(),
                    Apellido = dataReader["Apellido"].ToString(),
                    Telefono = dataReader["Telefono"].ToString(),
                    Correo = dataReader["Correo"].ToString(),
                    Direccion = dataReader["Direccion"].ToString(),
                    FechaRegistro = Convert.ToDateTime(dataReader["FechaRegistro"]),
                    Estado = Convert.ToBoolean(dataReader["Estado"])
                };
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener el cliente", ex);
        }
    }

    return cliente;
}


        public bool InsertNewCliente(Cliente cliente)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertNewCliente", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro);
                    cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

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
                    // Manejo de excepciones específicas de SQL
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    throw new Exception("Error al insertar el cliente: " + sqlEx.Message, sqlEx);
                }
                catch (Exception ex)
                {
                    // Manejo de otras excepciones
                    Console.WriteLine($"Error: {ex.Message}");
                    throw new Exception("Error al insertar el cliente", ex);
                }
            }

            return result;
        }



        public bool UpdateCliente(Cliente cliente)
        {
            bool result = false;

            if (cliente != null)
            {
                using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("EditclienteById", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_Cliente", cliente.id_Cliente);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                        cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        cmd.Parameters.AddWithValue("@FechaRegistro", cliente.FechaRegistro);
                        cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

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
                        throw new Exception("Error al actualizar el cliente", ex);
                    }
                }
            }
            else
            {
                throw new Exception("El cliente con el ID especificado no existe.");
            }

            return result;
        }


        // Implementación del método GetByClienteName
        public Cliente GetClienteByName(string nombre)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetClienteByName", connection); // Asegúrate de tener este procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", nombre);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        cliente = new Cliente()
                        {
                            id_Cliente = Convert.ToInt32(dataReader["id_Cliente"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            Telefono = dataReader["Telefono"].ToString(),
                            Correo = dataReader["Correo"].ToString(),
                            Direccion = dataReader["Direccion"].ToString(),
                            FechaRegistro = Convert.ToDateTime(dataReader["FechaRegistro"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el cliente por nombre", ex);
                }
            }

            return cliente;
        }

        public bool DeleteClienteById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteClienteById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@id_Cliente", id);

                    // Parámetro de salida
                    SqlParameter outputResult = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputResult);

                    // Ejecutar el comando
                    cmd.ExecuteNonQuery();

                    // Convertir el valor de salida
                    result = outputResult.Value != DBNull.Value && Convert.ToBoolean(outputResult.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el Cliente", ex);
                }
            }

            return result;
        }



    }
}

