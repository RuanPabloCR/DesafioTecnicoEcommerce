using MsVendas.Domain.Models.Enums;

namespace MsVendas.Domain.Models
{
    public class Venda
    {
        public Guid Id { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
