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
                            id_Menu = Convert.ToInt32(dataReader["id_Menu"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Descripcion = dataReader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(dataReader["Precio"]),
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                    return listMenuItems;
                }
                catch
                {
                    return null;
                }
            }
        }

        public MenuItems GetMenuItemById(int id)
        {
            MenuItems menuItems = null;

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
                            Nombre = dataReader["Nombre"].ToString(),
                            Descripcion = dataReader["Descripcion"].ToString(),
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
                    SqlCommand cmd = new SqlCommand("InsertNewMenuItem", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", menuItems.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", menuItems.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", menuItems.Precio);
                    cmd.Parameters.AddWithValue("@Estado", menuItems.Estado);

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
                    throw new Exception("Error al insertar el item del menú", ex);
                }
            }

            return result;
        }

        public bool UpdateMenuItem(MenuItems menuItems)
        {
            bool result = false;

            if (menuItems != null)
            {
                using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
                {
                    try
                    {
                        connection.Open();

                        // Verificar si el id_Categoria existe en la tabla Categoria
                        SqlCommand checkCmd = new SqlCommand("SELECT 1 FROM Categoria WHERE id_Categoria = @id_Categoria", connection);
                        checkCmd.Parameters.AddWithValue("@id_Categoria", menuItems.id_Categoria);

                        object categoriaExists = checkCmd.ExecuteScalar();

                        if (categoriaExists == null)
                        {
                            // Lanzar una excepción específica si la categoría no existe
                            throw new Exception("El id_Categoria especificado no existe en la tabla Categoria.");
                        }

                        // Procedimiento de actualización si la categoría existe
                        SqlCommand cmd = new SqlCommand("UpdateMenuItem", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_Menu", menuItems.id_Menu);
                        cmd.Parameters.AddWithValue("@Nombre", menuItems.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", menuItems.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", menuItems.Precio);
                        cmd.Parameters.AddWithValue("@Estado", menuItems.Estado);
                        cmd.Parameters.AddWithValue("@id_Categoria", menuItems.id_Categoria);

                        SqlParameter outputResult = new SqlParameter("@Result", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputResult);

                        cmd.ExecuteNonQuery();
                        result = Convert.ToBoolean(outputResult.Value);
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new Exception("Error en la base de datos al actualizar el elemento de menú", sqlEx);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al actualizar el elemento de menú", ex);
                    }
                }
            }
            else
            {
                throw new Exception("El elemento de menú especificado no existe.");
            }

            return result;
        }





        public bool DeleteMenuItemById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteMenuItemById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Menu", id);

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
                    throw new Exception("Error al eliminar el item del menú", ex);
                }
            }

            return result;
        }

        public bool ToggleEstado(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("ToggleEstadoMenuItem", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Menu", id);

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
                    throw new Exception("Error al cambiar el estado del item del menú", ex);
                }
            }

            return result;
        }
    }
}
