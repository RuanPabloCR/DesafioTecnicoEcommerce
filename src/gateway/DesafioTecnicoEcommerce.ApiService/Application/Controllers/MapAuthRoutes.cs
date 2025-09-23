using DesafioTecnicoEcommerce.ApiGateway.Application.DTOs;
using DesafioTecnicoEcommerce.ApiGateway.Application.Interfaces;
using DesafioTecnicoEcommerce.ApiGateway.Application.Services;
using DesafioTecnicoEcommerce.ApiGateway.Application.Validators;
using DesafioTecnicoEcommerce.ApiGateway.Domain.Models;
using DesafioTecnicoEcommerce.ApiGateway.Infrastructure.JWT;
using DesafioTecnicoEcommerce.ApiGateway.Models.Base;

namespace DesafioTecnicoEcommerce.ApiGateway.Application.Controllers
{
    public static class MapAuthRoutes
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/Cliente/register", async (ClienteRequest clienteRequest, ITokenService tokenService,
                IUserRepository userRepository, PasswordEncryption passwordEncryption) =>
            {
                var validator = new ClienteRequestValidator();

                var result = await validator.ValidateAsync(clienteRequest);
                if (!result.IsValid)
                {
                    return Results.BadRequest(result.Errors);
                }
                var email = new Email(clienteRequest.Email);
                var existingUser = await userRepository.GetClienteByEmailAsync(email);

                if (existingUser != null)
                {
                    return Results.BadRequest("Error creating the account.");
                }
               
                var hashedPassword = passwordEncryption.Hash(clienteRequest.Senha);
                var nome = new Nome(clienteRequest.Nome);
                var cpf = new CPF(clienteRequest.CPF);

                Cliente cliente = new Cliente(nome, email, hashedPassword, cpf);
                await userRepository.AddClienteAsync(cliente);

                var generatedToken = tokenService.GenerateToken(cliente);

                return Results.Ok(new ClienteResponseWithToken(
                    cliente.Name.Name, 
                    cliente.Email.EmailAddress, 
                    generatedToken));
            });
            
            app.MapPost("/Cliente/login", async (ClienteLoginRequest loginRequest, ITokenService tokenService,
                IUserRepository userRepository, PasswordEncryption passwordEncryption) =>
            {
                var validator = new LoginRequestValidator();
                var result = await validator.ValidateAsync(loginRequest);
                if (!result.IsValid)
                {
                    return Results.BadRequest(result.Errors);
                }
                var email = new Email(loginRequest.Email);
                var existingUser = await userRepository.GetClienteByEmailAsync(email);
                
                if (existingUser == null)
                {
                    return Results.BadRequest("Invalid credentials.");
                }
                
                var password = new Password(loginRequest.Senha);
                if (!passwordEncryption.Verify(password, existingUser.Senha))
                {
                    return Results.BadRequest("Invalid credentials.");
                }
                
                var generatedToken = tokenService.GenerateToken(existingUser);

                return Results.Ok(new ClienteResponseWithToken(
                    existingUser.Name.Name, 
                    existingUser.Email.EmailAddress, 
                    generatedToken));
            });
        }
    }
}
