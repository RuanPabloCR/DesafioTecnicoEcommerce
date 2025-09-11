using MassTransit;
using MSEstoque.Application.Services;
using shared.Events;

namespace MSEstoque.Application.Consumers
{
    internal sealed class PedidoRealizadoConsumer : IConsumer<PedidoRealizadoEvent>
    {
        private readonly ILogger<PedidoRealizadoConsumer> _logger;
        private readonly EstoqueService _estoqueService;
        private readonly PedidoStatusService _pedidoStatusService;

        public PedidoRealizadoConsumer(
            ILogger<PedidoRealizadoConsumer> logger, 
            EstoqueService estoqueService,
            PedidoStatusService pedidoStatusService)
        {
            _logger = logger;
            _estoqueService = estoqueService;
            _pedidoStatusService = pedidoStatusService;
        }

        public async Task Consume(ConsumeContext<PedidoRealizadoEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("Pedido Realizado: {PedidoId} para o cliente {ClienteId} no valor de {ValorTotal} com {QuantidadeItens} itens.",
                message.PedidoId, message.ClienteId, message.ValorTotal, message.Itens.Count);

            bool allUpdatesSuccessful = true;
            foreach (var item in message.Itens)
            {
                bool updateResult = await _estoqueService.AtualizarEstoqueProduto(item.ProdutoId, -item.Quantidade);
                if (!updateResult)
                {
                    _logger.LogWarning("Falha ao atualizar estoque para o item {ProdutoId} do pedido {PedidoId}.",
                        item.ProdutoId, message.PedidoId);
                    allUpdatesSuccessful = false;
                }
            }

            if (allUpdatesSuccessful)
            {
                var statusAtualizado = await _pedidoStatusService.AtualizarStatusPedidoParaEnviadoAsync(message.PedidoId);
                if (statusAtualizado)
                {
                    _logger.LogInformation("Status do pedido {PedidoId} atualizado para Enviado", message.PedidoId);
                }
                else
                {
                    _logger.LogWarning("Falha ao atualizar status do pedido {PedidoId} para Enviado", message.PedidoId);
                }
            }
            else
            {
                _logger.LogWarning("Uma ou mais atualizações de estoque falharam para o pedido {PedidoId}. Status não atualizado.", message.PedidoId);
            }
        }
    }
}
