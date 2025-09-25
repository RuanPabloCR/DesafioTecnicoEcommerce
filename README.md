
# Desafio Técnico E-commerce

## Próximos Passos

- **Remover entidades não utilizadas** em cada microsserviço
- **Corrigir lógica de consumo no broker** (RabbitMQ)
- **Criar tabela intermediária e refatorar tabela** no microsserviço de vendas

---

## Sobre o Projeto

Este projeto é uma solução backend para um desafio técnico, simulando uma plataforma de e-commerce baseada em arquitetura de microserviços. O objetivo é demonstrar boas práticas de desenvolvimento, escalabilidade e separação de responsabilidades usando .NET 9, Entity Framework e RabbitMQ.

### Arquitetura

- **Microserviço de Estoque:** Cadastro, consulta e atualização de produtos e quantidades em estoque.
- **Microserviço de Vendas:** Criação e consulta de pedidos, validação de estoque e notificação de vendas.
- **API Gateway:** Centraliza e roteia as requisições para os microserviços corretos.
- **RabbitMQ:** Comunicação assíncrona entre os microsserviços.
- **Autenticação JWT:** Protege os endpoints e garante acesso apenas a usuários autenticados.

### Tecnologias Utilizadas

- .NET 9
- Entity Framework Core
- RabbitMQ
- API Gateway (YARP)
- JWT Authentication
- PostgreSQL

### Funcionalidades Principais

- Cadastro e consulta de produtos
- Atualização automática de estoque após vendas
- Criação e consulta de pedidos
- Comunicação entre microsserviços via RabbitMQ
- Autenticação e autorização por JWT

### Estrutura do Projeto

- `src/services/MSEstoque`: Microserviço de gestão de estoque
- `src/services/MsVendas`: Microserviço de gestão de vendas
- `src/gateway/DesafioTecnicoEcommerce.ApiService`: API Gateway
- `src/shared`: Código compartilhado entre microsserviços

---

## Como Executar

### Subindo toda a aplicação com Aspire

Com Docker rodando, basta executar o comando abaixo na raiz do projeto para subir todos os microsserviços, banco de dados e dependências:

```powershell
dotnet aspire run
```

Esse comando irá orquestrar todos os serviços definidos no Aspire, incluindo containers Docker necessários (como RabbitMQ e PostgreSQL), e expor as URLs dos microsserviços e do gateway.

---