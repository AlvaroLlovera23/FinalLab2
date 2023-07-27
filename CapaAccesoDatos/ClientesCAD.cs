using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class ClientesCAD
    {
        private ConexionCAD objConexionCAD;
        private SqlDataReader leerTabla;
        private DataTable miTabla;
        private SqlCommand comando;

        public ClientesCAD()
        {
            objConexionCAD = new ConexionCAD();
            miTabla = new DataTable();
            comando = new SqlCommand();
        }

        public DataTable consultarCliente()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }

        public void agregarCliente(int idCliente, string nombre, string telefono, string domicilio)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "agregarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@domicilio", domicilio);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();

        }

        public void actualizarCliente(int idCliente, string nombre, string telefono, string domicilio)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "actualizarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@domicilio", domicilio);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();

        }

        public void eliminarCliente(int idCliente)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "eliminarCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();

        }

        public DataTable consultarNombreCliente()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarNombreCliente";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }
    }
}
