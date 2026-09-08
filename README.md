<h1 align="center">Sistema Gestão de Tarefas</h1>

<p align="center"> API para gerenciamento de pedidos e distribuição automática de tarefas entre funcionários. </p>

<p align="center"> <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" /> <img src="https://img.shields.io/badge/RabbitMQ-7.2.2-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white" /> <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" /> </p>

---

## Sobre o projeto

O Sistema Gestão de Tarefas é uma API desenvolvida como um projeto de estudo com o objetivo de aplicar conceitos de mensageria e processamento assíncrono utilizando RabbitMQ.

O sistema permite criar pedidos, que representam uma solicitação de trabalho dentro de uma empresa. Cada pedido possui uma descrição, uma área de atuação e um status.

Quando um pedido é criado, um evento é publicado em uma fila do RabbitMQ. Esse evento é posteriormente processado pela aplicação, que cria uma tarefa relacionada ao pedido e procura automaticamente um funcionário adequado para executá-la.

> ⚠️ Algumas funcionalidades e regras de negócio ainda estão sendo implementadas.

## Tecnologias
| Tecnologia | Utilização |
|---|---|
| [C# / .NET](https://dotnet.microsoft.com/languages/csharp) | Desenvolvimento da API |
| [ASP.NET Core](https://dotnet.microsoft.com/) | Construção dos endpoints REST |
| [RabbitMQ](https://www.rabbitmq.com/) | Sistema de mensageria |
| [Swagger / Swagger UI](https://swagger.io/) | Documentação e testes da API |
