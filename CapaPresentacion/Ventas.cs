using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaLogicaNegocio;

namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        private VentasCLN objVentasCLN;
        private ClientesCLN objClientesCLN;
        private ProductosCLN objProductosCLN;
        private DataTable tablaVentas, tablaClientes, tablaProductos;
        private int indiceCliente, indiceProducto, idCliente, idProducto, indice;
        private object valorUltFactura, clienteId, productoId, precioProducto, ventaId;
        private float precio;

        public frmVentas()
        {
            InitializeComponent();
            objVentasCLN = new VentasCLN();
            objClientesCLN = new ClientesCLN();
            objProductosCLN = new ProductosCLN();
            tablaVentas = new DataTable();
            tablaClientes = new DataTable();
            tablaProductos = new DataTable();
            indiceCliente = 0;
            indiceProducto = 0;
            idCliente = 0;
            idProducto = 0;
            indice = 0;
            valorUltFactura = new object();
            clienteId = new object();
            productoId = new object();
            precioProducto = new object();
            ventaId = new object();
            precio = 0.0f;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            mostrarTabla();
            actualizarId();
            cargarComboProductos();
            cargarComboClientes();
            disabledTextBox();
            disabledComboBox();
            limpiarTextBox();
        }

        public void actualizarId()
        {
            int cantFilas = dgvVentas.Rows.Count;
            int posicionUltFactura = objVentasCLN.getPosicionUltFactura(cantFilas);
            if (cantFilas == 0)
            {
                valorUltFactura = 0;
            }
            else
            {
                valorUltFactura = dgvVentas.Rows[posicionUltFactura].Cells[0].Value;
            }
            
        }

        private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceCliente = cboClientes.SelectedIndex;
            clienteId = tablaClientes.Rows[indiceCliente].ItemArray[0];
            idCliente = objVentasCLN.convertirIdCliente(clienteId);
        }

        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            indiceProducto = cboProductos.SelectedIndex;
            productoId = tablaProductos.Rows[indiceProducto].ItemArray[0];
            idProducto = objVentasCLN.convertirIdProducto(productoId);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            actualizarId();
            objVentasCLN.agregarVenta(valorUltFactura, txtTipoFactura.Text, txtVendedor.Text, txtCantidad.Text, txtTotal.Text, idProducto, idCliente);
            MessageBox.Show("Se agrego con exito", "Repuestos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarTabla();
            limpiarTextBox();
            disabledTextBox();
            disabledComboBox();
            disabledBotones();
        }

        public void limpiarTextBox()
        {
            txtTipoFactura.Clear();
            txtVendedor.Clear();
            txtCantidad.Clear();
            txtTotal.Clear();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Visible = false;
            btnEliminar.Enabled = false;
            btnCalcularTotal.Visible = true;
            enabledTextBox();
            enabledComboBox();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Visible = true;
            btnEliminar.Enabled = false;
            btnCalcularTotal.Visible = true;
            enabledTextBox();
            enabledComboBox();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult opcionElegida = MessageBox.Show("¿Desea eliminar los datos?", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (opcionElegida)
            {
                case DialogResult.Yes:
                    objVentasCLN.eliminarVenta(ventaId);
                    MessageBox.Show("Los datos se eliminaron con exito");
                    mostrarTabla();
                    limpiarTextBox();
                    disabledBotones();
                    break;

                case DialogResult.No:
                    MessageBox.Show("No se confirmó la eliminación");
                    break;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DialogResult opcionElegida = MessageBox.Show("¿Desea modificar los datos?", "Actualizar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (opcionElegida)
            {
                case DialogResult.Yes:
                    objVentasCLN.actualizarVenta(ventaId, txtTipoFactura.Text, txtVendedor.Text, txtCantidad.Text, txtTotal.Text, idProducto, idCliente);
                    MessageBox.Show("Se actualizó con exito");
                    mostrarTabla();
                    limpiarTextBox();
                    disabledTextBox();
                    disabledComboBox();
                    disabledBotones();
                    break;

                case DialogResult.No:
                    MessageBox.Show("No se confirmo la modificacion");
                    break;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Visible = false;
            btnEliminar.Enabled = true;
            btnCalcularTotal.Visible = false;
            disabledTextBox();
            disabledComboBox();
        }

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice == -1)
            {
                MessageBox.Show("Debe seleccionar una fila valida");
            }
            else
            {
                ventaId = dgvVentas.Rows[indice].Cells[0].Value;
                txtTipoFactura.Text = dgvVentas.Rows[indice].Cells[1].Value.ToString();
                txtVendedor.Text = dgvVentas.Rows[indice].Cells[2].Value.ToString();
                txtCantidad.Text = dgvVentas.Rows[indice].Cells[3].Value.ToString();
                txtTotal.Text = dgvVentas.Rows[indice].Cells[4].Value.ToString();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
            disabledTextBox();
            disabledComboBox();
            disabledBotones();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmMenu = new frmMenu();
            frmMenu.Show();
            Close();
        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            precio = consultarPrecioProducto(indiceProducto);
            txtTotal.Text = objVentasCLN.calcularTotal(precio, txtCantidad.Text);
        }

        public float consultarPrecioProducto(int indiceProducto)
        {
            precioProducto = tablaProductos.Rows[indiceProducto].ItemArray[2];
            precio = objVentasCLN.convertirPrecioVenta(precioProducto);
            //lblPrecioPrueba.Text = precio.ToString();
            //lblIdProducto.Text = idProducto.ToString();
            return precio;
        }

        public void mostrarTabla()
        {
            tablaVentas.Clear();
            tablaVentas = objVentasCLN.consultarVenta();
            dgvVentas.DataSource = tablaVentas;
        }

        public void cargarComboProductos()
        {
            tablaProductos.Clear();
            tablaProductos = objProductosCLN.consultarNombreProducto();
            cboProductos.ValueMember = "idProducto";
            cboProductos.DisplayMember = "descripcion";
            cboProductos.DataSource = tablaProductos;
        }

        public void cargarComboClientes()
        {
            tablaClientes.Clear();
            tablaClientes = objClientesCLN.consultarNombreCliente();
            cboClientes.ValueMember = "idCliente";
            cboClientes.DisplayMember = "nombre";
            cboClientes.DataSource = tablaClientes;
        }

        public void disabledTextBox()
        {
            txtTipoFactura.Enabled = false;
            txtVendedor.Enabled = false;
            txtCantidad.Enabled = false;
            txtTotal.Enabled = false;
        }

        public void enabledTextBox()
        {
            txtTipoFactura.Enabled = true;
            txtVendedor.Enabled = true;
            txtCantidad.Enabled = true;
            txtTotal.Enabled = true;
        }

        public void disabledComboBox()
        {
            cboProductos.Enabled = false;
            cboClientes.Enabled = false;
        }

        public void enabledComboBox()
        {
            cboProductos.Enabled = true;
            cboClientes.Enabled = true;
        }

        public void disabledBotones()
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCalcularTotal.Visible = false;
            btnCancelar.Visible = false;
        }
    }
}
