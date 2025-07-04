<h1 align="center" style="font-weight: bold;">Company Management API 🏢 </h1>
<p align="center">
API backend para gerenciamento de empresas e dados relacionados, oferecendo funcionalidades para cadastro e administração de empresas, endereços, funcionários e cargos. Desenvolvida como meu primeiro projeto de portfólio, demonstra a aplicação de boas práticas iniciais para construção de APIs usando ASP.NET Core com uma arquitetura monolítica simples e eficaz.
</p>

## ✨ Funcionalidades Principais

* 🏢 **CRUD de Empresas**: Gerenciamento de informações como nome, telefone, e-mail e endereço
* 📍 **CRUD de Endereços**: Armazenamento e gestão de informações de localização
* 👥 **CRUD de Funcionários**: Acompanhamento de informações incluindo nome, idade, empresa e cargo
* 👔 **CRUD de Cargos**: Definição de diferentes funções dentro das empresas
* 🔐 **Autenticação e Autorização**: Sistema seguro utilizando Identity e JWT
* 📊 **Paginação de Dados**: Melhoria de desempenho e usabilidade em grandes conjuntos de informações
* ✅ **Validações**: Verificação das entradas de dados com Fluent Validation
* 📖 **Documentação**: Endpoints documentados com Swagger/OpenAPI
* 🐳 **Docker**: Containerização completa da aplicação e banco de dados
* 🔄 **Migrações Automáticas**: Banco de dados configurado automaticamente na inicialização

## 🛠️ Tecnologias Utilizadas

* **.NET 8** - Framework principal
* **Entity Framework Core** - ORM para acesso ao banco de dados
* **SQL Server** - Sistema de gerenciamento de banco de dados
* **ASP.NET Identity** - Framework para gerenciamento de usuários e autenticação
* **JWT** - Tokens para autorização e autenticação
* **Fluent Validation** - Biblioteca para validações
* **AutoMapper** - Mapeamento entre entidades e DTOs
* **Swagger/OpenAPI** - Documentação da API
* **Docker** - Containerização da aplicação

## 🏗️ Arquitetura e Padrões de Design

### Princípios Arquiteturais
* **Arquitetura Monolítica**: Design simples e direto para facilitar o desenvolvimento inicial
* **Injeção de Dependência**: Uso do container DI nativo do .NET
* **DTOs**: Transferência segura de dados entre camadas

### Padrões Implementados
* **Repository Pattern**: Abstração da camada de persistência
* **Service Layer**: Separação da lógica de negócios
* **AutoMapper**: Mapeamento automático entre entidades e DTOs

## 🔗 Endpoints Principais
```
/api/Auth - Registro, login e gerenciamento de usuários
/api/Addresses - CRUD completo de endereços
/api/Companies - CRUD completo de empresas
/api/Employees - CRUD completo de funcionários
/api/Positions – CRUD completo de cargos
```

## 🚀 Como Executar

### 📋 Pré-requisitos
- **Docker** instalado 
- **Git** para clonar o repositório

### 🐳 Execução com Docker (Recomendado)
A maneira mais rápida e fácil de executar a aplicação:

```bash
# Clone o repositório
git clone https://github.com/renansantosm/CompanyEmployeeManager
cd CompanyEmployeeManager

# Execute com Docker Compose
docker-compose up -d

# Aguarde alguns segundos para os containers iniciarem
# As migrações do banco são executadas automaticamente
# Acesse a documentação Swagger
# http://localhost:8081/swagger
```

**🐳 Informações dos Containers:**
- **API**: Disponível na porta `8081` (http://localhost:8081)
- **SQL Server**: Disponível na porta `1433` com as credenciais:
  - **Usuário**: `sa`
  - **Senha**: `Senha!2077`

### ⚙️ Execução Local (Desenvolvimento)
Para desenvolvimento e debugging:

**Pré-requisitos para execução local:**
- .NET 8.0 SDK ou superior
- SQL Server (local ou remoto)

```bash
# Clone o repositório
git clone https://github.com/renansantosm/CompanyEmployeeManager
cd CompanyEmployeeManager

# Restaure as dependências
dotnet restore
cd CompanyEmployeeManager

# Configure a string de conexão no appsettings.json
# Execute a aplicação (migrações são aplicadas automaticamente)
dotnet run

# Acesse a documentação Swagger
# http://localhost:5083/swagger
# https://localhost:7234/swagger
```
## 🔑 Autenticação

Para utilizar os endpoints protegidos, siga os passos:

1. Crie um novo usuário através do endpoint `/api/Auth/register`
2. Obtenha um token JWT através do endpoint `/api/Auth/login`
3. Inclua o token no cabeçalho de autorização das requisições:

```http
Authorization: Bearer seu_token_jwt
```

## 📈 Jornada de Desenvolvimento

Este projeto marcou o início da minha jornada como desenvolvedor de APIs RESTful, representando meu primeiro projeto de portfólio estruturado. As lições e experiências adquiridas aqui serviram como base para projetos mais complexos posteriormente, como a **Healthcare Management API**, onde apliquei princípios mais avançados de arquitetura limpa e DDD.
