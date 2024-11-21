using DonChamol.Models.Data;
using DonChamol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class OrdenRepository: IOrdenRepository<Orden>
{
    // Obtener lista de clientes
    public List<Cliente> GetAllCliente()
    {
        List<Cliente> ListClientes = new List<Cliente>();

        using (SqlConnection connection = new SqlConnection(BDConnection.Connection()))
        {
            SqlCommand cmd = new SqlCommand("GetAllCliente", connection);
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
                ListClientes = null;
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
                ordenes = null;
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
                ListMeseros = null;
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
                ListMesas = null;
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
                listMenuItems = null;
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
                SqlCommand cmd = new SqlCommand("InsertOrden", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Cliente", orden.id_Cliente);
                cmd.Parameters.AddWithValue("@id_Mesero", orden.id_Mesero);
                cmd.Parameters.AddWithValue("@id_Mesa", orden.id_Mesa);
                cmd.Parameters.AddWithValue("@Fecha_Orden", orden.Fecha_Orden);
                cmd.Parameters.Add("@Result", SqlDbType.Bit).Direction = ParameterDirection.Output;

                // Crear tabla para los detalles de la orden
                DataTable detailsTable = new DataTable();
                detailsTable.Columns.Add("id_Menu", typeof(int));
                detailsTable.Columns.Add("Cantidad", typeof(int));
                detailsTable.Columns.Add("Precio", typeof(decimal));

                foreach (var detalle in ordenDetalles)
                {
                    detailsTable.Rows.Add(detalle.id_Menu, detalle.Cantidad, detalle.Precio);
                }

                SqlParameter detailsParam = cmd.Parameters.AddWithValue("@OrdenDetalles", detailsTable);
                detailsParam.SqlDbType = SqlDbType.Structured;
                detailsParam.TypeName = "dbo.OrdenDetallesType";

                conn.Open();
                cmd.ExecuteNonQuery();

                result = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
            }
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
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

    public bool DeleteOrdenById(int id)
    {
        throw new NotImplementedException();
    }
}