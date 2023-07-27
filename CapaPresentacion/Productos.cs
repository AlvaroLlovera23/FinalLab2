using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogicaNegocio;

namespace CapaPresentacion
{
    public partial class frmProductos : Form
    {
        ProductosCLN objProductosCLN;
        private int indice;
        private DataTable miTabla;
        private object valorUltIdProducto, productoId;

        public frmProductos()
        {
            InitializeComponent();
            objProductosCLN = new ProductosCLN();
            indice = 0;
            miTabla = new DataTable();
            valorUltIdProducto = new object();
            productoId = new object();
        }

        public void mostrarTabla()
        {
            miTabla.Clear();
            miTabla = objProductosCLN.consultarProductos();
            dgvProductos.DataSource = miTabla;
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            mostrarTabla();
            actualizarId();
            disabledTextBox();
            limpiarTextBox();
        }

        public void actualizarId()
        {
            int cantFilas = dgvProductos.Rows.Count;
            int posicionUltFila = objProductosCLN.getPosicionUltFila(cantFilas);
            if (cantFilas == 0)
            {
                valorUltIdProducto = 0;
            }
            else
            {
                valorUltIdProducto = dgvProductos.Rows[posicionUltFila].Cells[0].Value;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            actualizarId();
            objProductosCLN.agregarProducto(valorUltIdProducto, txtDescripcion.Text, txtPrecio.Text, txtStock.Text);
            MessageBox.Show("Se agrego con exito", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarTabla();
            actualizarId();
            limpiarTextBox();
            disabledTextBox();
            disabledBotones();
        }

        public void limpiarTextBox()
        {
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text == dgvProductos.Rows[indice].Cells[1].Value.ToString() && txtPrecio.Text == dgvProductos.Rows[indice].Cells[2].Value.ToString() && txtStock.Text == dgvProductos.Rows[indice].Cells[3].Value.ToString())
            {
                MessageBox.Show("No se realizaron modificaciones", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult opcionElegida = MessageBox.Show("¿Desea modificar los datos?", "Actualizar datos", MessageBoxButtons.YesNo);

                switch (opcionElegida)
                {
                    case DialogResult.Yes:
                        objProductosCLN.actualizarProducto(productoId, txtDescripcion.Text, txtPrecio.Text, txtStock.Text);
                        MessageBox.Show("Se actualizó con exito");
                        mostrarTabla();
                        limpiarTextBox();
                        disabledBotones();
                        disabledTextBox();
                        break;

                    case DialogResult.No:
                        MessageBox.Show("No se confirmo la modificacion");
                        break;
                }
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice == -1)
            {
                MessageBox.Show("Debe seleccionar una fila valida");
            }
            else
            {
                productoId = dgvProductos.Rows[indice].Cells[0].Value;
                txtDescripcion.Text = dgvProductos.Rows[indice].Cells[1].Value.ToString();
                txtPrecio.Text = dgvProductos.Rows[indice].Cells[2].Value.ToString();
                txtStock.Text = dgvProductos.Rows[indice].Cells[3].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult opcionElegida = MessageBox.Show("¿Desea eliminar los datos?", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (opcionElegida)
            {
                case DialogResult.Yes:
                    objProductosCLN.eliminarProducto(productoId);
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

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
            btnEditar.Enabled = false;
            btnCancelarEdicion.Visible = false;
            disabledTextBox();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelarEdicion.Visible = false;
            btnEliminar.Enabled = false;
            enabledTextBox();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelarEdicion.Visible = true;
            btnEliminar.Enabled = false;
            enabledTextBox();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelarEdicion.Visible = false;
            btnEliminar.Enabled = true;
            disabledTextBox();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmMenu = new frmMenu();
            frmMenu.Show();
            Close();
        }

        public void disabledTextBox()
        {
            txtDescripcion.Enabled = false;
            txtPrecio.Enabled = false;
            txtStock.Enabled = false;
        }

        public void enabledTextBox()
        {
            txtDescripcion.Enabled = true;
            txtPrecio.Enabled = true;
            txtStock.Enabled = true;
        }

        public void disabledBotones()
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelarEdicion.Visible = false;
        }
    }
}
