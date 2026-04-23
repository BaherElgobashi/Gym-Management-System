Gym Management System | ASP.NET Core MVC

A backend-focused Gym Management System built with ASP.NET Core MVC, following Clean Architecture and real-world business logic. The system goes beyond CRUD operations, including booking management, membership management, user management, and attendance tracking.


---

🚀 Key Features

1. Booking Management

Manage sessions with categories and trainers.

Book members for upcoming sessions.

Track member attendance for ongoing sessions.

Cancel bookings with validation on session start date.

Calculate available slots dynamically.

Views for Upcoming Sessions and Ongoing Sessions with action buttons.


2. Membership Management

Create memberships for members with active plans.

Cancel active memberships.

Display all active memberships with member and plan info.

Dropdown lists for members and plans integrated into Views.

Validation for member existence, plan existence, and active membership check.


3. User & Authentication Management

Register and login users using ASP.NET Core Identity.

Role-based access control (RBAC) for Admin and SuperAdmin.

Retrieve users by roles.

Password validation and role assignment during registration.


4. Technical Highlights

Clean Architecture: Clear separation of Presentation, Business Logic, and Data Access layers.

Repositories & Unit of Work: Generic repositories with Unit of Work pattern.

EF Core Code-First: Entity configurations and relationships defined with Fluent API.

AutoMapper: Maps entities to ViewModels seamlessly.

Validation & Alerts: Server-side and client-side validation with Bootstrap alerts.

Responsive Views: Built with Bootstrap 5, compatible with desktop and mobile.

Select2 Integration: Enhanced dropdowns for member selection in bookings.



---

📂 Project Structure

Gym_Management_System/
│
├─ Presentation/               # MVC Layer (Controllers, Views)
│   ├─ Controllers/
│   ├─ Views/
│   │   ├─ Booking/
│   │   ├─ Membership/
│   │   └─ Shared/
│
├─ BusinessLogic/              # Services, DTOs, ViewModels
│   ├─ Services/
│   │   ├─ Classes/            # Service Implementations
│   │   └─ Interfaces/
│   └─ ViewModels/
│       ├─ BookingViewModels/
│       └─ MembershipViewModels/
│
├─ DataAccess/                 # Repositories and DbContext
│   ├─ Repositories/
│   │   ├─ Classes/
│   │   └─ Interfaces/
│   └─ GymSystemDbContext.cs
│
├─ Domain/                     # Entities, Enums
│
├─ wwwroot/                    # Static assets (JS, CSS)
│
└─ README.md


---

⚙️ Technologies & Tools

.NET 9 / ASP.NET Core MVC

Entity Framework Core (Code-First)

SQL Server

ASP.NET Core Identity

AutoMapper

Bootstrap 5 & Select2

Dependency Injection

Unit of Work & Generic Repository Pattern



---

✅ Business Logic Implementation

1. Booking Workflow

Check if session exists and is upcoming.

Verify member has an active membership.

Prevent duplicate bookings.

Validate capacity before booking.

Track attendance with MemberAttended.

Cancel booking if session hasn't started.



2. Membership Workflow

Validate member and plan existence.

Check for existing active memberships.

Calculate membership end date based on plan duration.

Cancel membership if active.



3. User Management

Register new users with roles.

Validate login credentials.

Retrieve users based on roles.





---

📄 Sample Views

Sessions Dashboard: View upcoming & ongoing sessions with trainer, category, date, time, duration, and capacity.

Booking Pages: Book members, mark attendance, cancel bookings.

Membership Pages: Create and cancel memberships, view active memberships.

User Management Pages: List users, roles, and register new users (Admin / SuperAdmin).



---

⚡ Installation & Setup

1. Clone the repository:



git clone https://github.com/<YourUsername>/Gym-Management-System.git

2. Navigate to the project folder:



cd Gym-Management-System

3. Configure appsettings.json with your SQL Server connection string.


4. Apply migrations and update database:



dotnet ef database update

5. Run the project:



dotnet run

6. Open the application in your browser:



(https://localhost:7164/)


---

📌 Notes

Ensure SQL Server is installed and running.

The system uses TempData and Bootstrap alerts for feedback.

Select2 plugin is used for better dropdown UX.

All business rules (capacity, attendance, active membership) are validated server-side.



---

📫 Contact

Baher Mohamed Zakaria Elgobashi

Email:bahermohamedelgobashi@gmail.com

LinkedIn: https://www.linkedin.com/in/baher-elgobashi-1975a5298/

GitHub: https://github.com/BaherElgobashi
