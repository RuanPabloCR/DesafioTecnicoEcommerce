using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Events
{
    public class PedidoItemEvent
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
