using System.Text.Json.Serialization;

namespace DataAccess.Models.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public Pedido Pedido { get; set; }
    }
}
