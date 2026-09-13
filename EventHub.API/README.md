# EventHub API

API REST desenvolvida em ASP.NET Core para gerenciamento de eventos, lotes de ingressos, pedidos, tickets e check-in.

O projeto foi criado com foco em arquitetura em camadas, autenticação JWT, autorização por roles, Entity Framework Core e MySQL.

## Tecnologias

* C#
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* JWT Authentication
* Swagger / OpenAPI
* Git
* GitHub

## Arquitetura

O projeto segue uma arquitetura em camadas:

```text
Controller
   ↓
Service
   ↓
Repository
   ↓
AppDbContext
   ↓
MySQL
```

### Controllers

Responsáveis por receber as requisições HTTP, encaminhar os dados para os services e retornar as respostas da API.

### Services

Responsáveis pelas regras de negócio da aplicação.

### Repositories

Responsáveis pelo acesso aos dados utilizando Entity Framework Core.

### DTOs

Utilizados para controlar os dados de entrada e saída da API.

## Funcionalidades

### Usuários

* Cadastro de usuário
* Login
* Hash de senha
* Autenticação JWT
* Roles `Admin` e `Customer`

### Eventos

* Criar evento
* Listar eventos
* Buscar evento por ID
* Atualizar evento
* Excluir evento
* Paginação
* Filtro por nome
* Filtro por localização
* Filtro por intervalo de datas

A criação, atualização e exclusão de eventos são permitidas apenas para usuários `Admin`.

### Lotes de ingressos

* Criar lote
* Listar lotes
* Buscar lote por ID
* Atualizar lote
* Excluir lote
* Controle de preço
* Controle de quantidade disponível
* Período de vendas
* Associação com evento

As operações de criação, atualização e exclusão são restritas a usuários `Admin`.

### Pedidos

* Criação de pedido
* Associação do pedido ao usuário autenticado
* Cálculo automático do valor total
* Validação da quantidade disponível
* Redução automática da quantidade do lote
* Consulta dos pedidos do usuário

### Tickets

* Geração automática após a criação do pedido
* Código único no formato `EVT-XXXXXXXX`
* Consulta dos tickets do usuário
* Check-in
* Bloqueio de reutilização do ticket

O check-in pode ser realizado apenas por usuários `Admin`.

## Autenticação

A API utiliza JWT Bearer Authentication.

O login é realizado através do endpoint:

```http
POST /api/users/login
```

Após o login, a API retorna um token JWT.

Para acessar endpoints protegidos, o token deve ser enviado no header:

```text
Authorization: Bearer SEU_TOKEN
```

No Swagger também é possível utilizar o botão `Authorize`.

## Roles

A aplicação possui dois níveis de acesso:

```text
Customer
Admin
```

### Customer

Pode:

* consultar eventos
* consultar lotes
* criar pedidos
* visualizar seus pedidos
* visualizar seus tickets

### Admin

Além das permissões anteriores, pode:

* criar eventos
* atualizar eventos
* excluir eventos
* criar lotes
* atualizar lotes
* excluir lotes
* realizar check-in de tickets

## Paginação

O endpoint de eventos suporta paginação.

Exemplo:

```http
GET /api/events?pageNumber=1&pageSize=10
```

Exemplo de resposta:

```json
{
  "currentPage": 1,
  "pageSize": 10,
  "totalItems": 25,
  "totalPages": 3,
  "items": []
}
```

O valor permitido para `pageSize` é de 1 até 100.

## Filtros de eventos

Os filtros podem ser utilizados individualmente ou combinados.

### Nome

```http
GET /api/events?name=Tech
```

### Localização

```http
GET /api/events?location=Londrina
```

### Intervalo de datas

```http
GET /api/events?startDate=2026-11-01&endDate=2026-11-30
```

### Combinando filtros

```http
GET /api/events?pageNumber=1&pageSize=10&name=Tech&location=Londrina&startDate=2026-01-01&endDate=2026-12-31
```

## Principais endpoints

### Users

```text
POST /api/users/register
POST /api/users/login
```

### Events

```text
GET    /api/events
GET    /api/events/{id}
POST   /api/events
PUT    /api/events/{id}
DELETE /api/events/{id}
```

### Ticket Batches

```text
GET    /api/ticketbatches
GET    /api/ticketbatches/{id}
POST   /api/ticketbatches
PUT    /api/ticketbatches/{id}
DELETE /api/ticketbatches/{id}
```

### Orders

```text
POST /api/orders
GET  /api/orders/me
```

### Tickets

```text
GET  /api/tickets/me
POST /api/tickets/check-in/{code}
```

## Tratamento global de exceções

A API possui middleware global para tratamento de exceções.

Principais respostas utilizadas:

```text
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
500 Internal Server Error
```

Exemplo:

```json
{
  "status": 400,
  "message": "Ticket has already been used."
}
```

## Banco de dados

O projeto utiliza MySQL com Entity Framework Core.

A estrutura do banco é controlada através de migrations.

Para aplicar as migrations:

```bash
dotnet ef database update
```

## Configuração do banco de dados

Configure a connection string utilizando User Secrets ou outro método seguro de configuração.

Exemplo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=eventhub_db;User=root;Password=SUA_SENHA"
  }
}
```

## Configuração JWT

Exemplo de configuração:

```json
{
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA",
    "Issuer": "EventHubAPI",
    "Audience": "EventHubClient"
  }
}
```

> Não publique senhas, connection strings reais ou chaves JWT no GitHub.

## Como executar o projeto

Clone o repositório:

```bash
git clone https://github.com/mauesjp/eventhub-api.git
```

Entre na pasta do projeto:

```bash
cd EventHub
```

Restaure as dependências:

```bash
dotnet restore
```

Configure o banco de dados e a chave JWT.

Aplique as migrations:

```bash
dotnet ef database update
```

Execute a aplicação:

```bash
dotnet run
```

Depois, acesse o endereço do Swagger informado no terminal.

## Status do projeto

Funcionalidades principais concluídas:

* CRUD de eventos
* Autenticação JWT
* Autorização por roles
* Lotes de ingressos
* Pedidos
* Geração de tickets
* Código único de ticket
* Check-in
* Bloqueio de reutilização
* Tratamento global de exceções
* Paginação
* Filtros
* Documentação Swagger

## Autor

João Pedro

