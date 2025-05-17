using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string CodArticulo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        //Relación 1 a 1 con marcas.
        public Marca Marca { get; set; }

        //Relación 1 a 1 con categorías.
        public string Categoria { get; set; }
        
        //Relación 1 a Muchos con las imágenes.
        public string UrlImagen { get; set; }
    }
}
