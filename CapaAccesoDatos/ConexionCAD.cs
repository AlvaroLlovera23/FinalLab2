using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapaAccesoDatos
{
    public class ConexionCAD
    {
        private string cadena;
        private SqlConnection conectarDB;

        public ConexionCAD()
        {
            cadena = "Data Source = GLADYSOLMEDO22\\SQLEXPRESS06; Initial Catalog = Repuestos; User ID = sa; Password = betun98";
            conectarDB = new SqlConnection();
            conectarDB.ConnectionString = cadena;
        }
        ~ConexionCAD()
        {
            Console.WriteLine("Out..");
        }

        public SqlConnection abrirConexion()
        {
            try
            {
                conectarDB.Open();
                //MessageBox.Show("La conexion está abierta");
                return conectarDB;
            }
            catch(Exception ex)
            {
                MessageBox.Show("La conexion no se pudo abrir" + ex.Message);
                return conectarDB;
            }
        }

        public void cerrarConexion()
        {
            conectarDB.Close();
        }
    }
}
