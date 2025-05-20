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
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                //"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, C.Descripcion Categoria, M.Descripcion Marca, A.Precio, A.IdCategoria, A.IdMarca " +
                //"FROM ARTICULOS A, CATEGORIAS C, MARCAS M " +
                //"WHERE C.Id = A.IdCategoria AND M.Id = A.IdMarca;"
                //"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, C.Descripcion Categoria, M.Descripcion Marca, A.Precio, A.IdCategoria, A.IdMarca, I.ImagenUrl FROM ARTICULOS A, CATEGORIAS C, MARCAS M, IMAGENES I WHERE C.Id = A.IdCategoria AND M.Id = A.IdMarca AND I.IdArticulo = A.Id;"
                "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, C.Descripcion AS Categoria, M.Descripcion AS Marca, A.Precio, A.IdCategoria, A.IdMarca, (SELECT TOP 1 ImagenUrl FROM IMAGENES I WHERE I.IdArticulo = A.Id) AS ImagenUrl FROM ARTICULOS A LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria LEFT JOIN MARCAS M ON M.Id = A.IdMarca;"
                );

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = Math.Round((decimal)datos.Lector["Precio"], 2);

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"]; // Para poder leer y precargar la categoría cuando voy a modificar.
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    else
                        aux.Categoria.Descripcion = "";

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"]; // Para poder leer y precargar la marca cuando voy a modificar.
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Imagen = new List<Imagen>(); // Instancio la lista de imágenes.
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        Imagen imagen = new Imagen(); // Instancio la imagen.
                        imagen.UrlImagen = (string)datos.Lector["ImagenUrl"];
                        aux.Imagen.Add(imagen); // Guardo la imagen en la lista de imágenes.
                    }

                    listaArticulos.Add(aux);
                }

                return listaArticulos;
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

        public List<Imagen> listarImagenes()
        {
            List<Imagen> listaImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    listaImagenes.Add(aux);
                }

                return listaImagenes;
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
                datos.setearParametro("@codigo", articulo.Codigo);
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

        public void modificarArticulo(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta
                    (
                        "UPDATE ARTICULOS " +
                        "SET " +
                            "Codigo = @codigo, " +
                            "Nombre = @nombre, " +
                            "Descripcion = @descripcion, " +
                            "IdMarca = @idMarca, " +
                            "IdCategoria = @idCategoria, " +
                            "Precio = @precio " +
                        "WHERE Id = @id;"
                    );
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);

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

        public void eliminarArticulo(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @id;");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
