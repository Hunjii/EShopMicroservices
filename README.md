# EShopMicroservices

### Catalog Microservice

- **ASP.NET Core Minimal APIs** and latest features of .NET 8 and C# 12
- **Vertical Slice Architecture** implementation with Feature folders
- **CQRS implementation** using MediatR library
- **CQRS Validation Pipeline Behaviours** with MediatR and FluentValidation
- **Marten library** for .NET Transactional Document DB on PostgreSQL
- **Carter library** for Minimal API endpoint definition
- Cross-cutting concerns: Logging, global Exception Handling, and Health Checks
- **Dockerfile** and **docker-compose** file for running Multi-container in Docker environment

### Basket Microservice

- **ASP.NET 8 Web API** application, Following REST API principles, CRUD operations
- **Redis** as a Distributed Cache over basketdb
- Implements **Proxy**, **Decorator**, and **Cache-aside Design Patterns**
- Consume Discount **gRPC Service** for inter-service sync communication to calculate product final price
- Publish BasketCheckout Queue using **MassTransit** and **RabbitMQ**

### Discount Microservice

- **ASP.NET gRPC Server** application
- Build a Highly Performant inter-service gRPC Communication with Basket Microservice
- Exposing gRPC Services by creating **Protobuf messages**
- **Entity Framework Core ORM** - SQLite Data Provider and Migrations
- **SQLite database connection** and containerization

### Microservices Communication

- Sync inter-service gRPC Communication
- Async Microservices Communication with **RabbitMQ Message-Broker Service**
- Using **RabbitMQ Publish/Subscribe Topic Exchange Model**
- Using **MassTransit** for abstraction over RabbitMQ Message-Broker system
- Publishing BasketCheckout event queue from Basket microservices and Subscribing to this event from Ordering microservices
- Create RabbitMQ **EventBus.Messages library** and add references Microservices

### Ordering Microservice

- Implementing **DDD, CQRS, and Clean Architecture** using Best Practices
- Developing CQRS using **MediatR, FluentValidation, and Mapster** packages
- Use **Domain Events & Integration Events**
- **Entity Framework Core** Code-First Approach, Migrations, DDD Entity Configurations
- Consuming RabbitMQ BasketCheckout event queue using **MassTransit-RabbitMQ Configuration**
- **SqlServer database connection** and containerization
- Using Entity Framework Core ORM and auto-migrate to SqlServer on application startup

### Yarp API Gateway Microservice

- Implement API Gateways with **Yarp Reverse Proxy** applying Gateway Routing Pattern
- Yarp Reverse Proxy Configuration: Route, Cluster, Path, Transform, Destinations
- Rate Limiting with **FixedWindowLimiter** on Yarp Reverse Proxy Configuration
- Sample microservices/containers to reroute through the API Gateways

### Docker Compose Establishment

- Containerization of microservices
- Orchestrating microservices and backing services (databases, distributed caches, message brokers..)
- Override Environment variables
