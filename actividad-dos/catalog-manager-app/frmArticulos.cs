using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private void frmArticulos_Load(object sender, EventArgs e)
        {
            cargarDatos();
            cboCampo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCriterio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Codigo");
            cboCampo.Items.Add("Precio");

        }

        private void cargarDatos()
        {

            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulos = negocio.listarArticulos();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas(); // Oculta las columnas innecesarias en el DataGridView.
                cargarImagenes(listaArticulos[0].Imagen); // Lista de imágenes del primer artículo.
                dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Adapta el tamaño de las columnas al DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void ocultarColumnas()
        {
            if (dgvArticulos.Columns.Contains("Id"))
                dgvArticulos.Columns["Id"].Visible = false; // Oculta el campo Id en el Grid.
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagenes(articuloSeleccionado.Imagen);
            }
        }

        private void cargarImagenes(List<Imagen> imagenes)
        {
            try
            {
                if(imagenes.Count > 0)
                {
                    indiceImagenActual = 0; // Reinicia el índice.
                    pbxArticulo.Load(imagenes[indiceImagenActual].UrlImagen);
                }
                else
                {
                    throw new Exception("No hay imágenes disponibles.");
                }
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
            cargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
            cargarDatos();
        }


        private void btnSiguienteImg_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                {
                    indiceImagenActual++;
                    // Si el índice supera el número de imágenes, vuelve al inicio.
                    if (indiceImagenActual >= seleccionado.Imagen.Count)
                    {
                        indiceImagenActual = 0;
                    }
                    // Carga la imagen en el PictureBox.
                    pbxArticulo.Load(seleccionado.Imagen[indiceImagenActual].UrlImagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvArticulos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminar(false);
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void eliminar (bool logico = false)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Estás seguro que querés eliminar este elemento?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    if(logico)
                    {
                        negocio.eliminarArticuloLogico(seleccionado.Id);
                    }
                    else
                    {
                        negocio.eliminarArticulo(seleccionado.Id);
                    }
                    cargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltroRapido.Text;

            if (filtro.Length >= 2)
            {
                listaFiltrada = listaArticulos.FindAll(x =>
                x.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                x.Codigo.ToUpper().Contains(filtro.ToUpper()) ||
                (x.Marca != null && x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper())) ||
                (x.Categoria != null && x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper())));
                dgvArticulos.DataSource = null; // Limpia el DataGridView antes de asignar la nueva fuente de datos.
                dgvArticulos.DataSource = listaFiltrada;
                ocultarColumnas(); // Oculta las columnas innecesarias.
            }
            else
            {
                cargarDatos(); // Si el filtro está vacío, recarga todos los artículos.
            }
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string campoSeleccionado = cboCampo.SelectedItem.ToString();
            if (campoSeleccionado == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Igual a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("No igual a");
            }
            else 
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("No contiene");
            }
        }

        private bool validarCampos()
        {
            if (cboCampo.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un campo para filtrar.");
                return false;
            }
            if (cboCriterio.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un criterio para filtrar.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                MessageBox.Show("Debe ingresar un valor para filtrar.");
                return false;
            }
            if (cboCampo.SelectedItem.ToString() == "Precio" && !soloNumeros(txtFiltro.Text))
            {
                MessageBox.Show("El filtro de precio debe contener solo números.");
                return false;
            }
            return true;
        }

        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena )
            {
                if (!(char.IsNumber(caracter)))
                        return false; // Si encuentra un carácter que no es un número, retorna false.
            }
            return true; 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (!validarCampos())
                {
                    return; // Si la validación falla, no continúa con la búsqueda.
                }

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvArticulos.DataSource = negocio.filtrarArticulos(campo, criterio, filtro);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
