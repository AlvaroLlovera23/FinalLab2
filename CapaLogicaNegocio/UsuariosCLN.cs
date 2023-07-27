using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaAccesoDatos;

namespace CapaLogicaNegocio
{
    public class UsuariosCLN
    {
        private UsuariosCAD objUsuariosCAD;
        private DataTable miTabla;
        private object valorUltIdUsuario, usuarioId;
        private int idUsuario;

        public UsuariosCLN()
        {
            objUsuariosCAD = new UsuariosCAD();
            miTabla = new DataTable();
            valorUltIdUsuario = new object();
            usuarioId = new object();
            idUsuario = 0;
        }

        public DataTable consultarUsuarios()
        {
            miTabla = objUsuariosCAD.consultarUsuarios();
            return miTabla;
        }

        public void agregarUsuario(object valorUltIdUsuario, string nombreUsuario, string contraseña)
        {
            int idUsuario = Convert.ToInt32(valorUltIdUsuario) + 1;
            objUsuariosCAD.agregarUsuario(idUsuario, nombreUsuario, contraseña);
        }

        public void actualizarUsuario(object usuarioId, string nombreUsuario, string contraseña)
        {
            idUsuario = Convert.ToInt32(usuarioId);
            objUsuariosCAD.actualizarUsuario(idUsuario, nombreUsuario, contraseña);
        }

        public void eliminarUsuario(object usuarioId)
        {
            idUsuario = Convert.ToInt32(usuarioId);
            objUsuariosCAD.eliminarUsuario(idUsuario);
        }

        public int getPosicionUltId(int cantFilas)
        {
            int posicionUltId = 0;
            if (cantFilas == 0)
            {
                posicionUltId = 0;
            }
            else
            {
                posicionUltId = cantFilas - 1;
            }
            return posicionUltId;
        }

        public int validarUsuario(string usuario, string contraseña)
        {
            int bandera = 0;
            bandera = objUsuariosCAD.validarUsuario(usuario, contraseña);
            return bandera;
        }
    }
}
