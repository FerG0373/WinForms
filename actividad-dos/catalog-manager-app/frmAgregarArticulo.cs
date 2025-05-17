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
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        //private void btnAceptar_Click(object sender, EventArgs e)
        //{
        //    Articulo articulo = new Articulo();
        //    ArticuloNegocio negocio = new ArticuloNegocio();
        //    try
        //    {
        //        articulo.CodArticulo = txtCodigo.Text;
        //        articulo.Nombre = txtNombre.Text;
        //        articulo.Descripcion = txtDescripcion.Text;
        //        //articulo.Marca = txtMarca.Text;
        //        //articulo.Categoria = txtCategoria.Text;
        //        //articulo.Precio = decimal.Parse(txtPrecio.Text);
        //       //articulo.UrlImagen = txtUrlImagen.Text;

        //        negocio.agregar(articulo);
        //        MessageBox.Show("Articulo agregado correctamente");
        //        Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
