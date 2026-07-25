# Driving & Vehicle License Department (DVLD)

A desktop application developed using **C#**, **Windows Forms**, **SQL Server**, and **ADO.NET** following a **3-Tier Architecture** to automate the management of driving license services and applicants.

The system manages people, drivers, license applications, tests, issued licenses, and system users while enforcing business rules for each licensing process.

---

## Features

### License Services

- Issue a new driving license.
- Retake failed driving tests.
- Renew expired licenses.
- Replace lost licenses.
- Replace damaged licenses.
- Release detained licenses.
- Issue international driving licenses.

---

### People Management

- Add new people.
- Edit person information.
- Delete people.
- Search by National Number.
- Prevent duplicate records.

---

### Driver & License Management

- Register licensed drivers.
- View license details.
- Track driver's license history.
- Search licenses by license number or national number.

---

### License Applications

- Create license applications.
- Track application status.
- Prevent duplicate active applications.
- Validate applicant eligibility.

---

### Test Management

The licensing process includes:

- Vision Test
- Written Test
- Practical Driving Test

The system:

- Schedules test appointments.
- Records test results.
- Supports test retakes.
- Ensures tests are completed in the correct order.

---

### User Management

- Create system users.
- Edit user information.
- Delete users.
- Freeze user accounts.
- Manage user permissions.

---

## Business Rules

The application validates several important rules including:

- Minimum age based on license class.
- Prevent issuing duplicate licenses of the same class.
- Prevent duplicate active applications.
- Require passing all required tests before issuing a license.
- Validate renewal and replacement conditions.
- Maintain complete application and license history.

---

## Project Architecture

The application follows the **3-Tier Architecture**.

```
Presentation Layer
        │
Business Layer
        │
Data Access Layer
        │
SQL Server Database
```

### Presentation Layer

- Windows Forms user interface
- User interaction
- Input validation

### Business Layer

- Business rules
- Validation
- Licensing workflow
- Service processing

### Data Access Layer

- SQL Server connectivity
- CRUD operations
- Database communication using ADO.NET

---

## Technologies

- C#
- .NET Framework
- Windows Forms
- SQL Server
- ADO.NET
- Object-Oriented Programming (OOP)
- 3-Tier Architecture

---

## Main Modules

- Login
- People Management
- User Management
- Driver Management
- License Applications
- Test Appointments
- License Management
- International Licenses
- License Detention Management
- Application Types
- License Classes

---

## Learning Objectives

This project demonstrates practical experience with:

- Object-Oriented Programming
- Layered Architecture
- SQL Server Database Design
- ADO.NET
- CRUD Operations
- Authentication & Authorization
- Business Logic Implementation
- Desktop Application Development

---

## Getting Started

### Requirements

- Visual Studio 2022
- SQL Server
- SQL Server Management Studio (SSMS)
- .NET Framework

### Installation

1. Clone the repository.

```bash
git clone https://github.com/Taha-3laa/Driving-Vehicle-License-Department-System.git
```

2. Restore the SQL Server database.

3. Update the connection string.

4. Build and run the solution.

---

## Author

**Taha Alaa**

Computer Science Student | .NET Developer

Passionate about building desktop applications using C#, SQL Server, Object-Oriented Programming, and software architecture principles.
