
# POS System using C#, ASP.NET 7, and Onion Architecture

Welcome to the Point of Sale (POS) System project developed in C# with ASP.NET 7 and following the Onion Architecture pattern.

## Overview

The POS System is designed to streamline the sales and inventory management process for businesses. It provides a user-friendly interface for managing products, orders, and customer data. This project is structured following the Onion Architecture pattern, which promotes a clean and maintainable codebase.

## Introduction
Project Name: PosSystem
Language: C# with ASP.NET 7
Architecture: Onion Architecture

## Project Structure

Contract: This layer contains interfaces or contracts defining the interfaces for services, entities, and application logic.

Entity: The Entity layer contains definitions for objects and entities in the system, such as Product, Order, User, etc.

Repository: This layer houses classes responsible for data access from the database and performs CRUD operations related to entities and objects.

Service: The Service layer contains the main business logic services of the application, including specific service classes like ProductService, OrderService, etc.

Contract.Service: In this layer, you'll find contracts and interfaces for business services in the Service layer. It provides the ability to connect the Service layer with the Presentation layer.

Presentation: This layer comprises user interface components and API endpoints (controllers). It handles user interaction and data transformation between the source and display formats.

Shared: The Shared layer contains objects and logic that are shared throughout the project. This includes utility classes, support classes, and Data Transfer Objects (DTOs).

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

1. Clone the repository:
  git clone https://github.com/bugM4ker/PosSystem.git

2. Navigate to the project directory:
  cd PosSystem

3. Restore packages and build the project:
  dotnet restore

4. Run the application:
  dotnet run

## Configuration
Database connection strings can be configured in appsettings.json.
Jwt and Db can be configured in Program.cs.

## Acknowledgments
This project is inspired by the Onion Architecture pattern.
Thanks to the ASP.NET community for their valuable resources and documentation.
