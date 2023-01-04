using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pedalea.WebApp.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Descripcion { get; set; }

        //[DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Precio { get; set; }

        [Display(Name = "Estado")]
        public bool IsActive { get; set; }

        public List<Pedido> Pedido { get; set; }
    }
}
