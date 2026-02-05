# Task Management System

## 1. Overview
This is a Task Management System developed as part of the screening assignment for Oritso Private Limited.
The application allows users to create, view, update, delete, and search tasks using a clean MVC architecture.

The system demonstrates backend, frontend, database design, and documentation skills using enterprise-grade technologies.

---

## 2. Technology Stack

- Backend: ASP.NET Core MVC
- ORM: Entity Framework Core (Code First)
- Database: Microsoft SQL Server
- Frontend: Razor Views with Bootstrap
- Architecture Pattern: MVC

---

## 3. Application Features

- Create new tasks
- View list of tasks
- View task details
- Edit existing tasks
- Delete tasks
- Search tasks by title and status

---

## 4. Database Design

### 4.1 Entity Structure

Task Entity includes following fields:

- Id (Primary Key)
- Title
- Description
- Due Date
- Status
- Remarks
- Created On
- Last Updated On
- Created By (User Id & Name)
- Updated By (User Id & Name)

### 4.2 Indexes

- Index on Title (for fast search)
- Index on Status (for filtering)

### 4.3 Code First Approach

Entity Framework Core Code First approach is used to:
- Maintain domain-driven design
- Keep models as single source of truth
- Allow easier schema evolution

---

## 5. Application Structure

- MVC pattern is used
- Controllers handle business logic
- Models represent data structure
- Views render UI using Razor
  
Reason for choice:
MVC-based frontend ensures simplicity, maintainability, and faster development for enterprise CRUD applications.
---

## 6. Build & Run Instructions

### Prerequisites
- .NET SDK 7 or later
- SQL Server
- Visual Studio 2022

### Dependencies
The project uses the following main dependencies:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Mvc
All dependencies are managed via NuGet Package Manager.

### Steps
1. Clone the repository `https://github.com/SarfarajIT01/TaskManagementApp.git`
2. Update connection string in `appsettings.json`
3. Run EF migrations:

- Open Package Manager Console or terminal:
- Add-Migration InitialCreate  `in your case no neet to run this code`
- Update-Database  `only use this for create databas and tables`

4. Run the application
5. Navigate to `/Tasks`

---

## 7. Assumptions

- Authentication is simulated for CreatedBy and UpdatedBy fields
- Single-user context is assumed for simplicity

---

## 8. Future Enhancements

- User authentication & role management
- Task assignment to multiple users
- Pagination and sorting
- REST API + SPA frontend
