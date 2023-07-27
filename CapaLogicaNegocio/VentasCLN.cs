using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaAccesoDatos;

namespace CapaLogicaNegocio
{
    public class VentasCLN
    {
        private VentasCAD objVentasCAD;
        private DataTable miTabla;
        private object valorUltFactura, ventaId;
        private int nroFactura;

        public VentasCLN()
        {
            objVentasCAD = new VentasCAD();
            miTabla = new DataTable();
            valorUltFactura = new object();
            ventaId = new object();
            nroFactura = 0;
        }

        public int  getPosicionUltFactura(int cantFilas)
        {
            int posicionUltFactura = 0;
            if (cantFilas == 0)
            {
                posicionUltFactura = 0;
            }
            else
            {
                posicionUltFactura = cantFilas - 1;
            }
            return posicionUltFactura; 
        }

        public DataTable consultarVenta()
        {
            miTabla = objVentasCAD.consultarVenta();
            return miTabla;
        }

        public void agregarVenta(object valorUltFactura, string tipoFactura, string vendedor, string cantidad, string total, int idProducto, int idCliente)
        {
            nroFactura = Convert.ToInt32(valorUltFactura) + 1;
            objVentasCAD.agregarVenta(nroFactura, tipoFactura, vendedor, int.Parse(cantidad), float.Parse(total), idProducto, idCliente);
         
        }

        public void eliminarVenta(object ventaId)
        {
            nroFactura = Convert.ToInt32(ventaId);
            objVentasCAD.eliminarVenta(nroFactura);
        }

        public void actualizarVenta(object ventaId, string tipoFactura, string vendedor, string cantidad, string total, int idProducto, int idCliente)
        {
            nroFactura = Convert.ToInt32(ventaId);
            objVentasCAD.actualizarVenta(nroFactura, tipoFactura, vendedor, int.Parse(cantidad), float.Parse(total), idProducto, idCliente);
        }

        public float convertirPrecioVenta(object precioProducto)
        {
            int precioVenta = Convert.ToInt32(precioProducto);
            float precio = precioVenta;
            return precio;
        }

        public string calcularTotal(float precio, string cantidad)
        {
            string total = "";
            float totalVenta = 0.0f;
            int cantidadVenta = Convert.ToInt32(cantidad);
            totalVenta = precio * cantidadVenta;
            total = totalVenta.ToString();
            return total;
        }

        public int convertirIdCliente(object clienteId)
        {
            int idCliente = Convert.ToInt32(clienteId);
            return idCliente;
        }

        public int convertirIdProducto(object productoId)
        {
            int idProducto = Convert.ToInt32(productoId);
            return idProducto;
        }
    }
}
