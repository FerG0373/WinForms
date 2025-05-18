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
                datos.setearConsulta(
                    "SELECT " +
                        "A.Id, " +
                        "A.Codigo, " +
                        "A.Nombre, " +
                        "A.Descripcion, " +
                        "I.ImagenUrl, " +
                        "C.Descripcion Categoria, " +
                        "M.Descripcion Marca, " +
                        "Precio " +
                    "FROM " +
                        "ARTICULOS A " +
                        "LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id " +
                        "LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                        "LEFT JOIN MARCAS M ON A.IdMarca = M.Id;"
                );

                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.CodArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Categoria = new Categoria();

                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    else
                        aux.Categoria.Descripcion = "";
                    
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Precio = Math.Round((decimal)datos.Lector["Precio"], 2); //Se agregó un método para redondear y mostrar solo dos decimales.
                    aux.UrlImagen = new List<Imagen>();
                    Imagen imagen = new Imagen();
                    imagen.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.UrlImagen.Add(imagen);

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

        public void agregarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                    "VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)"
                    );
                datos.setearParametro("@codigo", articulo.CodArticulo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@precio", articulo.Precio);
                datos.ejecutarAccion();
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

        //public void agregar(Articulo nuevo)
        //{
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio)");
        //        datos.setearParametro("@codigo", nuevo.CodArticulo);
        //        datos.setearParametro("@nombre", nuevo.Nombre);
        //        datos.setearParametro("@descripcion", nuevo.Descripcion);
        //        datos.setearParametro("@idMarca", nuevo.Marca);
        //        datos.setearParametro("@idCategoria", nuevo.Categoria);
        //        datos.setearParametro("@precio", nuevo.Precio);
        //        datos.ejecutarLectura();
        //        nuevo.CodArticulo = (string)datos.Lector["Codigo"];
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

    }
}
