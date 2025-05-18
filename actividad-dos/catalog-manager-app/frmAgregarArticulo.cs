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
                articulo.CodArticulo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                articulo.UrlImagen = new List<Imagen>(); // Inicializar la lista
                articulo.UrlImagen.Add(new Imagen { UrlImagen = txtUrlImagen.Text });

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
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                cboMarca.DataSource = marcaNegocio.listarMarcas();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                //Es un modificar artículo.
                if (articulo != null)
                {
                    txtCodigo.Text = articulo.CodArticulo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtUrlImagen.Text = articulo.UrlImagen[0].UrlImagen;
                    cargarImagenes(articulo.UrlImagen);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            List<Imagen> imagenes = new List<Imagen>
            {
                new Imagen { UrlImagen = txtUrlImagen.Text }
            };

            cargarImagenes(imagenes);
        }
    }
}
