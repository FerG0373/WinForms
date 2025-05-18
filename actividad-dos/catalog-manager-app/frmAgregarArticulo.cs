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
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;

        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                articulo.CodArticulo = (string)txtCodigo.Text;
                articulo.Nombre = (string)txtNombre.Text;
                articulo.Descripcion = (string)txtDescripcion.Text;
                //articulo.Categoria = (Categoria);
                //articulo.Marca = (Marca);

                negocio.agregarArticulo(articulo);
                MessageBox.Show("Artículo agregado con éxito.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }        

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {            
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoriaNegocio.listarCategorias();
                cboMarca.DataSource = marcaNegocio.listarMarcas();

                //Es un modificar artículo.
                if (articulo != null)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
    }
}
