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
    public partial class frmUsuarios : Form
    {
        UsuariosCLN objUsuariosCLN;
        private int indice;
        private DataTable miTabla;
        private object valorUltIdUsuario, usuarioId;

        public frmUsuarios()
        {
            InitializeComponent();
            objUsuariosCLN = new UsuariosCLN();
            indice = 0;
            miTabla = new DataTable();
            valorUltIdUsuario = new object();
            usuarioId = new object();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            mostrarTabla();
            actualizarId();
            disabledTextBox();
            limpiarTextBox();
        }

        public void actualizarId()
        {
            int cantFilas = dgvUsuarios.Rows.Count;
            int posicionUltId = objUsuariosCLN.getPosicionUltId(cantFilas);
            if (cantFilas == 0)
            {
                valorUltIdUsuario = 0;
            }
            else
            {
                valorUltIdUsuario = dgvUsuarios.Rows[posicionUltId].Cells[0].Value;
            }
        }

        public void mostrarTabla()
        {
            miTabla.Clear();
            miTabla = objUsuariosCLN.consultarUsuarios();
            dgvUsuarios.DataSource = miTabla;
        }

        public void limpiarTextBox()
        {
            txtNombreUsuario.Clear();
            txtContraseña.Clear();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            actualizarId();
            objUsuariosCLN.agregarUsuario(valorUltIdUsuario, txtNombreUsuario.Text, txtContraseña.Text);
            MessageBox.Show("Se agrego con exito","Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarTabla();
            actualizarId();
            limpiarTextBox();
            disabledBotones();
            disabledTextBox();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.Text == dgvUsuarios.Rows[indice].Cells[1].Value.ToString() && txtContraseña.Text == dgvUsuarios.Rows[indice].Cells[2].Value.ToString())
            {
                MessageBox.Show("No se realizaron modificaciones", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult opcionElegida = MessageBox.Show("¿Desea modificar los datos?", "Actualizar datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                switch (opcionElegida)
                {
                    case DialogResult.Yes:

                        objUsuariosCLN.actualizarUsuario(usuarioId, txtNombreUsuario.Text, txtContraseña.Text);
                        MessageBox.Show("Se modifico con exito", "Actualizar datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mostrarTabla();
                        limpiarTextBox();
                        disabledTextBox();
                        disabledBotones();
                        break;

                    case DialogResult.No:
                        MessageBox.Show("No se confirmo la modificacion", "Actualizar datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            if (indice == -1)
            {
                MessageBox.Show("Debe seleccionar una fila valida");
            }
            else
            {
                usuarioId = dgvUsuarios.Rows[indice].Cells[0].Value;
                txtNombreUsuario.Text = dgvUsuarios.Rows[indice].Cells[1].Value.ToString();
                txtContraseña.Text = dgvUsuarios.Rows[indice].Cells[2].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult opcionElegida = MessageBox.Show("¿Desea eliminar los datos?", "Eliminar Datos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (opcionElegida)
            {
                case DialogResult.Yes:
                    objUsuariosCLN.eliminarUsuario(usuarioId);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarTextBox();
            btnCancelar.Visible = false;
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            disabledTextBox();
        }
         
        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
            btnEliminar.Enabled = false;
            enabledTextBox();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = true;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
            btnEliminar.Enabled = false;
            enabledTextBox();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
            btnCancelar.Visible = false;
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
            txtNombreUsuario.Enabled = false;
            txtContraseña.Enabled = false;
        }

        public void enabledTextBox()
        {
            txtNombreUsuario.Enabled = true;
            txtContraseña.Enabled = true;
        }

        public void disabledBotones()
        {
            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Visible = false;
        }
    }
}

