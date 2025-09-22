using FluentValidation;
using MSEstoque.Application.DTOs;
using MSEstoque.Application.Services;

namespace MSEstoque.Application.Controllers
{
    public static class ProdutoEndpointsExtensions
    {
        public static void AddProdutosRoutes(this WebApplication app)
        {
            var produtosGroup = app.MapGroup("/estoque")
                .RequireAuthorization();

            produtosGroup.MapPost("/cadastrar", async (ProdutoRequest produtoRequest,
                ProdutoService cadastrarProdutoService) =>
            {
                try
                {
                    ProdutoResponse produtoResponse = await cadastrarProdutoService.Execute(produtoRequest);
                    return Results.Created($"/produtos/{produtoResponse.Id}", produtoResponse);
                }
                catch(ValidationException ex)
                {
                    return Results.BadRequest(ex.Errors);
                }
                catch(Exception)
                {
                    return Results.Problem("Ocorreu um erro.");
                }
                
            });
            produtosGroup.MapGet("/produtos", async (int? page, ProdutoService produtoService) =>
            {
                try
                {
                    var produtos = await produtoService.GetProdutosAsync(page);
                    if(produtos == null || !produtos.Any())
                    {
                        return Results.NoContent();
                    }
                    return Results.Ok(produtos);
                }
                catch (Exception)
                {
                    return Results.BadRequest("Ocorreu um erro.");
                }
            });

        }
    }
}
