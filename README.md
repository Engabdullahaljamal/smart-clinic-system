# Smart Clinic System

## Overview
Smart Clinic System is an ASP.NET Core RESTful API designed to help clinics manage patients, doctors, departments, and appointments.

The system allows clinic staff to:
- Manage patient records
- Manage doctors
- Assign doctors to departments
- Schedule and track appointments

## Technologies Used
- ASP.NET Core
- C#
- Entity Framework Core
- Azure SQL Database
- SQL Server
- Swagger
- xUnit
- EF Core InMemory Database

## Architecture
The project follows a layered architecture:

- Controllers: Handle HTTP requests and responses
- Services: Contain business logic
- Data: Contains the EF Core DbContext
- Models: Represent database entities
- DTOs: Control request and response data

## Main Features
- Create, read, and delete patients
- Create, read, and delete doctors
- Create, read, and delete appointments
- Store data in Azure SQL Database
- Use DTOs for clean API input/output
- Unit tests for service layer methods

## Database Relationships
- Department has many Doctors
- Doctor has many Appointments
- Patient has many Appointments
- Doctor has many Specializations through DoctorSpecialization

## API Endpoints

### Patients
- GET /api/Patients
- GET /api/Patients/{id}
- POST /api/Patients
- DELETE /api/Patients/{id}

### Doctors
- GET /api/Doctors
- GET /api/Doctors/{id}
- POST /api/Doctors
- DELETE /api/Doctors/{id}

### Appointments
- GET /api/Appointments
- GET /api/Appointments/{id}
- POST /api/Appointments
- DELETE /api/Appointments/{id}

## Running the Project
```bash
dotnet restore
dotnet build
dotnet run