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
            List<Imagen> listaImagenes = listarImagenes();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, (SELECT C.Descripcion FROM CATEGORIAS C WHERE C.Id = A.IdCategoria) Categoria, (SELECT M.Descripcion FROM MARCAS M WHERE M.Id = A.IdMarca) Marca, A.Precio, A.IdCategoria, A.IdMarca FROM ARTICULOS A;"
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

                    aux.Imagen = new List<Imagen>(); // Inicializamos la lista de imágenes.
                    foreach (Imagen img in listaImagenes)
                    {
                        if (img.IdArticulo == aux.Id) // Comprobamos si la imagen corresponde al artículo actual.
                        {
                            aux.Imagen.Add(img); // Añadimos la imagen a la lista del artículo.
                        }
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

        public void agregarImagen(Imagen url)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "INSERT INTO IMAGENES (IdArticulo, ImagenUrl) " +
                    "VALUES (@idArticulo, @imagenUrl)"
                    );
                datos.setearParametro("@idArticulo", url.IdArticulo);
                datos.setearParametro("@imagenUrl", url.UrlImagen);

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

        public void eliminarArticuloLogico(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("UPDATE ARTICULOS SET Eliminado = 1 WHERE Id = @id;");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> filtrarArticulos(string campo, string criterio, string filtro)
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, (SELECT C.Descripcion FROM CATEGORIAS C WHERE C.Id = A.IdCategoria) Categoria, (SELECT M.Descripcion FROM MARCAS M WHERE M.Id = A.IdMarca) Marca, A.Precio, A.IdCategoria, A.IdMarca FROM ARTICULOS A WHERE ";
                switch (campo)
                {
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre LIKE '" + filtro + "%' ";
                                break;
                            case "Contiene":
                                consulta += "Nombre LIKE '%' + '" +filtro+ "' + '%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre LIKE '%" + filtro + "'";
                                break;
                            case "No contiene":
                                consulta += "Nombre NOT LIKE '%' '" + filtro + "' '%'";
                                break;
                        }
                        break;
                    case "Código":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Codigo LIKE '" + filtro + "%' ";
                                break;
                            case "Contiene":
                                consulta += "Codigo LIKE '%' + '" + filtro + "' + '%'";
                                break;
                            case "Termina con":
                                consulta += "Codigo LIKE '%" + filtro + "'";
                                break;
                            case "No contiene":
                                consulta += "Codigo NOT LIKE '%' '" + filtro + "' '%'";
                                break;
                        }
                        break;
                    case "Precio":
                        switch (criterio)
                        {
                            case "Menor a":
                                consulta += "Precio < " + filtro;
                                break;
                            case "Mayor a":
                                consulta += "Precio > " + filtro;
                                break;
                            case "No igual a":
                                consulta += "Precio <> " + filtro;
                                break;
                            default:
                                consulta += "Precio = " + filtro;
                                break;
                        }
                        break;
                }

                datos.setearConsulta(consulta);
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
                }

                return listaFiltrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
