# CMA Marketing Agency - Full Stack Application

A comprehensive marketing agency management system built with .NET 10 and Angular 18+, implementing Clean Architecture principles.

## 🏗️ Architecture

This project follows **Clean Architecture** with clear separation of concerns. For detailed architecture documentation, see [ARCHITECTURE.md](ARCHITECTURE.md).

## 🚀 Features

### User Roles & Capabilities

- **Public Users**: View products (without prices), access About Us and Contact pages
- **Dealer**: View products with pricing, create and manage orders
- **Sales Agent**: View assigned orders, forward to managers
- **Manager**: Review orders, create purchase orders to suppliers
- **Supervisor**: Full system administration, manage products, employees, vehicles, attendance, salaries
- **Accountant**: Manage salaries, financial reports
- **Driver**: View assigned vehicles, update delivery status
- **Worker**: View attendance, submit timesheets

## 📋 Prerequisites

### Backend
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server LocalDB

### Frontend
- [Node.js](https://nodejs.org/) (v20.x+)
- [npm](https://www.npmjs.com/) (v10.x+)

## 🛠️ Setup Instructions

### Backend Setup

```bash
# Clone and navigate
git clone https://github.com/chaitanya-tvsmotor/CMA.git
cd CMA

# Restore and build
dotnet restore
dotnet build

# Run migrations
cd src/CMA.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../CMA.API
dotnet ef database update --startup-project ../CMA.API

# Run the API
cd ../CMA.API
dotnet run
```

API endpoints:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `https://localhost:5001/swagger`

### Frontend Setup

```bash
# Navigate and install
cd cma-client
npm install

# Run development server
npm start
```

Application: `http://localhost:4200`

## 🔑 Default Credentials

### Supervisor
- Username: `supervisor`
- Password: `Supervisor@123`

### Manager
- Username: `manager`
- Password: `Manager@123`

## 📊 Key Entities

- **Products**: Categories, brands, dimensions
- **Orders**: Status tracking, multi-level approval
- **Employees**: Multiple types with attendance and salary management
- **Vehicles**: Fleet management
- **Suppliers & Purchase Orders**

## 🔐 Security

- JWT authentication
- Role-based authorization
- Password hashing
- HTTPS enforcement
- CORS configuration

## 📝 API Documentation

Swagger UI: `https://localhost:5001/swagger`

## 🏗️ Project Structure

```
CMA/
├── src/
│   ├── CMA.API/              # Web API
│   ├── CMA.Application/      # DTOs, Interfaces
│   ├── CMA.Domain/           # Entities, Enums
│   └── CMA.Infrastructure/   # Data, Services
├── cma-client/               # Angular frontend
├── ARCHITECTURE.md           # Architecture docs
└── README.md
```

## 📧 Support

Create an issue in the GitHub repository for support.

---

**CMA Marketing Agency** - Streamlining automotive marketing and sales operations
