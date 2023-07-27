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
    public partial class frmLogin : Form
    {
        private UsuariosCLN objUsuariosCLN;

        public frmLogin()
        {
            InitializeComponent();
            objUsuariosCLN = new UsuariosCLN();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int bandera = 0;
            bandera = objUsuariosCLN.validarUsuario(txtUsuario.Text, txtContraseña.Text);
            if (bandera == 1)
            {
                    frmMenu frmMenu = new frmMenu();
                    frmMenu.Show();
                    Hide();
            }
            if (bandera == 0)
            {
                MessageBox.Show("Usuario no registrado, póngase en contacto con el Administrador", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
