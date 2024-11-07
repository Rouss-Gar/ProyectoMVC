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
                            Nombre = dataReader["Nombre"]?.ToString() ?? string.Empty,  // Asignar cadena vacía si es nulo
                            Descripcion = dataReader["Descripcion"]?.ToString() ?? string.Empty, // Asignar cadena vacía si es nulo
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        });
                    }
                    return listCategorias;
                }
                catch
                {
                    return new List<Categoria>(); // En caso de error, devolver lista vacía
                }
            }
        }



        public Categoria GetCategoriaById(int id)
        {
            Categoria? categoria = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
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
                            Nombre = dataReader["Nombre"]?.ToString() ?? string.Empty,  // Usa cadena vacía si es nulo
                            Descripcion = dataReader["Descripcion"]?.ToString() ?? string.Empty,  // Usa cadena vacía si es nulo
                            Estado = Convert.ToBoolean(dataReader["Estado"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la categoría", ex);
            }

            return categoria ?? new Categoria(); // Asegura que no se devuelva null
        }



        public bool InsertNewCategoria(Categoria categoria)
        {
            categoria.Estado = categoria.Estado == default ? true : categoria.Estado;

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
                    cmd.Parameters.AddWithValue("@Estado", categoria.Estado); // Asegurarse de enviar como true si es activo

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

    }
}
