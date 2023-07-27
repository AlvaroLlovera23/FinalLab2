using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class VentasCAD
    {
        private ConexionCAD objConexionCAD;
        private SqlDataReader leerTabla;
        private DataTable miTabla;
        private SqlCommand comando;

        public VentasCAD()
        {
            objConexionCAD = new ConexionCAD();
            comando = new SqlCommand();
            miTabla = new DataTable();
        }

        public DataTable consultarVenta()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarVenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }

        public void agregarVenta(int nroFactura, string tipoFactura, string vendedor, int cantidad, float total, int idProducto, int idCliente)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "agregarVenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@nroFactura",nroFactura);
            comando.Parameters.AddWithValue("@tipoFactura", tipoFactura);
            comando.Parameters.AddWithValue("@vendedor", vendedor);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@total", total);
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void eliminarVenta(int nroFactura)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "eliminarVenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@nroFactura", nroFactura);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void actualizarVenta(int nroFactura, string tipoFactura, string vendedor, int cantidad, float total, int idProducto, int idCliente)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "actualizarVenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@nroFactura", nroFactura);
            comando.Parameters.AddWithValue("@tipoFactura", tipoFactura);
            comando.Parameters.AddWithValue("@vendedor", vendedor);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@total", total);
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }
    }
}
