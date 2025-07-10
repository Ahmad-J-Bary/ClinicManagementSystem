# Clinic Management System

A comprehensive clinic management system built with ASP.NET Core 8 following Clean Architecture principles. This system provides functionality for managing patients, doctors, appointments, medical records, prescriptions, and more.

## 🏗️ Architecture

This project follows Clean Architecture pattern with the following layers:

- **Domain Layer** (`Clinic.Domain`): Contains business entities, enums, and domain logic
- **Application Layer** (`Clinic.Application`): Contains business logic, DTOs, and interfaces
- **Infrastructure Layer** (`Clinic.Infrastructure`): Contains external services and implementations
- **Persistence Layer** (`Clinic.Persistence`): Contains database context and repositories
- **API Layer** (`Clinic.API`): Contains controllers and API configuration

## 🚀 Features

### User Roles
- **Patients**: Self-registration, appointment booking, medical records viewing
- **Doctors**: Appointment management, medical record creation, prescription issuing
- **Administrators**: User management, system configuration, reporting
- **Receptionists**: Appointment scheduling, patient check-in
- **Pharmacists**: Prescription management and dispensing

### Core Functionality
- **Patient Management**: Registration, profile management, medical history
- **Doctor Management**: Professional profiles, specializations, availability
- **Appointment System**: Booking, confirmation, cancellation, rescheduling
- **Medical Records**: Diagnosis, treatment, prescriptions
- **Notification System**: Appointment reminders, status updates
- **Payment Processing**: Multiple payment methods, billing management
- **Department Management**: Medical departments and specializations

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 8
- **Database**: Entity Framework Core with SQL Server
- **Architecture**: Clean Architecture
- **Patterns**: Repository Pattern, CQRS with MediatR
- **Validation**: FluentValidation
- **Mapping**: AutoMapper
- **Authentication**: ASP.NET Core Identity with JWT
- **API Documentation**: Swagger/OpenAPI

## 📦 Domain Entities

### Core Entities
- `BaseEntity`: Abstract base class with common properties
- `User`: Abstract base class for all user types
- `Patient`: Patient-specific information and relationships
- `Doctor`: Doctor profiles, qualifications, and availability
- `Admin`: Administrative users with elevated privileges
- `Receptionist`: Front desk staff for appointment management
- `Pharmacist`: Prescription and medication management

### Business Entities
- `Department`: Medical departments and specializations
- `Appointment`: Appointment scheduling and management
- `MedicalRecord`: Patient medical history and records
- `Prescription`: Medication prescriptions and dispensing
- `Notification`: System notifications and alerts
- `Payment`: Payment processing and billing
- `DoctorAvailability`: Doctor scheduling and time slots

### Enums
- `AppointmentStatus`: Pending, Confirmed, Cancelled, Rejected, Completed, NoShow
- `PrescriptionStatus`: Active, Dispensed, Cancelled, Expired
- `NotificationType`: Various notification categories
- `PaymentMethod`: CreditCard, DebitCard, Cash, Insurance, etc.
- `PaymentStatus`: Pending, Completed, Failed, Refunded, etc.
- `UserRole`: Patient, Doctor, Admin, Receptionist, Pharmacist

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ClinicManagementSystem
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string**
   - Edit `Clinic.API/appsettings.json`
   - Update the `DefaultConnection` string for your SQL Server instance

4. **Run database migrations** (when implemented)
   ```bash
   dotnet ef database update --project Clinic.Persistence --startup-project Clinic.API
   ```

5. **Run the application**
   ```bash
   dotnet run --project Clinic.API
   ```

6. **Access the API**
   - API: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`
   - Health Check: `http://localhost:5000/health`

## 📚 API Endpoints

### Patients
- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient

### Doctors
- `GET /api/doctors` - Get all doctors
- `GET /api/doctors/{id}` - Get doctor by ID
- `GET /api/doctors/specialization/{specialization}` - Get doctors by specialization
- `GET /api/doctors/available` - Get available doctors for time slot

### Appointments
- `GET /api/appointments` - Get all appointments
- `GET /api/appointments/{id}` - Get appointment by ID
- `POST /api/appointments` - Create new appointment
- `GET /api/appointments/patient/{patientId}` - Get patient appointments
- `GET /api/appointments/doctor/{doctorId}` - Get doctor appointments
- `PUT /api/appointments/{id}/confirm` - Confirm appointment
- `PUT /api/appointments/{id}/cancel` - Cancel appointment

## 🏥 Business Logic

### Appointment Management
- Patients can book appointments with available doctors
- Doctors can confirm, reject, or reschedule appointments
- Automatic conflict detection for overlapping appointments
- Emergency appointment prioritization
- Appointment status tracking and notifications

### Medical Records
- Comprehensive patient medical history
- Doctor notes, diagnoses, and treatments
- Prescription management and tracking
- Lab results and imaging integration
- Follow-up appointment scheduling

### User Management
- Role-based access control
- Profile management for all user types
- Authentication and authorization
- Activity logging and audit trails

### Payment Processing
- Multiple payment method support
- Insurance integration
- Billing and invoicing
- Payment status tracking
- Refund processing

## 🔒 Security Features

- JWT-based authentication
- Role-based authorization
- Secure password handling
- API rate limiting (to be implemented)
- Data encryption (to be implemented)
- Audit logging (to be implemented)

## 🧪 Testing

The project includes comprehensive testing:
- Unit tests for domain logic
- Integration tests for API endpoints
- Repository pattern for testability
- Mock data for development and testing

## 📈 Future Enhancements

- Real-time notifications with SignalR
- Mobile application support
- Telemedicine integration
- Advanced reporting and analytics
- Integration with external medical systems
- Multi-language support
- Advanced scheduling algorithms

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Support

For support and questions, please contact the development team or create an issue in the repository.

---

**Built with ❤️ using ASP.NET Core and Clean Architecture**

