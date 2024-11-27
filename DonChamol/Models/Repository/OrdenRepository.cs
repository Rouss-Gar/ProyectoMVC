using DonChamol.Models;
using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

public class OrdenRepository : IOrdenRepository<Orden>
{
    // Obtener lista de clientes
    public List<Cliente> GetAllCliente()
    {
        List<Cliente> ListClientes = new List<Cliente>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllClientes", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ListClientes.Add(new Cliente
                    {
                        id_Cliente = Convert.ToInt32(dataReader["id_Cliente"]),
                        Nombre = dataReader["Nombre"].ToString()
                    });
                }
                dataReader.Close();
            }
            catch
            {
                return new List<Cliente>();
            }
        }

        return ListClientes;
    }
    public List<GetAllOrden> GetAllOrden()
    {
        List<GetAllOrden> ordenes = new List<GetAllOrden>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllOrden", connection);  // Asume que tienes un procedimiento almacenado
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ordenes.Add(new GetAllOrden
                    {
                        id_Menu = Convert.ToInt32(dataReader["id_Menu"]),
                        Nombre = dataReader["Nombre"].ToString(),
                        Descripcion = dataReader["Descripcion"].ToString(),
                        Precio = Convert.ToDecimal(dataReader["Precio"])
                    });
                }

                dataReader.Close();
            }
            catch
            {
                return new List<GetAllOrden>();
            }
        }

        return ordenes;
    }


    // Obtener lista de meseros
    public List<Meseros> GetAllMeseros()
    {
        List<Meseros> ListMeseros = new List<Meseros>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllMeseros", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ListMeseros.Add(new Meseros
                    {
                        id_Mesero = Convert.ToInt32(dataReader["id_Mesero"]),
                        Nombre = dataReader["Nombre"].ToString()
                    });
                }
                dataReader.Close();
            }
            catch
            {
                return new List<Meseros>();
            }

        }

        return ListMeseros;
    }

    // Obtener lista de mesas
    public List<Mesas> GetAllMesas()
    {
        List<Mesas> ListMesas = new List<Mesas>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllMesas", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    ListMesas.Add(new Mesas
                    {
                        id_Mesa = Convert.ToInt32(dataReader["id_Mesa"]),
                        Numero_Mesa = Convert.ToInt32(dataReader["Numero_Mesa"])
                    });
                }
                dataReader.Close();
            }
            catch
            {
                return new List<Mesas>();
            }
        }

        return ListMesas;
    }

    // Obtener lista de elementos del menú
    public List<MenuItems> GetAllMenuItems()
    {
        List<MenuItems> listMenuItems = new List<MenuItems>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllMenuItems", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    listMenuItems.Add(new MenuItems()
                    {
                        id_Menu = dataReader["id_Menu"] != DBNull.Value ? Convert.ToInt32(dataReader["id_Menu"]) : 0,  // Verifica si es nulo
                        Nombre = dataReader["Nombre"] as string ?? string.Empty,  // Si es nulo, asigna cadena vacía
                        Descripcion = dataReader["Descripcion"] as string ?? string.Empty,  // Si es nulo, asigna cadena vacía
                        Precio = dataReader["Precio"] != DBNull.Value ? Convert.ToDecimal(dataReader["Precio"]) : 0m,  // Si es nulo, asigna 0
                    });
                }
                dataReader.Close();
            }
            catch
            {
                return new List<MenuItems>();
            }

        }

        return listMenuItems;
    }

    // Insertar orden
    public bool InsertOrden(Orden orden, List<OrdenDetalle> ordenDetalles)
    {
        bool result = false;

        try
        {
            using (SqlConnection conn = new SqlConnection(BDConnection.Connection()))
            {
                SqlCommand cmd = new SqlCommand("InsertNewOrden", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Cliente", orden.id_Cliente);
                cmd.Parameters.AddWithValue("@id_Mesero", orden.id_Mesero);
                cmd.Parameters.AddWithValue("@id_Mesa", orden.id_Mesa);
                cmd.Parameters.AddWithValue("@id_Menu", orden.id_Menu); // Aquí agregamos el id_Menu
                cmd.Parameters.AddWithValue("@Fecha_Orden", orden.Fecha_Orden);
                cmd.Parameters.AddWithValue("@Total", orden.Total);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                result = rowsAffected > 0;
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores (puedes registrar el error si es necesario).
            Console.WriteLine($"Error al insertar la orden: {ex.Message}");
        }

        return result;
    }


    public bool DeleteOrdenById(int idOrden)
    {
        bool resultado = false; // Inicializa el resultado en false

        try
        {
            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                using (SqlCommand command = new SqlCommand("[dbo].[USP_EliminarOrden]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar el parámetro de entrada
                    command.Parameters.AddWithValue("@id_Orden", idOrden);

                    // Agregar el parámetro de salida
                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    // Abrir la conexión
                    connection.Open();

                    // Ejecutar el comando
                    command.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    resultado = Convert.ToBoolean(resultParam.Value);
                }
            }
        }
        catch (SqlException ex)
        {
            // Manejar errores específicos de SQL
            Console.WriteLine($"Error SQL: {ex.Message}");
            resultado = false;
        }
        catch (Exception ex)
        {
            // Manejar errores generales
            Console.WriteLine($"Error: {ex.Message}");
            resultado = false;
        }

        return resultado;
    }






    List<Orden> IOrdenRepository<Orden>.GetAllOrden()
    {
        throw new NotImplementedException();
    }

    public Orden GetOrdenById(int id)
    {
        throw new NotImplementedException();
    }

    public bool InsertNewOrden(Orden orden)
    {
        throw new NotImplementedException();
    }

    public bool UpdateOrden(Orden orden)
    {
        throw new NotImplementedException();
    }

}