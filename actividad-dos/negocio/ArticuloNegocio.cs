using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarArticulos()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, Precio, ImagenUrl FROM ARTICULOS A, IMAGENES I, CATEGORIAS C, MARCAS M WHERE A.Id = I.Id AND A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLecutra();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.CodArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Categoria = (string)datos.Lector["Categoria"];
                    aux.Marca = (string)datos.Lector["Marca"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)");
                datos.setearParametro("@codigo", nuevo.CodArticulo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca);
                datos.setearParametro("@idCategoria", nuevo.Categoria);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.ejecutarLecutra();
                nuevo.CodArticulo = (string)datos.Lector["Codigo"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
