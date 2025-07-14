# Enhanced Clinic Management System

A comprehensive clinic management system built with ASP.NET Core 8 following Clean Architecture principles. This enhanced version incorporates best practices from multiple reference projects and implements a robust, scalable, and maintainable solution for healthcare management.

## 🏗️ Architecture

This project follows Clean Architecture pattern with clear separation of concerns across multiple layers:

### **Domain Layer** (`Clinic.Domain`)
- Contains business entities, enums, and domain logic
- Independent of external dependencies
- Defines core business rules and enterprise logic

### **Application Layer** (`Clinic.Application`)
- Contains business logic, DTOs, and interfaces
- Implements CQRS pattern using MediatR
- Includes AutoMapper profiles for object mapping
- FluentValidation for input validation
- Custom exceptions for error handling

### **Infrastructure Layer** (`Clinic.Infrastructure`)
- Contains external services and implementations
- Email services, logging, and third-party integrations
- Isolated from core business logic

### **Persistence Layer** (`Clinic.Persistence`)
- Data access layer using Entity Framework Core
- Repository pattern implementation
- Database context and configurations
- Specification pattern for complex queries

### **API Layer** (`Clinic.API`)
- RESTful API endpoints
- Controllers using MediatR for CQRS
- Centralized exception handling middleware
- Swagger/OpenAPI documentation
- Health checks and monitoring

## 🚀 Features

### **Enhanced Architecture Features**
- **CQRS with MediatR**: Clear separation between commands and queries
- **AutoMapper Integration**: Seamless object-to-object mapping
- **FluentValidation**: Comprehensive input validation with business rules
- **Centralized Exception Handling**: Consistent error responses across the API
- **Specification Pattern**: Complex query logic encapsulation
- **Repository Pattern**: Clean data access abstraction

### **User Roles**
- **Patients**: Self-registration, appointment booking, medical records viewing
- **Doctors**: Appointment management, medical record creation, prescription issuing
- **Administrators**: User management, system configuration, reporting
- **Receptionists**: Appointment scheduling, patient check-in
- **Pharmacists**: Prescription management and dispensing

### **Core Functionality**
- **Patient Management**: Registration, profile management, medical history
- **Doctor Management**: Professional profiles, specializations, availability
- **Appointment System**: Booking, confirmation, cancellation, rescheduling
- **Medical Records**: Diagnosis, treatment, prescriptions
- **Notification System**: Email notifications for appointments and updates
- **Payment Processing**: Multiple payment methods, billing management
- **Department Management**: Medical departments and specializations

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8
- **Database**: Entity Framework Core with SQL Server
- **Architecture**: Clean Architecture with CQRS
- **Patterns**: Repository Pattern, Specification Pattern, MediatR
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Authentication**: ASP.NET Core Identity with JWT (ready for implementation)
- **Logging**: Serilog with structured logging
- **API Documentation**: Swagger/OpenAPI
- **Health Checks**: Built-in health monitoring

## 📦 Domain Entities

### **Core Entities**
- `BaseEntity`: Abstract base class with common properties
- `User`: Abstract base class for all user types
- `Patient`: Patient-specific information and relationships
- `Doctor`: Doctor profiles, qualifications, and availability
- `Admin`: Administrative users with elevated privileges
- `Receptionist`: Front desk staff for appointment management
- `Pharmacist`: Prescription and medication management

### **Business Entities**
- `Department`: Medical departments and specializations
- `Appointment`: Appointment scheduling and management
- `MedicalRecord`: Patient medical history and records
- `Prescription`: Medication prescriptions and dispensing
- `Notification`: System notifications and alerts
- `Payment`: Payment processing and billing
- `DoctorAvailability`: Doctor scheduling and time slots

### **Enums**
- `AppointmentStatus`: Pending, Confirmed, Cancelled, Rejected, Completed, NoShow
- `PrescriptionStatus`: Active, Dispensed, Cancelled, Expired
- `NotificationType`: Various notification categories
- `PaymentMethod`: CreditCard, DebitCard, Cash, Insurance, etc.
- `PaymentStatus`: Pending, Completed, Failed, Refunded, etc.
- `UserRole`: Patient, Doctor, Admin, Receptionist, Pharmacist

## 🚀 Getting Started

### **Prerequisites**
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### **Installation**

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EnhancedClinicManagementSystem
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string**
   - Edit `Clinic.API/appsettings.json`
   - Update the `DefaultConnection` string for your SQL Server instance

4. **Run database migrations**
   ```bash
   dotnet ef database update --project Clinic.Persistence --startup-project Clinic.API
   ```

5. **Run the application**
   ```bash
   dotnet run --project Clinic.API
   ```

6. **Access the API**
   - API: `https://localhost:5001` or `http://localhost:5000`
   - Swagger UI: `https://localhost:5001` (root path)
   - Health Check: `https://localhost:5001/health`

## 📚 API Endpoints

### **Patients**
- `POST /api/patients` - Create new patient
- `GET /api/patients/{id}` - Get patient by ID
- `PUT /api/patients/{id}` - Update patient
- `GET /api/patients` - Get all patients (paginated)
- `GET /api/patients/search` - Search patients

### **Doctors**
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `GET /api/doctors/specialization/{specialization}` - Get doctors by specialization
- `GET /api/doctors/available` - Get available doctors for time slot

### **Appointments**
- `GET /api/appointments` - Get all appointments
- `GET /api/appointments/{id}` - Get appointment by ID
- `POST /api/appointments` - Create new appointment
- `GET /api/appointments/patient/{patientId}` - Get patient appointments
- `GET /api/appointments/doctor/{doctorId}` - Get doctor appointments
- `PUT /api/appointments/{id}/confirm` - Confirm appointment
- `PUT /api/appointments/{id}/cancel` - Cancel appointment

## 🏥 Enhanced Features

### **CQRS Implementation**
All business operations are implemented using the Command Query Responsibility Segregation (CQRS) pattern with MediatR:

- **Commands**: Handle write operations (Create, Update, Delete)
- **Queries**: Handle read operations (Get, Search, List)
- **Handlers**: Process commands and queries with business logic
- **Validators**: Validate input using FluentValidation

### **AutoMapper Profiles**
Centralized mapping configurations in the `MappingProfiles` folder:
- `PatientProfile`: Maps Patient entities to DTOs and commands
- `DoctorProfile`: Maps Doctor entities to DTOs and commands
- `AppointmentProfile`: Maps Appointment entities to DTOs and commands

### **Validation Pipeline**
Comprehensive validation using FluentValidation:
- Input validation for all commands
- Business rule validation
- Custom validators for domain-specific rules
- Automatic validation in MediatR pipeline

### **Exception Handling**
Centralized exception handling middleware:
- Consistent error responses
- Proper HTTP status codes
- Structured error logging
- Validation error details

### **Repository Pattern**
Clean data access with repository pattern:
- Generic repository for common operations
- Specific repositories for domain-specific queries
- Specification pattern for complex queries
- Unit of Work pattern for transactions

## 🔒 Security Features

- JWT-based authentication (ready for implementation)
- Role-based authorization
- Secure password handling
- Input validation and sanitization
- SQL injection prevention through EF Core
- CORS configuration for cross-origin requests

## 📊 Logging and Monitoring

- **Serilog**: Structured logging with multiple sinks
- **Health Checks**: Database and application health monitoring
- **Request/Response Logging**: Detailed API request tracking
- **Error Logging**: Comprehensive error tracking and reporting

## 🧪 Testing Strategy

The enhanced architecture supports comprehensive testing:

- **Unit Tests**: Domain logic and application handlers
- **Integration Tests**: API endpoints and database operations
- **Repository Tests**: Data access layer validation
- **Validation Tests**: FluentValidation rule testing

## 📈 Development Workflow

### **Branching Strategy**
- `main`: Production-ready code
- `develop`: Integration branch for ongoing development
- `feature/*`: Feature development branches
- `bugfix/*`: Bug fix branches
- `hotfix/*`: Critical production fixes

### **Commit Message Convention**
Following Conventional Commits specification:
```
<type>(<scope>): <description>

[optional body]

[optional footer]
```

**Types**: feat, fix, docs, style, refactor, perf, test, build, ci, chore, revert

**Example**:
```
feat(patient): add patient registration with validation

- Implement CreatePatientCommand with MediatR
- Add comprehensive validation using FluentValidation
- Include AutoMapper profile for Patient entity
- Add unit tests for patient creation logic

Closes #123
```

## 🔧 Configuration

### **Database Configuration**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EnhancedClinicDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### **Email Configuration**
```json
{
  "EmailSettings": {
    "ApiKey": "your-sendgrid-api-key",
    "FromEmail": "noreply@enhancedclinic.com",
    "FromName": "Enhanced Clinic System"
  }
}
```

### **Logging Configuration**
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

## 🚀 Deployment

### **Development**
```bash
dotnet run --project Clinic.API --environment Development
```

### **Production**
```bash
dotnet publish --configuration Release --output ./publish
```

### **Docker Support** (Future Enhancement)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Clinic.API/Clinic.API.csproj", "Clinic.API/"]
RUN dotnet restore "Clinic.API/Clinic.API.csproj"
COPY . .
WORKDIR "/src/Clinic.API"
RUN dotnet build "Clinic.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinic.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinic.API.dll"]
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/my-feature`
3. Commit changes following conventional commits
4. Push to the branch: `git push origin feature/my-feature`
5. Open a Pull Request

Please ensure:
- All tests pass
- Code follows the established patterns
- Proper documentation is included
- Commit messages follow the convention

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions:
- Create an issue in the repository
- Contact the development team at support@enhancedclinic.com

## 🙏 Acknowledgments

This enhanced version incorporates best practices and patterns from:
- **HIAST-Clinics**: Feature inspiration and clinic domain modeling
- **HR.LeaveManagement.Clean**: Clean architecture implementation and CQRS patterns
- **Mediplus-Spring-FullStack**: Workflow and development practices

**Built with ❤️ using ASP.NET Core 8 and Clean Architecture**

---

## 📋 Project Status

- ✅ **Domain Layer**: Complete with all entities and enums
- ✅ **Application Layer**: CQRS with MediatR, AutoMapper, FluentValidation
- ✅ **Infrastructure Layer**: Email services, logging, external integrations
- ✅ **Persistence Layer**: Entity Framework Core, Repository pattern
- ✅ **API Layer**: RESTful endpoints, exception handling, documentation
- 🔄 **Authentication**: JWT implementation ready
- 🔄 **Testing**: Unit and integration test projects
- 🔄 **CI/CD**: GitHub Actions workflow
- 🔄 **Docker**: Containerization support

## 🎯 Next Steps

1. Implement JWT authentication and authorization
2. Add comprehensive unit and integration tests
3. Create database migrations and seed data
4. Implement remaining CQRS handlers for all entities
5. Add real-time notifications with SignalR
6. Implement advanced reporting and analytics
7. Add mobile API support
8. Integrate with external medical systems

