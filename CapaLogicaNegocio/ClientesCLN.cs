using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaAccesoDatos;

namespace CapaLogicaNegocio
{
    public class ClientesCLN
    {
        private DataTable miTabla;
        private ClientesCAD objClientesCAD;
        private object valorUltIdCliente, clienteId;
        private int idCliente;

        public ClientesCLN()
        {
            miTabla = new DataTable();
            objClientesCAD = new ClientesCAD();
            valorUltIdCliente = new object();
            clienteId = new object();
            idCliente = 0;
        }

        public DataTable consultarCliente()
        {
            miTabla = objClientesCAD.consultarCliente();
            return miTabla;
        }

        public void agregarCliente(object valorUltIdCliente, string nombre, string telefono, string domicilio)
        {
            int idCliente = Convert.ToInt32(valorUltIdCliente) + 1;
            objClientesCAD.agregarCliente(idCliente, nombre, telefono, domicilio);
        }

        public void actualizarCliente(object clienteId, string nombre, string telefono, string domicilio)
        {
            idCliente = Convert.ToInt32(clienteId);
            objClientesCAD.actualizarCliente(idCliente, nombre, telefono, domicilio);
        }

        public void eliminarCliente(object clienteId)
        {
            idCliente = Convert.ToInt32(clienteId);
            objClientesCAD.eliminarCliente(idCliente);
        }

        public int getPosicionUltFila(int cantFilas)
        {
            int posicionUltFila = 0;
            if (cantFilas == 0)
            {
                posicionUltFila = 0;
            }
            else
            {
                posicionUltFila = cantFilas - 1;
            }
            return posicionUltFila;
        }

        public DataTable consultarNombreCliente()
        {
            miTabla = objClientesCAD.consultarNombreCliente();
            return miTabla;
        }
    }
}
