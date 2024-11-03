using DonChamol.Models.Data;
using System.Data;
using System.Data.SqlClient;

namespace DonChamol.Models.Repository
{
    public class CategoriaRepository : ICategoriaRepository<Categoria>
    {
        public List<Categoria> GetAllCategoria()
        {
            List<Categoria> listCategorias = new List<Categoria>();

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetAllCategoria", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        listCategorias.Add(new Categoria()
                        {
                            id_Categoria = Convert.ToInt32(dataReader["id_Categoria"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Descripcion = dataReader["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dataReader["Estado"]) // Añadir el campo Estado
                        });
                    }
                    return listCategorias;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Categoria GetCategoriaById(int id)
        {
            Categoria categoria = null;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetCategoriaById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        categoria = new Categoria()
                        {
                            id_Categoria = Convert.ToInt32(dataReader["id_Categoria"]),
                            Nombre = dataReader["Nombre"].ToString(),
                            Descripcion = dataReader["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dataReader["Estado"]) // Añadir el campo Estado
                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener la categoría", ex);
                }
            }

            return categoria;
        }

        public bool InsertNewCategoria(Categoria categoria)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("InsertNewCategoria", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado); // Añadir el parámetro Estado

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
                    throw new Exception("Error al insertar la categoría", ex);
                }
            }

            return result;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            bool result = false;

            if (categoria != null)
            {
                using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand("UpdateCategoria", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_Categoria", categoria.id_Categoria);
                        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);
                        cmd.Parameters.AddWithValue("@Estado", categoria.Estado); // Añadir el parámetro Estado

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
                        throw new Exception("Error al actualizar la categoría", ex);
                    }
                }
            }
            else
            {
                throw new Exception("La categoría con el ID especificado no existe.");
            }

            return result;
        }

        public bool EditCategoriaByName(Categoria category)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("EditCategoriaByName", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_Categoria", category.id_Categoria);
                    cmd.Parameters.AddWithValue("@Nombre", category.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", category.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", category.Estado);

                    SqlParameter outputResult = new SqlParameter("@Result", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output,
                    };
                    cmd.Parameters.Add(outputResult);

                    cmd.ExecuteNonQuery();
                    result = Convert.ToBoolean(outputResult.Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar la categoría", ex);
                }
            }

            return result;
        }

        // Implementación del método DeleteCategoryById
        public bool DeleteCategoriaById(int id)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("DeleteCategoriaById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetro de entrada
                    cmd.Parameters.AddWithValue("@id_Categoria", id);

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
                    throw new Exception("Error al eliminar la categoría", ex);
                }
            }

            return result;
        }

    }
}
