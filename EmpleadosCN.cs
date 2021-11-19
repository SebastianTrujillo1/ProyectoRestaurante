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
    public class EmpleadosCN
    {
        private Empleados Empleados = new Empleados();

        public List<EmpleadosEntidad> GetAll()
        {
            return Empleados.GetAll();
        }
        public void CreateInvoice(EmpleadosEntidad empleados)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Empleados.Create(empleados);

                Empleados.Update(empleados);

                scope.Complete();
            }
        }
        public EmpleadosEntidad SaveCustomer(EmpleadosEntidad empleados)
        {
            if (CapaAccesoDatos.Empleados.Exist(empleados.lIdEmpleado))
            {
                return Empleados.Update(empleados);
            }
            else
            {
                return Empleados.Update(empleados);
            }
        }
    }
}
