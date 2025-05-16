using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Articulo
    {
        public string CodArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //public Marca Marca { get; set; }
        //public Categoria Categoria { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }
    }
}
