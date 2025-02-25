# Bookify

Bookify is a .NET 9.0 based application designed to manage book-related operations. This project is structured into multiple layers to ensure separation of concerns and maintainability.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022
- PostgreSQL
- Redis

### Installation

1. Clone the repository:
git clone https://github.com/scaber/bookify.git
cd bookify


2. Restore the dependencies:
    dotnet restore
	
	
3. Update the database connection strings in `appsettings.json` files.

4. Apply the database migrations:
    
	dotnet ef database update
	
	
### Usage

To run the application, use the following command:

## Project Structure

The project is divided into several layers:

- **Bookify.Api**: The entry point of the application, responsible for handling HTTP requests.
- **Bookify.Application**: Contains the business logic and application services.
- **Bookify.Domain**: Contains the domain entities and domain services.
- **Bookify.Infrastructure**: Contains the implementation of infrastructure services like data access, authorization, etc.
 


## Authorization

The authorization mechanism in Bookify is designed to ensure that users have the appropriate permissions to perform actions within the system. The key components involved in authorization are:

### PermissionAuthorizationHandler

This class handles the authorization logic based on user roles and permissions. It checks the user's permissions for specific actions (GET, PUT, POST, PATCH, DELETE) on different controllers.

### AuthorizationService

This service interacts with the database to retrieve user roles and permissions. It also caches the permissions to improve performance.

### CustomClaimsTransformation

This class is responsible for transforming the claims of the authenticated user to include roles and permissions. It ensures that the user's claims are up-to-date with the latest roles and permissions.

### RolePermissionController

This controller provides endpoints to manage role permissions. It allows updating permissions for specific roles.

### Example Usage

To protect a controller action with a specific permission, you can use the `[Authorize]` attribute with a custom requirement:


The `PermissionAuthorizationHandler` will then ensure that the user has the necessary permissions to access the action.

 
 