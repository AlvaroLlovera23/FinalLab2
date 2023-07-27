using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaAccesoDatos;

namespace CapaLogicaNegocio
{
    public class ProductosCLN
    {
        private ProductosCAD objProductosCAD;
        private DataTable miTabla;
        private object valorUltIdProducto;
        private object productoId;
        private int idProducto;

        public ProductosCLN()
        {
            objProductosCAD = new ProductosCAD();
            miTabla = new DataTable();
            valorUltIdProducto = new object();
            productoId = new object();
            idProducto = 0;
        }

        public DataTable consultarProductos()
        {
            miTabla = objProductosCAD.consultarProductos();
            return miTabla;
        }

        public void agregarProducto(object valorUltIdProducto, string descripcion, string precio, string stock)
        {
            int idProducto = Convert.ToInt32(valorUltIdProducto) + 1;
            objProductosCAD.agregarProducto(idProducto, descripcion, float.Parse(precio), int.Parse(stock));
        }

        public void actualizarProducto(object productoId, string descripcion, string precio, string stock)
        {
            idProducto = Convert.ToInt32(productoId);
            objProductosCAD.actualizarProducto(idProducto, descripcion, float.Parse(precio), int.Parse(stock));
        }

        public void eliminarProducto(object productoId)
        {
            idProducto = Convert.ToInt32(productoId);
            objProductosCAD.eliminarProducto(idProducto);
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

        public DataTable consultarNombreProducto()
        {
            miTabla = objProductosCAD.consultarNombreProducto();
            return miTabla;
        }
    }
}
