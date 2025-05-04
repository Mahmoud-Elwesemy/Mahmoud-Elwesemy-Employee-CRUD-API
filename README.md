🧠 Project Architecture & Technical Overview
✅ Architecture Pattern
The project follows the principles of Clean Architecture to ensure separation of concerns and maintainable, testable code. The solution is divided into distinct layers:

Domain – contains core business logic and entity models.

Application – includes use cases and service contracts.

Infrastructure – handles external concerns like database, file storage, and third-party integrations.

Presentation (API) – the Web API layer that exposes endpoints to the client.

This structure allows the project to scale and be adaptable to future changes.

🐢 Lazy Initialization
To improve performance and reduce resource usage, Lazy Initialization is applied where possible, especially in service registration and heavy-loading objects. This ensures that objects are only created when they are actually needed.

🔍 Search Functionality with Specification Pattern
The Specification Design Pattern is used to encapsulate complex query logic and filtering criteria, particularly in the search functionality. This promotes reusability, readability, and a clean separation between query definitions and execution logic.

🗃️ Database Strategy
The project uses Code-First approach with Entity Framework Core.

All database schema and relationships are defined through C# classes and migrations.

Supports automatic and versioned migrations.

☁️ Deployment
The application is deployed with its database on a Monester Server environment.

The backend is published and hosted on a cloud server, while the frontend (Angular) is deployed using Firebase Hosting.

