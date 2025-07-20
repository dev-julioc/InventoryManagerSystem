# 📦 Inventory Manager System

Sistema completo para gerenciamento de inventário, desenvolvido com **ASP.NET Core (API)** e **Blazor WebAssembly (Front-end)**, utilizando **Clean Architecture**, **JWT para autenticação**, e **Docker Compose** para orquestração de containers.

---

## 🧱 Estrutura do Projeto

A solução está dividida em duas aplicações principais:

### ✅ API - ASP.NET Core (.NET 8)

A API foi construída com base nos princípios da **Clean Architecture**, separando responsabilidades em diferentes camadas para garantir escalabilidade, testabilidade e manutenção simples.

**Principais Tecnologias e Conceitos:**

- ASP.NET Core 8 Web API  
- Entity Framework Core  
- PostgreSQL  
- JWT Authentication  
- Identity  
- FluentValidation  
- Swagger/OpenAPI  
- Repository Pattern  
- Service Result Pattern  
- MediatR (opcional)  

**Camadas:**

```
📂 InventoryManagerSystem.API           -> Apresentação da API (Controllers, Middlewares, Configurações)
📂 InventoryManagerSystem.Application   -> Regras de aplicação (Use Cases, DTOs, Validações)
📂 InventoryManagerSystem.Domain        -> Entidades e regras de negócio
📂 InventoryManagerSystem.Infra         -> Acesso a dados, autenticação, persistência
```

---

### 🎨 Front-End - Blazor WebAssembly

O front-end foi desenvolvido com **Blazor WebAssembly**, permitindo uma SPA (Single Page Application) moderna e responsiva, totalmente em C#.

**Principais Funcionalidades:**

- Integração com a API via `HttpClient`
- Autenticação via JWT (armazenado no `localStorage`)
- Autorização baseada em roles
- Componentes reutilizáveis com Razor
- Estilização com **Tailwind CSS**
- Formulários com validação

---

## 🐘 Banco de Dados - PostgreSQL

A aplicação utiliza PostgreSQL como banco de dados relacional.

- O banco é configurado e inicializado via Docker Compose.
- Migrations do Entity Framework Core garantem a criação das tabelas e estrutura correta.
- Já existe um usuário padrão criado para facilitar o acesso inicial à aplicação:

```
Email: admin@admin.com
Senha: Admin@123
```

---

## 🐳 Docker Compose

O sistema está containerizado com Docker Compose para facilitar a execução local.

**Serviços:**

- `api`: ASP.NET Core API
- `web`: Blazor WebAssembly
- `db`: PostgreSQL

**Comando para subir o ambiente:**

```bash
docker-compose up --build
```

---

## 🔐 Autenticação e Autorização

A autenticação é baseada em **JWT Tokens**, e a autorização é feita com base nas **roles** dos usuários.

- Tokens são gerados após o login e enviados via `Authorization: Bearer <token>`
- Gerenciamento de usuários feito com **ASP.NET Core Identity**
- Acesso restrito com base em políticas configuradas

---

## 📁 Funcionalidades

- ✅ Cadastro e login de usuários

---

## 🚧 Em Desenvolvimento

Funcionalidades planejadas:

- 📊 Relatórios gerenciais
- 📦 Leitor de código de barras
- 📈 Dashboard com gráficos (via Blazor)
-   Autorização por perfil (Admin, Estoquista etc.)
-   Visualização de inventário em tempo real
-   Cadastro, edição e exclusão de produtos
-   Controle de estoque (entradas e saídas)

---


## 📄 Licença

Este projeto está licenciado sob a **MIT License**.
