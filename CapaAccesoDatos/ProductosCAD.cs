using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class ProductosCAD
    {
        private ConexionCAD objConexionCAD;
        private DataTable miTabla;
        private SqlDataReader leerTabla;
        private SqlCommand comando;

        public ProductosCAD()
        {
            objConexionCAD = new ConexionCAD();
            miTabla = new DataTable();
            comando = new SqlCommand();
        }

        public DataTable consultarProductos()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }

        public void agregarProducto(int idProducto, string descripcion, float precio, int stock)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "agregarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@descripcion", descripcion);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void actualizarProducto(int idProducto, string descripcion, float precio, int stock)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "actualizarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idProducto",idProducto);
            comando.Parameters.AddWithValue("@descripcion", descripcion);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void eliminarProducto(int idProducto)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "eliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public DataTable consultarNombreProducto()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarNombreProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }
    }
}
