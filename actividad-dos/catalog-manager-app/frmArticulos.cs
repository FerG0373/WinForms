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
        //private int indiceImagenActual = 0;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void frmGestion_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.listarArticulos();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["Id"].Visible = false;  //Oculta el campo Id en el Grid.
                cargarImagenes(listaArticulos[0].UrlImagen); //Lista de imágenes del primer artículo.
                dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagenes(articuloSeleccionado.UrlImagen);
            //indiceImagenActual = 0; //Reinicia el índice al cambiar de fila.
        }

        private void cargarImagenes(List<Imagen> imagenes)
        {
            try
            {
                pbxArticulo.Load(imagenes[0].UrlImagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo agregar = new frmAgregarArticulo();
            agregar.ShowDialog();
        }

    }
}
