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

## 🐳 Quick Start with Docker (Recommended)

The easiest way to run the application is using Docker:

```bash
# Clone the repository
git clone https://github.com/chaitanya-tvsmotor/CMA.git
cd CMA

# Start all services (SQL Server, Backend API, Frontend)
docker-compose up --build

# Access the application
# Frontend: http://localhost:4200
# Backend API: http://localhost:5000
# Swagger: http://localhost:5000/swagger
```

**Default Credentials:**
- Supervisor: `supervisor` / `Supervisor@123`
- Manager: `manager` / `Manager@123`

For detailed Docker instructions, see [DOCKER-GUIDE.md](DOCKER-GUIDE.md).

## 📋 Prerequisites (Manual Setup)

### Backend
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server LocalDB

### Frontend
- [Node.js](https://nodejs.org/) (v20.x+)
- [npm](https://www.npmjs.com/) (v10.x+)

## 🛠️ Manual Setup Instructions

### Backend Setup

```bash
# Clone and navigate
git clone https://github.com/chaitanya-tvsmotor/CMA.git
cd CMA

# Restore and build
dotnet restore
dotnet build

# Update connection string in src/CMA.API/appsettings.json

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

# Update API URL in service files if needed
# Default is http://localhost:5000/api

# Run development server
npm start
```

Application: `http://localhost:4200`

### SQL Server in Docker

If you want to use SQL Server in Docker for development:

```bash
# Start SQL Server
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 --name sql-server -d \
  mcr.microsoft.com/mssql/server:2022-latest

# Update connection string in appsettings.json:
# Server=localhost,1433;Database=CMADb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True
```

## 🔑 Default Credentials

### Supervisor
- Username: `supervisor`
- Password: `Supervisor@123`

### Manager
- Username: `manager`
- Password: `Manager@123`

## 📊 Key Features

### Products
- Hierarchical categories
- Brand management  
- Dimensions (Length, Width, Height, Weight)
- SKU and barcode tracking
- Stock management

### Orders
- Multi-level approval workflow (Dealer → Sales Agent → Manager)
- Status tracking
- Order history
- Detailed order items

### Employee Management
- Multiple employee types (Supervisor, Sales Agent, Manager, Driver, Accountant, Worker)
- Attendance tracking with check-in/check-out
- Salary management with detailed breakdowns
- Vehicle assignments

### Purchase Orders
- Manager-created purchase orders to suppliers
- Item-level tracking
- Delivery date management

## 🔐 Security

- JWT authentication with 7-day expiration
- Role-based authorization
- Password hashing with ASP.NET Identity
- HTTPS enforcement
- CORS configuration
- XSS and CSRF protection

## 📝 API Documentation

Swagger UI available at:
- Docker: `http://localhost:5000/swagger`
- Manual: `https://localhost:5001/swagger`

## 🏗️ Project Structure

```
CMA/
├── src/
│   ├── CMA.API/              # Web API (Controllers, Program.cs)
│   ├── CMA.Application/      # DTOs, Interfaces
│   ├── CMA.Domain/           # Entities, Enums
│   └── CMA.Infrastructure/   # Data, Services, EF Core
├── cma-client/               # Angular frontend
│   ├── src/app/
│   │   ├── components/       # UI components by role
│   │   ├── services/         # HTTP services
│   │   ├── guards/           # Route guards
│   │   └── models/           # TypeScript interfaces
│   └── ...
├── docker-compose.yml        # Docker orchestration
├── Dockerfile.backend        # Backend container
├── Dockerfile.frontend       # Frontend container
├── ARCHITECTURE.md           # Architecture documentation
├── DOCKER-GUIDE.md          # Docker setup guide
└── README.md                 # This file
```

## �� Testing

### Backend Tests
```bash
dotnet test
```

### Frontend Tests
```bash
cd cma-client
npm test
```

## 📦 Building for Production

### Using Docker
```bash
docker-compose build
```

### Manual Build

**Backend:**
```bash
dotnet publish -c Release -o ./publish
```

**Frontend:**
```bash
cd cma-client
ng build --configuration production
# Output in dist/cma-client/browser/
```

## 🚀 Deployment Options

### Docker Deployment (Recommended)
- Use docker-compose.yml for orchestration
- Configure environment variables for production
- Use external database for data persistence

### Traditional Deployment
- **Backend**: Deploy to IIS, Azure App Service, or Linux server with Kestrel
- **Frontend**: Serve with Nginx, Apache, or Azure Static Web Apps
- **Database**: Azure SQL, AWS RDS, or on-premise SQL Server

## 🔧 Configuration

### Environment Variables

**Backend (appsettings.json or environment):**
- `ConnectionStrings__DefaultConnection` - Database connection
- `Jwt__Key` - JWT secret key
- `Jwt__Issuer` - JWT issuer
- `Jwt__Audience` - JWT audience

**Frontend (environment.ts):**
- `apiUrl` - Backend API URL

## 📊 Database Management

### Migrations
```bash
# Create migration
cd src/CMA.Infrastructure
dotnet ef migrations add MigrationName --startup-project ../CMA.API

# Apply migration
dotnet ef database update --startup-project ../CMA.API

# Remove last migration
dotnet ef migrations remove --startup-project ../CMA.API
```

### Backup (Docker)
```bash
docker exec cma-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P YourStrong@Passw0rd \
  -Q "BACKUP DATABASE CMADb TO DISK='/var/opt/mssql/backup/CMADb.bak'"
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Support

For support:
- Create an issue in the GitHub repository
- Email: support@cma.com
- Documentation: See ARCHITECTURE.md and DOCKER-GUIDE.md

## 🙏 Acknowledgments

- Built with [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- Frontend powered by [Angular](https://angular.io/)
- UI components from [Angular Material](https://material.angular.io/)
- Database management with [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- Containerization with [Docker](https://www.docker.com/)

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

**CMA Marketing Agency** - Streamlining automotive marketing and sales operations
