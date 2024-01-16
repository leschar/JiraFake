# Jira Fake

## Pré-requisitos
- Visual Studio com .NET 6 SDK instalado
- SQL Server
- Docker para o RabbitMQ (siga os passos abaixo)
  ```bash
  docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management

## Estrutura do projeto
.
├── docs
│   └── readme.md
├── src
│   ├── Api
│   │   └── Controllers
│   ├── Core
│   │   ├── Application
│   │   │   ├── Adapters
│   │   │   ├── ViewModels
│   │   │   └── Worker
│   │   └── Domain
│   │       ├── AppSettings
│   │       ├── Commands
│   │       ├── Communications
│   │       ├── DomainObjects
│   │       ├── Enum
│   │       ├── Events
│   │       ├── Extensions
│   │       ├── Interfaces
│   │       ├── Mediator
│   │       ├── Messages
│   │       ├── Models
│   │       └── Utils
│   └── Infra
│       └── Data
│           ├── Context
│           ├── Extensions
│           ├── Mappings
│           └── Repositories
└── tests
    └── Unit

## Instruções de instalação

Clone o repositório
git clone https://github.com/leschar/JiraFake

## Execução

Para executar o projeto, siga estas etapas:
Abra a solução no Visual Studio.
Configure e inicie o RabbitMQ usando Docker Desktop para Windows.
Configure o SQL Server e execute as migrações.
Alterar as connectionsstrings no arquivo appsetings se necessário passando os dados da maquina.
Execute o projeto.

## Testes
Em desenvolvimento

## Autor
**Charles Albert Fernandes**
