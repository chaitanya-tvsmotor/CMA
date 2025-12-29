# CMA Marketing Agency - Docker Setup Guide

This guide explains how to run the CMA Marketing Agency application using Docker containers.

## Prerequisites

- [Docker](https://docs.docker.com/get-docker/) (v20.10+)
- [Docker Compose](https://docs.docker.com/compose/install/) (v2.0+)
- At least 4GB RAM available for Docker
- Ports 1433, 4200, and 5000 available

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/chaitanya-tvsmotor/CMA.git
cd CMA
```

### 2. Start All Services

```bash
docker-compose up --build
```

This command will:
- Build and start SQL Server container
- Build and start .NET backend API container
- Build and start Angular frontend container

### 3. Access the Application

Once all containers are running:

- **Frontend (Angular)**: http://localhost:4200
- **Backend API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger
- **SQL Server**: localhost:1433

### 4. Login Credentials

Use these default credentials to test:

**Supervisor:**
- Username: `supervisor`
- Password: `Supervisor@123`

**Manager:**
- Username: `manager`
- Password: `Manager@123`

## Individual Service Management

### Start Services in Detached Mode

```bash
docker-compose up -d
```

### Stop Services

```bash
docker-compose down
```

### Stop and Remove Volumes (Clean Start)

```bash
docker-compose down -v
```

### View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f sqlserver
```

### Restart a Specific Service

```bash
docker-compose restart backend
docker-compose restart frontend
```

## Service Details

### SQL Server Container

**Image**: `mcr.microsoft.com/mssql/server:2022-latest`  
**Port**: 1433  
**Credentials**:
- User: `sa`
- Password: `YourStrong@Passw0rd`

**Connect using SQL Server Management Studio (SSMS)**:
- Server: `localhost,1433`
- Authentication: SQL Server Authentication
- Login: `sa`
- Password: `YourStrong@Passw0rd`

### Backend API Container

**Technology**: .NET 10  
**Port**: 5000  
**Base URL**: http://localhost:5000  
**API Documentation**: http://localhost:5000/swagger

**Environment Variables**:
- `ASPNETCORE_ENVIRONMENT=Development`
- `ConnectionStrings__DefaultConnection` - SQL Server connection
- `Jwt__Key` - JWT secret key

### Frontend Container

**Technology**: Angular 18 + Nginx  
**Port**: 4200 (mapped to 80 inside container)  
**URL**: http://localhost:4200

## Development Workflow

### Rebuild After Code Changes

#### Backend Only
```bash
docker-compose up -d --build backend
```

#### Frontend Only
```bash
docker-compose up -d --build frontend
```

#### All Services
```bash
docker-compose up -d --build
```

### Access Container Shell

```bash
# Backend
docker exec -it cma-backend /bin/bash

# SQL Server
docker exec -it cma-sqlserver /bin/bash

# Frontend
docker exec -it cma-frontend /bin/sh
```

### Run Database Migrations

The backend automatically runs migrations on startup. To run them manually:

```bash
docker exec -it cma-backend dotnet ef database update --project /src/src/CMA.Infrastructure --startup-project /src/src/CMA.API
```

## Troubleshooting

### SQL Server Not Starting

**Issue**: SQL Server container exits immediately

**Solution**:
1. Ensure at least 2GB RAM is allocated to Docker
2. Check Docker logs: `docker-compose logs sqlserver`
3. Verify password meets complexity requirements

### Backend Cannot Connect to SQL Server

**Issue**: Backend shows connection errors

**Solution**:
1. Wait 30 seconds after SQL Server starts (initialization time)
2. Check if SQL Server is healthy: `docker-compose ps`
3. Verify connection string in docker-compose.yml

### Frontend Cannot Connect to Backend

**Issue**: API calls fail from frontend

**Solution**:
1. Verify backend is running: `curl http://localhost:5000/api/products`
2. Check nginx configuration in `nginx.conf`
3. Review browser console for CORS errors

### Port Already in Use

**Issue**: Error binding to port

**Solution**:
```bash
# Check what's using the port
# On Linux/Mac
lsof -i :4200
lsof -i :5000
lsof -i :1433

# On Windows
netstat -ano | findstr :4200
netstat -ano | findstr :5000
netstat -ano | findstr :1433

# Stop conflicting services or change ports in docker-compose.yml
```

### Database Migration Errors

**Issue**: Backend fails to start due to migration errors

**Solution**:
```bash
# Remove volume and restart
docker-compose down -v
docker-compose up --build
```

## Production Deployment

### Environment Variables

For production, update these in `docker-compose.yml`:

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection=<your-production-db>
  - Jwt__Key=<strong-random-key>
```

### Using External SQL Server

If you have an external SQL Server instance:

1. Remove the `sqlserver` service from docker-compose.yml
2. Update the backend connection string
3. Remove the `depends_on` from backend service

### HTTPS Configuration

To enable HTTPS:

1. Generate SSL certificates
2. Mount certificates in containers
3. Update nginx configuration for SSL
4. Update backend to use HTTPS endpoints

## Data Persistence

### Database Data

Database data is persisted in Docker volume `sqlserver-data`:

```bash
# Backup database
docker exec cma-sqlserver /opt/mssql-tools18/bin/sqlcmd \
  -S localhost -U sa -P YourStrong@Passw0rd \
  -Q "BACKUP DATABASE CMADb TO DISK='/var/opt/mssql/backup/CMADb.bak'"

# Copy backup to host
docker cp cma-sqlserver:/var/opt/mssql/backup/CMADb.bak ./backup/
```

## Performance Optimization

### Resource Limits

Add resource limits in docker-compose.yml:

```yaml
services:
  backend:
    deploy:
      resources:
        limits:
          cpus: '1.0'
          memory: 1G
        reservations:
          cpus: '0.5'
          memory: 512M
```

### Caching

Docker builds use layer caching. To speed up builds:

1. Don't change package files frequently
2. Use `.dockerignore` to exclude unnecessary files
3. Order Dockerfile commands from least to most frequently changing

## Monitoring

### Health Checks

All services include health checks:

```bash
# View health status
docker-compose ps
```

### Container Stats

```bash
# Real-time resource usage
docker stats
```

## Cleanup

### Remove All CMA Containers

```bash
docker-compose down
```

### Remove Containers and Volumes

```bash
docker-compose down -v
```

### Remove Images

```bash
docker-compose down --rmi all
```

### Complete Cleanup

```bash
docker-compose down -v --rmi all
docker system prune -a --volumes
```

## Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [.NET Docker Images](https://hub.docker.com/_/microsoft-dotnet)
- [SQL Server Docker Guide](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker)
- [Angular Docker Deployment](https://angular.io/guide/deployment)

## Support

For issues specific to Docker setup:
1. Check container logs: `docker-compose logs`
2. Verify all prerequisites are met
3. Ensure ports are available
4. Check Docker daemon is running

For application issues, refer to the main [README.md](README.md).
