using DataAccess.Data;
using DataAccess.Models.Entities;
using DataAccess.Repositories.ProductoRepository;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Repositories.PedidoRepository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IProductoRepository _productoRepository;

        public PedidoRepository(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Pedido> AddPedidoAsync(Pedido pedido)
        {
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_crearPedido";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("ppId", SqlDbType.Int).Value = pedido.ProductoId;
                sqlCommand.Parameters.Add("pNumeroPedido", SqlDbType.Int).Value = pedido.NumeroPedido;
                sqlCommand.Parameters.Add("pFecha", SqlDbType.DateTime).Value = pedido.Fecha;
                sqlCommand.Parameters.Add("pDireccionEnvio", SqlDbType.VarChar).Value = pedido.DireccionEnvio;
                sqlCommand.Parameters.Add("pEstado", SqlDbType.VarChar).Value = pedido.Estado;
                //guarda lo que trae la consulta
                await sqlCommand.ExecuteNonQueryAsync();
                sqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                sqlTransaction?.Rollback();
                throw new Exception($"Se produjo un error al Crear un nuevo pedido: {ex.Message}");
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return pedido;
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            bool result = false;
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                Pedido pedido = await GetPedidoAsync(id);
                if (pedido == null)
                {
                    return result;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_desactivarPedido";
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

        public async Task<Pedido> GetPedidoAsync(int id)
        {
            Pedido pedido = null;
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
                sqlCommand.CommandText = "dbo.sp_buscarPedido";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = id;
                //guarda lo que trae la consulta
                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                //lee cada columna hasta el final
                while (sqlDataReader.Read())
                {
                    pedido = new()
                    {
                        Id = Convert.ToInt32(sqlDataReader["pd_id"]),
                        NumeroPedido = Convert.ToInt32(sqlDataReader["pd_numeroPedido"]),
                        Fecha = Convert.ToDateTime(sqlDataReader["pd_fecha"]),
                        DireccionEnvio = sqlDataReader["pd_direccionEnvio"].ToString(),
                        Estado = sqlDataReader["pd_estado"].ToString(),
                        IsActive = Convert.ToBoolean(sqlDataReader["pd_isActive"]),
                    };
                    Producto p = await _productoRepository.GetProductoAsync(Convert.ToInt32(sqlDataReader["p_id"]));
                    pedido.Productos.Add(p);
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync()
        {
            List<Pedido> Pedidos = new List<Pedido>();
            Pedido pedido = null;
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
                sqlCommand.CommandText = "dbo.sp_listarPedidos";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                //guarda lo que trae la consulta
                sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                //lee cada columna hasta el final
                while (sqlDataReader.Read())
                {
                    pedido = new()
                    {
                        Id = Convert.ToInt32(sqlDataReader["pd_id"]),
                        NumeroPedido = Convert.ToInt32(sqlDataReader["pd_numeroPedido"]),
                        Fecha = Convert.ToDateTime(sqlDataReader["pd_fecha"]),
                        DireccionEnvio = sqlDataReader["pd_direccionEnvio"].ToString(),
                        Estado = sqlDataReader["pd_estado"].ToString(),
                        IsActive = Convert.ToBoolean(sqlDataReader["pd_isActive"]),
                    };
                    Producto p = await _productoRepository.GetProductoAsync(Convert.ToInt32(sqlDataReader["p_id"]));
                    pedido.Productos.Add(p);
                    Pedidos.Add(pedido);
                }
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return Pedidos;
        }

        public async Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido)
        {
            SqlConnection sqlConnection = Conexion.GetInstancia().CreateConnection();
            SqlCommand sqlCommand = null;
            SqlTransaction sqlTransaction = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                Pedido pedidoM = await GetPedidoAsync(id);
                if (pedidoM == null)
                {
                    return pedidoM;
                }
                sqlTransaction = sqlConnection.BeginTransaction();
                sqlCommand.CommandText = "dbo.sp_actualizarPedido";
                //typo de comando se llama enumerable de tipo procedimiento almacenado
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add("pdId", SqlDbType.Int).Value = id;
                sqlCommand.Parameters.Add("pId", SqlDbType.Int).Value = pedido.ProductoId;
                sqlCommand.Parameters.Add("pNumeroPedido", SqlDbType.Int).Value = pedido.NumeroPedido;
                sqlCommand.Parameters.Add("pFecha", SqlDbType.Date).Value = pedido.Fecha;
                sqlCommand.Parameters.Add("pDireccionEnvio", SqlDbType.VarChar).Value = pedido.DireccionEnvio;
                sqlCommand.Parameters.Add("pEstado", SqlDbType.VarChar).Value = pedido.Estado;
                //guarda lo que trae la consulta
                await sqlCommand.ExecuteNonQueryAsync();
                sqlTransaction.Commit();
                return pedido;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw new Exception($"Se produjo un error al editar el pedido: {ex.Message}");
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
