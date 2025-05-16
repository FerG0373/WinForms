using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace catalog_manager_app
{
    public partial class frmGestion : Form
    {
        private List<Articulo> listaArticulos;
        public frmGestion()
        {
            InitializeComponent();
        }

        private void frmGestion_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarArticulos();
            dgvArticulos.DataSource = listaArticulos;
            pbxArticulo.Load(listaArticulos[0].UrlImagen);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            pbxArticulo.Load(articuloSeleccionado.UrlImagen);
        }
    }
}
