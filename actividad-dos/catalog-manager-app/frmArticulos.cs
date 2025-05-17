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
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        private int indiceImagenActual = 0;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmGestion_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listarArticulos();
            dgvArticulos.DataSource = listaArticulos;
            dgvArticulos.Columns["Id"].Visible = false;  //Oculta el campo Id en el Grid.
            cargarImagen(listaArticulos[0].UrlImagen[0]);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(articuloSeleccionado.UrlImagen[0]);
        }

        private void cargarImagen(Imagen imagen)
        {
            try
            {
                pbxArticulo.Load(imagen.UrlImagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }
        private void btnSiguienteImg_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (articuloSeleccionado.UrlImagen != null && articuloSeleccionado.UrlImagen.Count > 0)
                {
                    // Incrementa el índice y asegúrate de que no exceda la cantidad de imágenes.
                    indiceImagenActual = (indiceImagenActual + 1) % articuloSeleccionado.UrlImagen.Count;

                    // Carga la nueva imagen en el PictureBox.
                    cargarImagen(articuloSeleccionado.UrlImagen[indiceImagenActual]);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo agregar = new frmAgregarArticulo();
            agregar.ShowDialog();
        }

    }
}
