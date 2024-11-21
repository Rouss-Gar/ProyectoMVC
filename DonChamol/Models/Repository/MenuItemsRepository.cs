using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

namespace DonChamol.Models.Repository
{
    public class MenuItemsRepository : IMenuItemsRepository<MenuItems>
    {
        public List<MenuItems> GetAllMenuItems()
        {
            List<MenuItems> listMenuItems = new List<MenuItems>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllMenuItems", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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
                    return listMenuItems;
                }
                catch (Exception ex)
                {
                    // Aquí podrías registrar el error o agregar detalles adicionales si es necesario
                    throw new Exception("Error al obtener el item del menú", ex);
                }

            }
        }



        public MenuItems? GetMenuItemById(int id)
        {
            MenuItems? menuItems = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetMenuItemById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Menu", id);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        menuItems = new MenuItems()
                        {
                            id_Menu = Convert.ToInt32(dataReader["id_Menu"]),
                            Nombre = dataReader["Nombre"]?.ToString() ?? string.Empty,
                            Descripcion = dataReader["Descripcion"]?.ToString() ?? string.Empty,
                            Precio = Convert.ToDecimal(dataReader["Precio"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        };

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener el item del menú", ex);
                }
            }

            return menuItems;
        }


        public bool InsertNewMenuItem(MenuItems menuItems)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();

                    // Verificar si el id_Categoria existe en la tabla Categoria
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Categoria WHERE id_Categoria = @id_Categoria", connection);
                    checkCmd.Parameters.AddWithValue("@id_Categoria", menuItems.id_Categoria);
                    int categoryExists = (int)checkCmd.ExecuteScalar();

                    if (categoryExists == 0)
                    {
                        throw new Exception("El id_Categoria especificado no existe en la tabla Categoria.");
                    }

                    // Insertar nuevo item en la tabla MenuItems
                    SqlCommand cmd = new SqlCommand("InsertNewMenuItem", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", menuItems.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", menuItems.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", menuItems.Precio);
                    cmd.Parameters.AddWithValue("@Estado", menuItems.Estado);
                    cmd.Parameters.AddWithValue("@id_Categoria", menuItems.id_Categoria);

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
                    throw new Exception("Error en la base de datos al insertar el elemento de menú", sqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar el item del menú", ex);
                }
            }

            return result;
        }

        public bool EditMenuItem(MenuItems menuItems)
        {
            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("usp_UpdateMenuItemById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Menu", menuItems.id_Menu);
                    cmd.Parameters.AddWithValue("@Nombre", menuItems.Nombre ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Descripcion", menuItems.Descripcion ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Precio", menuItems.Precio);
                    cmd.Parameters.AddWithValue("@Estado", menuItems.Estado);
                    cmd.Parameters.AddWithValue("@id_Categoria", menuItems.id_Categoria);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar el item del menú", ex);
                }
            }
        }

        public bool DeleteMenuItemById(int id)
        {
            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteMenuItemById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Menu", id);

                    // Agregar el parámetro de salida
                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Bit);
                    resultParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(resultParam);

                    cmd.ExecuteNonQuery();

                    // Obtener el valor del parámetro de salida
                    bool result = Convert.ToBoolean(resultParam.Value);
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el item del menú", ex);
                }
            }
        }


        public bool ToggleEstado(int id)
        {
            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ToggleMenuItemEstado", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Menu", id);

                    // Agregar el parámetro de salida para obtener el estado actual
                    SqlParameter estadoParam = new SqlParameter("@Estado", SqlDbType.Bit);
                    estadoParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(estadoParam);

                    cmd.ExecuteNonQuery();

                    // Obtener el valor de salida
                    bool estadoActualizado = Convert.ToBoolean(estadoParam.Value);
                    return estadoActualizado;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cambiar el estado del item del menú", ex);
                }
            }
        }





    }
}
