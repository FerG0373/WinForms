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
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }        
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; } //Relación 1 a 1 con categorías.
        public Marca Marca { get; set; } //Relación 1 a 1 con marcas.
        public decimal Precio { get; set; }
        public List<Imagen> Imagen { get; set; } //Relación 1 a Muchos con imágenes.
    }
}
