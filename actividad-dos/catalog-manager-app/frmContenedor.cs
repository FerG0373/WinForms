using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace catalog_manager_app
{
    public partial class frmContenedor : Form
    {
        public frmContenedor()
        {
            InitializeComponent();
            // Inicia la otra ventana desde el arranque de la aplicación.
            AbrirFrmInicio();
        }

        private void AbrirFrmInicio()
        {
            frmInicio ventanaDos = new frmInicio();
            ventanaDos.MdiParent = this;
            ventanaDos.Show();
        }
    }
}
