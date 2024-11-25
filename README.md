# Company Management API  
[![NPM](https://img.shields.io/npm/l/react)](https://github.com/renansantosm/CompanyEmployeeManager/blob/master/LICENSE) 

# Sobre o projeto

A **Company Management API** é uma Web API RESTful projetada para simplificar a gestão de empresas e seus dados relacionados. A API fornece endpoints para lidar com as seguintes entidades:  
- **Empresas:** Gerencie informações como nome, telefone, e-mail e endereço associado.  
- **Endereços:** Armazene e gerencie informações de endereços.  
- **Funcionários:** Acompanhe informações de funcionários, incluindo nome, idade e a empresa e cargo associados.  
- **Cargos:** Defina diferentes funções dentro das empresas.  

### Principais Funcionalidades  
- Operações de criação, leitura, atualização e exclusão (CRUD) para todas as entidades.
- Retorno de dados relacionados entre entidades, permitindo consultas detalhadas entre empresas, endereços, funcionários e cargos.  
- Integração com banco de dados relacional usando **Entity Framework Core** e **MySQL**.  
- Dados de exemplo preenchidos automaticamente para facilitar os testes.  
- Integração com **Swagger UI** para exploração interativa da API.
- Paginação de dados para melhorar o desempenho e a usabilidade em grandes conjuntos de informações.  
- Autenticação e autorização utilizando **JSON Web Tokens (JWT)**.  
- Validações nas entradas de dados com **Fluent Validation**.  
- Arquitetura limpa com uso de serviços, repositórios e DTOs para melhor separação de responsabilidades.

Este projeto foi desenvolvido como um exercício de aprendizado e demonstra boas práticas para construção de APIs usando ASP.NET Core. 

# Tecnologias utilizadas
- C#
- ASP.NET Core
- Entity Framework Core
- MySQL
- NuGet 

# Como executar o projeto

## Pré-requisitos: 
- .NET 8 SDK
- MySQL Server
- MySQL Workbench (opcional, para gerenciamento do banco de dados)
- Editor de código como Visual Studio Code ou Visual Studio

### 1. Clone o repositório 
```bash
# clonar repositório
git clone https://github.com/renansantosm/CompanyEmployeeManager

# entrar na pasta do projeto 
cd CompanyEmployeeManager
```

### 2. Configure o Banco de Dados
Abra o arquivo **appsettings.json** e altere as configurações de conexão com o banco de dados para refletir o ambiente local (por exemplo, usuário e senha). O arquivo deve se parecer com o seguinte:
```C#
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=CompanyDB;Uid=your_user;Pwd=your_password;"
    }
```

### 3. Restaure as Dependências
No terminal, execute o seguinte comando para restaurar os pacotes NuGet:
``` bash
dotnet restore
```

### 4. Aplique as Migrações
Certifique-se de que você está na pasta principal do projeto onde o arquivo *.csproj está localizado. Caso não esteja, navegue até ela utilizando o comando:
``` bash
cd CompanyEmployeeManager
```
Em seguida, aplique as migrations para criar o banco de dados:
``` bash
dotnet ef database update
```

### 5. Compile e Execute a Aplicação
``` bash
dotnet run
```

### 6. Acesse a API
Após iniciar a aplicação, os endereços para acesso serão exibidos no console, como no exemplo abaixo:
``` bash
Now listening on: https://localhost:5083  
```
Você pode usar esses endereços para acessar a API:
``` bash
    Swagger UI: https://localhost:5083/swagger/index.html
```
# Autor

Renan Moreira 

https://www.linkedin.com/in/renanhsmoreira
