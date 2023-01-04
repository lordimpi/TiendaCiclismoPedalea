using System.Text.Json.Serialization;

namespace DataAccess.Models.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public string DireccionEnvio { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public bool IsActive { get; set; }

        public List<Producto> Productos { get; set; } = new List<Producto>();
    }
}
