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
    public class Empleados
    {
        public List<EmpleadosEntidad> GetAll()
        {
            List<EmpleadosEntidad> list = new List<EmpleadosEntidad>();
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"SELECT sTurno, lIdEmpleados FROM BY lIdEmpleados";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Loadempelados(reader));
                }
            }
            return list;
        }

        public static bool Exist(int lIdEmpleado)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string id)
        {
            int nrorecord = 0;
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"SELECT Count(*) FROM tblEmpleados WHERE lIdEmpelados= @lidempleados";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("lidEmpleado", id);
                nrorecord = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return nrorecord > 0;
        }

        public EmpleadosEntidad Create(EmpleadosEntidad empleados)
        {
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"INSERT INTO tblEmpleado (lIdEmpleado,sTurno)
                                values(@lIdEmpleado, @sTurno)
                                SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sladmin", empleados.sTurno);
                cmd.Parameters.AddWithValue("@lIdEmpelado", empleados.lIdEmpleado);
                cmd.ExecuteNonQuery();
            }
            return empleados;
        }

        private EmpleadosEntidad Loadempelados(IDataReader reader)
        {
            EmpleadosEntidad empelados = new EmpleadosEntidad();

            empelados.lIdEmpleado = Convert.ToInt32(reader["lIdEmpleado"]);
            empelados.sTurno = Convert.ToString(reader["sTurno"]);
            return empelados;
        }
        public EmpleadosEntidad Update(EmpleadosEntidad empleados)
        {
            using (SqlConnection conn = new SqlConnection(ConexionSQL.ObtenerCadenaConexion()))
            {
                conn.Open();
                string sql = @"UPDATE tblPedido SET
                                        sTurno=@sturno,
                                        WHERE lIdEmpleado=@lidempleado";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@lidempleado", empleados.lIdEmpleado);
                cmd.Parameters.AddWithValue("@sTurno", empleados.sTurno);
                cmd.ExecuteNonQuery();
            }
            return empleados;
        }
    }
}
