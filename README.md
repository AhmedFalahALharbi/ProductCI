# Product Catalog System

A full-stack application consisting of a .NET 8 backend API and a standalone Angular frontend for product management.

## Project Overview

This solution demonstrates a modern web application architecture with:
- .NET 8 Web API with dependency injection
- Angular frontend consuming the API
- Unit and integration tests
- CI/CD pipeline using GitHub Actions

## Solution Architecture

```
ProductSolution/
├── ProductApi/                   # Backend API
│   ├── Controllers/              # API controllers
│   │   └── ProductsController.cs
│   ├── Models/                   # Domain models
│   │   └── Product.cs
│   ├── Services/                 # Business logic layer
│   │   ├── IProductService.cs
│   │   └── ProductService.cs
│   ├── Configuration/            # Configuration classes
│   │   └── AppSettings.cs
│   ├── Properties/
│   ├── appsettings.json          # App configuration
│   ├── appsettings.Development.json  # Development config
│   ├── Program.cs                # Application entry point
│   └── ProductApi.csproj         # Project file
│
├── ProductApi.Tests/             # Unit and integration tests
│   ├── ProductServiceTests.cs    # Service unit tests
│   ├── ProductsControllerTests.cs # Controller unit tests
│   ├── ProductApiIntegrationTests.cs # Integration tests
│   └── ProductApi.Tests.csproj   # Test project file
│
├── product-frontend/             # Angular frontend application
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   │   ├── product-list/
│   │   │   │   └── product-detail/
│   │   │   ├── services/
│   │   │   │   └── product.service.ts
│   │   │   ├── models/
│   │   │   │   └── product.ts
│   │   │   ├── app.component.ts
│   │   │   ├── app.component.html
│   │   │   ├── app.component.scss
│   │   │   └── app.module.ts
│   │   ├── environments/
│   │   │   └── environment.ts
│   │   └── ...
│   └── ...
│
├── .github/
│   └── workflows/
│       └── ci.yml               # GitHub Actions CI workflow
│
└── ProductSolution.sln          # Solution file
```

## Backend Features

- **API Endpoints:**
  - GET /api/products: List all products
  - GET /api/products/{id}: Get a single product by ID
  - POST /api/products: Add a product
- **In-memory Product Database:** Uses a static list for product storage
- **Dependency Injection:** Implements a service layer for managing product logic
- **Configuration:** Reads AppName and DefaultCurrency from appsettings.json
- **Cross-Origin Resource Sharing (CORS):** Configured to allow the Angular app to access the API

## Frontend Features

- **Product List View:** Displays all products with basic information
- **Product Detail View:** Shows detailed information for a single product
- **Responsive Design:** Works well on different screen sizes
- **Environment Configuration:** Separate configurations for development and production

## Setup Instructions

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (LTS version)
- [Angular CLI](https://cli.angular.io/)

### Backend Setup

```bash
# Clone the repository
git clone https://github.com/AhmedFalahALharbi/ProductCI.git
cd ProductCI

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the tests
dotnet test
```

### Frontend Setup

```bash
# Navigate to the frontend directory
cd product-frontend

# Install dependencies
npm install

# Configure API URL
# Open src/environments/environment.ts and update apiUrl if needed
```

## Running the Application

### Backend

```bash
# From the ProductApi directory
cd ProductApi
dotnet run


```

### Frontend

```bash
# From the product-frontend directory
cd product-frontend
ng serve

# The application will be available at:
# http://localhost:4200
```

## CI/CD Pipeline

The project uses GitHub Actions for continuous integration:

- **Workflow File:** `.github/workflows/ci.yml`
- **Triggered On:** Push or pull request to main, master, or develop branches
- **Jobs:**
  - Build the .NET Core solution
  - Restore dependencies
  - Run unit tests
  - Generate and store test reports

## License

[MIT License](LICENSE)

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
