using MassTransit;
using MsVendas.Application.DTOs;
using MsVendas.Application.Services;

namespace MsVendas.Application.Handlers
{
    public class PedidoHandler
    {
        private ILogger<PedidoHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ProdutosEstoqueService _produtosEstoqueService;
        public PedidoHandler(ILogger<PedidoHandler> logger, IPublishEndpoint publishEndpoint
            , ProdutosEstoqueService produtosEstoqueService)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _produtosEstoqueService = produtosEstoqueService;
        }
        // Verificar quais produtos tem quantidade maior ou igual a quantidade pedida ideia1: enviar no produtoestoqueresponse
        public async Task<ProdutosVerificadosResponse> HandleAsync(PedidoRequest pedidoRequest, CancellationToken cancellationToken)
        {
            if (pedidoRequest.Itens == null || !pedidoRequest.Itens.Any())
            {
                _logger.LogWarning("O pedido não contém itens.");
                return null;
            }
            var produtosVerificados = await _produtosEstoqueService.VerificarProdutosEstoqueAsync(pedidoRequest.Itens, cancellationToken);
            var produtosDisponiveis = produtosVerificados
                .Where(p => p.IsDisponivel && p.QuantidadeSolicitada <= p.QuantidadeDisponivel)
                .ToList();
            _logger.LogInformation("Produtos disponíveis: {produtosDisponiveis}", produtosDisponiveis);
            var produtosNaoDisponiveis = produtosVerificados.Where(p => !p.IsDisponivel || p.QuantidadeSolicitada > p.QuantidadeDisponivel)
                .ToList();
            _logger.LogInformation("Produtos não disponíveis: {produtosNaoDisponiveis}", produtosNaoDisponiveis);
            return new ProdutosVerificadosResponse(produtosDisponiveis, produtosNaoDisponiveis);

        }
    }
}
