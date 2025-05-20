# Company Management API 🏢

API backend para gerenciamento de empresas e dados relacionados, oferecendo funcionalidades para cadastro e administração de empresas, endereços, funcionários e cargos. Desenvolvida como meu primeiro projeto de portfólio, demonstra a aplicação de boas práticas iniciais para construção de APIs usando ASP.NET Core com uma arquitetura monolítica simples e eficaz.

## ✨ Funcionalidades Principais

* 🏢 **CRUD de Empresas**: Gerenciamento de informações como nome, telefone, e-mail e endereço
* 📍 **CRUD de Endereços**: Armazenamento e gestão de informações de localização
* 👥 **CRUD de Funcionários**: Acompanhamento de informações incluindo nome, idade, empresa e cargo
* 👔 **CRUD de Cargos**: Definição de diferentes funções dentro das empresas
* 🔐 **Autenticação e Autorização**: Sistema seguro utilizando Identity e JWT
* 📊 **Paginação de Dados**: Melhoria de desempenho e usabilidade em grandes conjuntos de informações
* ✅ **Validações**: Verificação das entradas de dados com Fluent Validation
* 📖 **Documentação**: Endpoints documentados com Swagger/OpenAPI

## 🛠️ Tecnologias Utilizadas

* **.NET 8** - Framework principal
* **Entity Framework Core** - ORM para acesso ao banco de dados
* **SQL Server** - Sistema de gerenciamento de banco de dados
* **ASP.NET Identity** - Framework para gerenciamento de usuários e autenticação
* **JWT** - Tokens para autorização e autenticação
* **Fluent Validation** - Biblioteca para validações
* **AutoMapper** - Mapeamento entre entidades e DTOs
* **Swagger/OpenAPI** - Documentação da API

## 🏗️ Arquitetura e Padrões de Design

### Princípios Arquiteturais
* **Arquitetura Monolítica**: Design simples e direto para facilitar o desenvolvimento inicial
* **Injeção de Dependência**: Uso do container DI nativo do .NET
* **DTOs**: Transferência segura de dados entre camadas

### Padrões Implementados
* **Repository Pattern**: Abstração da camada de persistência
* **Service Layer**: Separação da lógica de negócios
* **AutoMapper**: Mapeamento automático entre entidades e DTOs

## 🚀 Como Executar

1. 📋 **Pré-requisitos**
   - .NET 8.0 SDK ou superior
   - SQL Server (local ou remoto)
   - Git

### ⚙️ Instalação e Execução

```bash
# Clone o repositório
git clone https://github.com/CompanyEmployeeManager
cd CompanyEmployeeManager

# Restaure as dependências
dotnet restore

# Configure a string de conexão no appsettings.json
# Execute as migrações do banco
dotnet ef database update 

# Execute a aplicação
dotnet run

# Acesse a documentação Swagger
# # [http://localhost:5083/swagger]
# # [https://localhost:7234/swagger]
```
