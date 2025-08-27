namespace MsVendas.Domain.Events
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Dados { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    }
}
