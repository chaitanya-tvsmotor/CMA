# CMA Marketing Agency - Architecture Documentation

## System Architecture Overview

### High-Level Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                         CLIENT LAYER                             │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │              Angular 18+ SPA                             │   │
│  │  ┌────────────┐  ┌────────────┐  ┌────────────┐        │   │
│  │  │   Public   │  │  Dealer    │  │ Supervisor │        │   │
│  │  │   Module   │  │  Module    │  │  Module    │        │   │
│  │  └────────────┘  └────────────┘  └────────────┘        │   │
│  │  ┌────────────┐  ┌────────────┐  ┌────────────┐        │   │
│  │  │Sales Agent │  │  Manager   │  │ Shared/Core│        │   │
│  │  │   Module   │  │  Module    │  │   Module   │        │   │
│  │  └────────────┘  └────────────┘  └────────────┘        │   │
│  └──────────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │ HTTP/REST + JWT
                            │
┌───────────────────────────┴─────────────────────────────────────┐
│                      API GATEWAY LAYER                           │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │          ASP.NET Core 10.0 Web API                       │   │
│  │  ┌────────────────────────────────────────────────────┐ │   │
│  │  │         Middleware Pipeline                        │ │   │
│  │  │  Authentication │ Authorization │ CORS │ Logging  │ │   │
│  │  └────────────────────────────────────────────────────┘ │   │
│  │  ┌─────────┐ ┌─────────┐ ┌──────────┐ ┌────────────┐  │   │
│  │  │  Auth   │ │Products │ │  Orders  │ │ Employees  │  │   │
│  │  │Controller│ │Controller│ │Controller│ │ Controller │  │   │
│  │  └─────────┘ └─────────┘ └──────────┘ └────────────┘  │   │
│  └──────────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────┴─────────────────────────────────────┐
│                     APPLICATION LAYER                            │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │              Business Logic Services                      │   │
│  │  ┌────────────┐  ┌─────────────┐  ┌─────────────┐       │   │
│  │  │   Product  │  │    Order    │  │  Employee   │       │   │
│  │  │  Service   │  │   Service   │  │  Service    │       │   │
│  │  └────────────┘  └─────────────┘  └─────────────┘       │   │
│  │  ┌────────────┐  ┌─────────────┐  ┌─────────────┐       │   │
│  │  │ Attendance │  │   Salary    │  │    Auth     │       │   │
│  │  │  Service   │  │   Service   │  │  Service    │       │   │
│  │  └────────────┘  └─────────────┘  └─────────────┘       │   │
│  │                                                            │   │
│  │              DTOs & Interfaces                            │   │
│  └──────────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────┴─────────────────────────────────────┐
│                      DOMAIN LAYER                                │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │               Domain Entities                             │   │
│  │  Product │ Category │ Brand │ Order │ Employee           │   │
│  │  Attendance │ Salary │ Vehicle │ Supplier                │   │
│  └──────────────────────────────────────────────────────────┘   │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │            Domain Value Objects & Enums                   │   │
│  │  EmployeeType │ OrderStatus │ AttendanceStatus           │   │
│  └──────────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────┴─────────────────────────────────────┐
│                   INFRASTRUCTURE LAYER                           │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │            Entity Framework Core                          │   │
│  │  ┌────────────────────────────────────────────────────┐  │   │
│  │  │         ApplicationDbContext                       │  │   │
│  │  │    - DbSets                                        │  │   │
│  │  │    - Configurations                                │  │   │
│  │  │    - Migrations                                    │  │   │
│  │  └────────────────────────────────────────────────────┘  │   │
│  │  ┌────────────────────────────────────────────────────┐  │   │
│  │  │         Repository Implementations                 │  │   │
│  │  └────────────────────────────────────────────────────┘  │   │
│  │  ┌────────────────────────────────────────────────────┐  │   │
│  │  │         External Services                          │  │   │
│  │  │    - Identity                                      │  │   │
│  │  │    - JWT Token Generation                          │  │   │
│  │  └────────────────────────────────────────────────────┘  │   │
│  └──────────────────────────────────────────────────────────┘   │
└───────────────────────────┬─────────────────────────────────────┘
                            │
┌───────────────────────────┴─────────────────────────────────────┐
│                      DATABASE LAYER                              │
│  ┌──────────────────────────────────────────────────────────┐   │
│  │              Microsoft SQL Server                         │   │
│  │  Tables: Products, Categories, Brands, Orders,           │   │
│  │          Employees, Attendance, Salaries, etc.           │   │
│  └──────────────────────────────────────────────────────────┘   │
└──────────────────────────────────────────────────────────────────┘
```

## Clean Architecture Layers

### 1. Domain Layer (Core)
- **Pure business logic**
- No dependencies on other layers
- Entities, Value Objects, Enums
- Domain events and specifications

### 2. Application Layer
- **Business use cases**
- DTOs (Data Transfer Objects)
- Service interfaces
- Depends only on Domain layer

### 3. Infrastructure Layer
- **Technical implementations**
- Data access (EF Core, Repositories)
- External services (Email, SMS)
- Identity and Authentication
- Depends on Application layer

### 4. API/Presentation Layer
- **User interface**
- Controllers
- Middleware
- Request/Response models
- Depends on Application and Infrastructure

## Database Schema

```
┌──────────────────────────────────────────────────────────────────┐
│                        DATABASE SCHEMA                            │
└──────────────────────────────────────────────────────────────────┘

Categories                      Brands
┌──────────────┐               ┌──────────────┐
│ Id (PK)      │◄──┐           │ Id (PK)      │◄──┐
│ Name         │   │           │ Name         │   │
│ Description  │   │           │ Description  │   │
│ ParentId (FK)├───┘           │ Country      │   │
│ IsActive     │               │ IsActive     │   │
└──────────────┘               └──────────────┘
       △                               △
       │                               │
       │                               │
Products ───────────────────────────────┘
┌──────────────────────┐
│ Id (PK)              │
│ Name                 │
│ Description          │
│ CategoryId (FK)      │
│ BrandId (FK)         │
│ Price                │
│ Dimensions           │
│ Weight               │
│ StockQuantity        │
│ ImageUrl             │
│ Specifications (JSON)│
└──────────────────────┘
       △
       │
       │
OrderItems                      Orders
┌──────────────┐               ┌──────────────────┐
│ Id (PK)      │               │ Id (PK)          │
│ OrderId (FK) ├──────────────►│ OrderNumber      │
│ ProductId(FK)│               │ OrderDate        │
│ Quantity     │               │ Status           │
│ UnitPrice    │               │ TotalAmount      │
│ TotalPrice   │               │ DealerId (FK)    │
└──────────────┘               │ SalesAgentId (FK)│
                               │ ManagerId (FK)   │
                               │ Notes            │
                               └──────────────────┘

AspNetUsers (ApplicationUser)
┌─────────────────────────┐
│ Id (PK)                 │
│ UserName                │
│ Email                   │
│ FirstName               │
│ LastName                │
│ PhoneNumber             │
│ Address                 │
│ EmployeeType (enum)     │◄────┐
│ IsActive                │     │
└─────────────────────────┘     │
       △                        │
       │                        │
       ├────────────────────────┤
       │                        │
Employees                       │
┌─────────────────────────┐     │
│ Id (PK)                 │     │
│ UserId (FK)             ├─────┘
│ EmployeeId              │
│ EmployeeType            │
│ HireDate                │
│ Department              │
│ Position                │
│ IsActive                │
└─────────────────────────┘
       △
       │
       ├───────────────────┬──────────────────┐
       │                   │                  │
Attendance            Salaries          Vehicles
┌─────────────┐      ┌─────────────┐   ┌──────────────┐
│ Id (PK)     │      │ Id (PK)     │   │ Id (PK)      │
│ EmployeeId  │      │ EmployeeId  │   │ VehicleNo    │
│ Date        │      │ Month       │   │ Make         │
│ CheckIn     │      │ Year        │   │ Model        │
│ CheckOut    │      │ BasicSalary │   │ AssignedTo   │
│ Status      │      │ Allowances  │   │ Status       │
│ Notes       │      │ Deductions  │   └──────────────┘
└─────────────┘      │ NetSalary   │
                     │ PayDate     │
                     │ Status      │
                     └─────────────┘

Suppliers                    PurchaseOrders
┌─────────────────┐         ┌──────────────────┐
│ Id (PK)         │◄────────┤ Id (PK)          │
│ Name            │         │ PONumber         │
│ ContactPerson   │         │ SupplierId (FK)  │
│ Email           │         │ ManagerId (FK)   │
│ Phone           │         │ PODate           │
│ Address         │         │ Status           │
│ IsActive        │         │ TotalAmount      │
└─────────────────┘         │ ExpectedDelivery │
                            └──────────────────┘
                                    △
                                    │
                            PurchaseOrderItems
                            ┌──────────────────┐
                            │ Id (PK)          │
                            │ PurchaseOrderId  │
                            │ ProductId (FK)   │
                            │ Quantity         │
                            │ UnitPrice        │
                            │ TotalPrice       │
                            └──────────────────┘
```

## User Roles and Permissions

```
┌─────────────────────────────────────────────────────────────────┐
│                      ROLE-BASED ACCESS CONTROL                   │
└─────────────────────────────────────────────────────────────────┘

Public (Anonymous)
├── View Products (without prices)
├── View About Us
└── View Contact Information

Dealer
├── View Products (with prices)
├── Create Orders
├── View Own Orders
└── Assign Orders to Sales Agent

Sales Agent
├── View Products (with prices)
├── View Assigned Orders
├── Update Order Status
└── Forward Orders to Manager

Manager
├── View All Orders from Sales Agents
├── Approve/Reject Orders
├── Create Purchase Orders
└── View Suppliers

Supervisor
├── Manage Products (CRUD)
├── Manage Categories & Brands
├── Manage Employees
├── Manage Vehicles
├── Manage Attendance
├── Manage Salaries
└── View All Orders

Other Employee Types
├── Driver: View assigned vehicles, update delivery status
├── Accountant: Manage salaries, view financial reports
└── Worker: View attendance, submit timesheets
```

## Data Flow Diagrams

### Order Processing Flow

```
Dealer                Sales Agent           Manager              System
  │                        │                    │                  │
  ├─1.Create Order────────►│                    │                  │
  │                        │                    │                  │
  │                        ├─2.Review Order────►│                  │
  │                        │                    │                  │
  │                        │                    ├─3.Approve Order──►│
  │                        │                    │                  │
  │                        │                    │                  ├─4.Update Inventory
  │                        │                    │                  │
  │                        │                    ├─5.Create PO─────►│
  │                        │                    │                  │
  │◄───────6.Notification──┴────────────────────┴──────────────────┤
  │                                                                 │
```

### Employee Attendance Flow

```
Employee              Supervisor             System              Database
  │                        │                    │                  │
  ├─1.Check In────────────►│                    │                  │
  │                        ├─2.Record──────────►│                  │
  │                        │                    ├─3.Save──────────►│
  │                        │                    │                  │
  ├─4.Check Out───────────►│                    │                  │
  │                        ├─5.Calculate Hours─►│                  │
  │                        │                    ├─6.Update────────►│
  │                        │                    │                  │
  │                        ├─7.Generate Report─►│                  │
  │◄───────8.Monthly Summary────────────────────┤                  │
```

## Security Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                      SECURITY LAYERS                             │
└─────────────────────────────────────────────────────────────────┘

1. Transport Security
   ├── HTTPS/TLS 1.3
   └── CORS Policy

2. Authentication
   ├── JWT Bearer Token
   ├── Token Expiration: 7 days
   └── Secure Token Storage (HttpOnly cookies recommended)

3. Authorization
   ├── Role-Based Access Control (RBAC)
   ├── Policy-Based Authorization
   └── Resource-Based Authorization

4. Data Protection
   ├── Password Hashing (ASP.NET Identity)
   ├── Sensitive Data Encryption
   └── SQL Injection Prevention (EF Core)

5. Application Security
   ├── Input Validation
   ├── XSS Prevention
   ├── CSRF Protection
   └── Rate Limiting
```

## Deployment Architecture

```
┌─────────────────────────────────────────────────────────────────┐
│                    DEPLOYMENT ARCHITECTURE                       │
└─────────────────────────────────────────────────────────────────┘

Production Environment
┌────────────────────────────────────────────────────────────────┐
│                                                                 │
│  ┌──────────────┐         ┌──────────────┐                    │
│  │   Load       │         │   Angular    │                    │
│  │  Balancer    ├────────►│     SPA      │                    │
│  │  (Nginx)     │         │   (Static)   │                    │
│  └──────────────┘         └──────────────┘                    │
│         │                                                       │
│         │                                                       │
│  ┌──────▼───────────────────────────────┐                     │
│  │      API Servers (Multiple)          │                     │
│  │  ┌─────────────┐  ┌─────────────┐   │                     │
│  │  │  API Node 1 │  │  API Node 2 │   │                     │
│  │  │ .NET 10 API │  │ .NET 10 API │   │                     │
│  │  └─────────────┘  └─────────────┘   │                     │
│  └────────────┬──────────────────────────┘                     │
│               │                                                 │
│  ┌────────────▼──────────────┐                                │
│  │     SQL Server Cluster     │                                │
│  │  ┌──────────┐ ┌──────────┐│                                │
│  │  │ Primary  │ │Secondary ││                                │
│  │  │  Node    │ │  Node    ││                                │
│  │  └──────────┘ └──────────┘│                                │
│  └────────────────────────────┘                                │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 10.0
- **Language**: C# 13
- **ORM**: Entity Framework Core 10
- **Database**: Microsoft SQL Server
- **Authentication**: ASP.NET Core Identity + JWT
- **API Documentation**: Swagger/OpenAPI

### Frontend
- **Framework**: Angular 18+
- **Language**: TypeScript 5.x
- **UI Library**: Angular Material
- **State Management**: RxJS
- **Build Tool**: Angular CLI
- **Styling**: SCSS

### DevOps
- **Version Control**: Git
- **CI/CD**: GitHub Actions
- **Containerization**: Docker (optional)
- **Hosting**: Azure App Service / IIS

## Scalability Considerations

1. **Horizontal Scaling**: Multiple API instances behind load balancer
2. **Database Optimization**: Indexing, query optimization, connection pooling
3. **Caching**: Redis for frequently accessed data
4. **CDN**: Static assets delivery
5. **Background Jobs**: Hangfire for long-running tasks
