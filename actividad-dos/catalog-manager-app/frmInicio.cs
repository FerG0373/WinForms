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
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            frmArticulos ventanaGestion = new frmArticulos();
            ventanaGestion.Show();
        }
    }
}
