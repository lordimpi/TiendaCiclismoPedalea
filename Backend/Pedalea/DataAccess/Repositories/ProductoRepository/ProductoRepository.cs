using DataAccess.Data;
using DataAccess.Models.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories.ProductoRepository
{
    public class ProductoRepository : IProductoRepository
    {
        public async Task<Producto> AddProductoAsync(Producto producto)
        {
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_crearProducto";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pNombre", SqlDbType.VarChar).Value = producto.Nombre;
                sqlCommand.Parameters.Add("pDescripcion", SqlDbType.VarChar).Value = producto.Descripcion;
                sqlCommand.Parameters.Add("pPrecio", SqlDbType.VarChar).Value = producto.Precio;
                //guarda lo que trae la consulta
                await sqlCommand.ExecuteNonQueryAsync();
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al Crear un nuevo producto: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return producto;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            bool result = false;
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                Producto producto = await GetProductoAsync(id);
                if (producto == null)
                {
                    return result;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_desactivarProducto";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = id;
                await sqlCommand.ExecuteNonQueryAsync();
                sqlTransaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al borrar los productos: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return result;
        }

        public async Task<Producto> GetProductoAsync(int id)
        {
            Producto producto = null;
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                //manda lo que se ejecuta
                sqlCommand.CommandText = "dbo.sp_buscarProducto";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = id;
                //guarda lo que trae la consulta
                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                //lee cada columna hasta el final
                while (sqlDataReader.Read())
                {
                    producto = new Producto
                    {
                        Id = Convert.ToInt32(sqlDataReader["p_id"]),
                        Nombre = sqlDataReader["p_nombre"].ToString(),
                        Descripcion = sqlDataReader["p_descripcion"].ToString(),
                        Precio = Convert.ToDecimal(sqlDataReader["p_precio"]),
                        IsActive = Convert.ToBoolean(sqlDataReader["p_isActivo"])
                    };
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            List<Producto> Productos = new List<Producto>();
            Producto producto = null;
            //se usa para crear eliminar
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            // leer datos de lo que trae la consulta y se guardan
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                //manda lo que se ejecuta
                sqlCommand.CommandText = "dbo.sp_listarProductos";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //guarda lo que trae la consulta
                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                //lee cada columna hasta el final
                while (sqlDataReader.Read())
                {
                    producto = new Producto
                    {
                        Id = Convert.ToInt32(sqlDataReader["p_id"]),
                        Nombre = sqlDataReader["p_nombre"].ToString(),
                        Descripcion = sqlDataReader["p_descripcion"].ToString(),
                        Precio = Convert.ToDecimal(sqlDataReader["p_precio"]),
                        IsActive = Convert.ToBoolean(sqlDataReader["p_isActivo"])
                    };
                    Productos.Add(producto);
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return Productos;
        }

        public async Task<Producto> UpdateProductoAsync(int id, Producto producto)
        {
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                Producto productoM = await GetProductoAsync(id);
                if (productoM == null)
                {
                    return productoM;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_actualizarProducto";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.Add("pNombre", SqlDbType.VarChar).Value = producto.Nombre;
                sqlCommand.Parameters.Add("pDescripcion", SqlDbType.VarChar).Value = producto.Descripcion;
                sqlCommand.Parameters.Add("pPrecio", SqlDbType.VarChar).Value = producto.Precio;
                //guarda lo que trae la consulta
                await sqlCommand.ExecuteNonQueryAsync();
                sqlTransaction.Commit();
                return producto;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al editar el producto: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
