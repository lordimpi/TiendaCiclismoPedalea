using System.ComponentModel.DataAnnotations;

namespace Pedalea.WebApp.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Display(Name = "Numero de pedido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int NumeroPedido { get; set; }

        [Display(Name = "Dirección de envío")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string DireccionEnvio { get; set; }

        [Display(Name = "Fecha de pedido")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Estado del envío")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.MultilineText)]
        public string Estado { get; set; }
        
        public bool IsActive { get; set; }

        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
