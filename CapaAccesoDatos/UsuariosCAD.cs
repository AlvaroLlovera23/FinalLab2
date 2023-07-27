using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaAccesoDatos
{
    public class UsuariosCAD
    {
        private ConexionCAD objConexionCAD;
        private DataTable miTabla;
        private SqlDataReader leerTabla;
        private SqlCommand comando;

        public UsuariosCAD()
        {
            objConexionCAD = new ConexionCAD();
            miTabla = new DataTable();
            comando = new SqlCommand();
        }

        public DataTable consultarUsuarios()
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "consultarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            leerTabla = comando.ExecuteReader();
            miTabla.Load(leerTabla);
            objConexionCAD.cerrarConexion();
            return miTabla;
        }

        public void agregarUsuario(int idUsuario, string nombreUsuario, string contraseña)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "agregarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void actualizarUsuario(int idUsuario, string nombreUsuario, string contraseña)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "actualizarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public void eliminarUsuario(int idUsuario)
        {
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "eliminarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.ExecuteNonQuery();
            objConexionCAD.cerrarConexion();
        }

        public int validarUsuario(string usuario, string contraseña)
        {
            int bandera = 0;
            comando.Connection = objConexionCAD.abrirConexion();
            comando.CommandText = "validarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@nombreUsuario", usuario);
            comando.Parameters.AddWithValue("@contraseña", contraseña);
            leerTabla = comando.ExecuteReader();
            if (leerTabla.Read())
            {
                bandera = 1;
            }
            leerTabla.Close();
            objConexionCAD.cerrarConexion();
            return bandera;
        }
    }
}
