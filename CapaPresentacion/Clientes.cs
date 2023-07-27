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
    public partial class frmClientes : Form
    {
        ClientesCLN objClientesCLN;
        private int indice;
        private DataTable miTabla;
        private object valorUltIdCliente, clienteId;

        public frmClientes()
        {
            InitializeComponent();
            objClientesCLN = new ClientesCLN();
            clienteId = new object();
            indice = 0;
            miTabla = new DataTable();
            valorUltIdCliente = new object();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            mostrarTabla();
            actualizarId();
            disabledTextBox();
            limpiarTextBox();
        }

        public void actualizarId()
        {
            int cantFilas = dgvClientes.Rows.Count;
            int posicionUltFila = objClientesCLN.getPosicionUltFila(cantFilas);
            if (cantFilas == 0)
            {
                valorUltIdCliente = 0;
            }
            else
            {
                valorUltIdCliente = dgvClientes.Rows[posicionUltFila].Cells[0].Value;
            }
        }

        public void mostrarTabla()
        {
            miTabla.Clear();
            miTabla = objClientesCLN.consultarCliente();
            dgvClientes.DataSource = miTabla;
        }

        public void limpiarTextBox()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDomicilio.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            actualizarId();
            objClientesCLN.agregarCliente(valorUltIdCliente, txtNombre.Text, txtTelefono.Text, txtDomicilio.Text);
            MessageBox.Show("Se agrego con exito","Clientes",MessageBoxButtons.OK,MessageBoxIcon.Information);
            mostrarTabla();
            actualizarId();
            limpiarTextBox();
            disabledTextBox();
            disabledBotones();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == dgvClientes.Rows[indice].Cells[1].Value.ToString() && txtTelefono.Text == dgvClientes.Rows[indice].Cells[2].Value.ToString() && txtDomicilio.Text == dgvClientes.Rows[indice].Cells[3].Value.ToString())
            {
                MessageBox.Show("No se realizaron modificaciones", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult opcionElegida = MessageBox.Show("¿Desea modificar los datos?", "Actualizar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (opcionElegida)
                {
                    case DialogResult.Yes:
                        objClientesCLN.actualizarCliente(clienteId, txtNombre.Text, txtTelefono.Text, txtDomicilio.Text);
                        MessageBox.Show("Se actualizó con exito");
                        mostrarTabla();
                        limpiarTextBox();
                        disabledTextBox();
                        disabledBotones();
                        break;

                    case DialogResult.No:
                        MessageBox.Show("No se confirmo la modificacion");
                        break;
                }
            }
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice == -1)
            {
                MessageBox.Show("Debe seleccionar una fila valida");
            }
            else
            {
                clienteId = dgvClientes.Rows[indice].Cells[0].Value;
                txtNombre.Text = dgvClientes.Rows[indice].Cells[1].Value.ToString();
                txtTelefono.Text = dgvClientes.Rows[indice].Cells[2].Value.ToString();
                txtDomicilio.Text = dgvClientes.Rows[indice].Cells[3].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult opcionElegida = MessageBox.Show("¿Desea eliminar los datos?", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (opcionElegida)
            {
                case DialogResult.Yes:
                    objClientesCLN.eliminarCliente(clienteId);
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

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
            disabledBotones();
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
            txtNombre.Enabled = false;
            txtTelefono.Enabled = false;
            txtDomicilio.Enabled = false;
        }

        public void enabledTextBox()
        {
            txtNombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtDomicilio.Enabled = true;
        }

        public void disabledBotones()
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelarEdicion.Visible = false;
        }
    }
}
