using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace CapaAccesoDatos
{
    public class  PedidosAD
    {
        public List<PedidoEntidad> GetAll()
        {
            List<PedidoEntidad> list = new List<PedidoEntidad>();
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"SELECT lIdPedido, sDescripcion_Pedido, lCantidad_Platos FROM BY lIdPedido";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(LoadPedidoEntidad(reader));
                }
            }
            return list;
        }

        public bool Exist(int lIdPedido)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string id)
        {
            int nrorecord = 0;
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"SELECT Count(*) FROM tblPedido WHERE lIdPedidos = @lidpedido";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("lidpedido", id);
                nrorecord = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return nrorecord > 0;
        }

        public PedidoEntidad Create(PedidoEntidad pedido)
        {
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"INSERT INTO tblPedido (lIdPedido, sDescripcion_Pedido, lCantidad_Platos )
                                values(@lIdCantidad_Platos, @sDescripcion_Pedido, @lIdPedido)
                                SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@lIdPedido", pedido.lIdPedido);
                cmd.Parameters.AddWithValue("@lCantidad_Platos", pedido.lCantidad_Platos);
                cmd.Parameters.AddWithValue("@sDescripcion_pedido", pedido.sDescripcion_Pedido);
                cmd.ExecuteNonQuery();
            }
            return pedido;
        }

        private PedidoEntidad LoadPedidoEntidad(IDataReader reader)
        {
           PedidoEntidad pedido = new PedidoEntidad();

            pedido.lCantidad_Platos = Convert.ToInt32(reader["lCantidad_Platos"]);
            pedido.sDescripcion_Pedido = Convert.ToString(reader["sDescripcion_Pedido"]);
            pedido.lIdPedido = Convert.ToInt32(reader["lIdPedido"]);
            return pedido;
        }
        public PedidoEntidad Update(PedidoEntidad pedido)
        {
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"UPDATE tblPedido SET
                                        sDescripcion=@sdescripcion,
                                        lCantidad_Platos=@lcantidad_platos,
                                        WHERE lIdPedido=@lidpedido";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@lidcliente", pedido.lIdCliente);
                cmd.Parameters.AddWithValue("@lcantidad_platos", pedido.lCantidad_Platos);
                cmd.Parameters.AddWithValue("@sdescripcion_pedido", pedido.sDescripcion_Pedido);
                cmd.ExecuteNonQuery();
            }
            return pedido;
        }
    }
}
