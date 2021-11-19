using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using CapaAccesoDatos;
using System.Transactions;

namespace CapaNegocios
{
    class PedidoCN
    {
        private PedidosAD pedidosAD = new PedidosAD();

        public List<PedidoEntidad> GetAll()
        {
            return pedidosAD.GetAll();
        }
        public void CreateInvoice(PedidoEntidad pedido)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                pedidosAD.Create(pedido);

                pedidosAD.Update(pedido);

                scope.Complete();
            }
        }
        public PedidoEntidad SaveCustomer(PedidoEntidad pedido)
        {
            if (pedidosAD.Exist(pedido.lIdPedido))
            {
                return pedidosAD.Update(pedido);
            }
            else
            {
                return pedidosAD.Update(pedido);
            }
        }
    }
}
