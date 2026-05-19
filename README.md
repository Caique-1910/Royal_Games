👑 Royal Games API

API desenvolvida em C# utilizando ASP.NET no Visual Studio Community, estruturada com o padrão Domain-Driven Design (DDD).

A API fornece endpoints REST para gerenciamento de dados da plataforma Royal Games, com documentação automática gerada pelo Swagger UI e testes realizados com Insomnia REST Client.

📌 Sobre o Projeto

O Royal Games é uma API que permite gerenciar informações relacionadas a jogos dentro de uma plataforma digital.

O projeto foi desenvolvido utilizando o padrão DDD (Domain-Driven Design), que organiza a aplicação em camadas bem definidas, separando responsabilidades entre domínio, aplicação e infraestrutura.

A API segue o padrão REST, permitindo integração com:

aplicações web

aplicativos mobile

outros sistemas

🏗 Arquitetura

O projeto utiliza o padrão Domain-Driven Design (DDD) para estruturar o sistema.

Principais camadas:

📦 Domain

Contém as regras de negócio da aplicação.

Entidades

Interfaces de repositório

Objetos de valor

Regras do domínio

📦 Application

Responsável pelos casos de uso da aplicação.

Services

DTOs

Regras de aplicação

📦 Infrastructure

Responsável pela comunicação com recursos externos.

Banco de dados

Implementação de repositórios

Configurações

📦 API

Camada responsável pela exposição dos endpoints.

Controllers

Configuração da API

Middleware

🛠 Tecnologias Utilizadas

C#

.NET

ASP.NET Web API

Visual Studio Community

Swagger UI

Insomnia REST Client

📂 Estrutura do Projeto

Exemplo de organização utilizando DDD:
```text
RoyalGames
│
├── RoyalGames.API
│   ├── Controllers
│   └── Program.cs
│
├── RoyalGames.Application
│   ├── Services
│   └── DTOs
│
├── RoyalGames.Domain
│   ├── Entities
│   ├── Interfaces
│   └── ValueObjects
│
├── RoyalGames.Infrastructure
│   ├── Repositories
│   └── Data
```
🚀 Como Executar o Projeto

1️⃣ Clonar o repositório
```cmd
git clone https://github.com/seu-usuario/royal-games.git
```
2️⃣ Abrir o projeto

Abra o projeto no Visual Studio Community.

3️⃣ Executar a aplicação

Pressione:

F5

ou

Ctrl + F5

📑 Documentação da API

A documentação é gerada automaticamente pelo Swagger UI.

Após iniciar o projeto, acesse:

````cmd
https://localhost:xxxx/swagger
`````

No Swagger é possível:

visualizar todos os endpoints

testar requisições

verificar parâmetros

visualizar respostas da API

🧪 Testes da API

Os endpoints da API foram testados utilizando o Insomnia REST Client, permitindo simular requisições HTTP como:

GET

POST

PUT

DELETE

🎯 Objetivo do Projeto

O projeto foi desenvolvido com o objetivo de:

aplicar conceitos de Domain-Driven Design (DDD)

desenvolver APIs REST com C#

utilizar Swagger para documentação

realizar testes de endpoints com Insomnia

👨‍💻 Autores

Desenvolvido por:

Caique Lima Alves
Arthur Sales
