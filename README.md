A RESTful Web API for an accommodation booking platform, built with ASP.NET Core and Entity Framework Core. Tenants search properties and book rooms, landlords manage their properties and confirm reservations, and admins manage users, lookup data and the audit log.

The Angular frontend for this API is in booking-front.

Features
JWT authentication with role- and permission-based authorization
Three roles: Tenant, Landlord and Admin
Properties and rooms: CRUD, multiple images per property, amenities, room pricing
Reservations: tenants book rooms, and landlords confirm bookings for properties they own
Reviews for properties
Admin area: users, countries, destinations, amenities, roles, permissions
Audit log of user actions
Request validation and paginated list endpoints
Swagger UI with JWT support for testing protected endpoints
Architecture

The solution is split into five projects so that business logic is independent of data access and HTTP:

Project	Responsibility
Booking.Domain	Entities
Booking.DataAccess	EF Core DbContext and entity configuration
Booking.Application	Interfaces, DTOs and use case contracts
Booking.Implementation	Business logic, validators and use case implementations
Booking.API	Controllers, JWT setup, middleware, Swagger
Roles
Role	Can do
Tenant	Search properties, make reservations, write reviews, manage profile
Landlord	Everything a tenant can, plus manage own properties, rooms, prices and amenities, and confirm guest reservations
Admin	Everything a landlord can (for properties they own), plus manage users, lookup tables, roles, permissions and the audit log

Users cannot register themselves as Admin.

Tech stack
C#, ASP.NET Core Web API
Entity Framework Core
SQL database
JWT Bearer authentication
Swagger (Swashbuckle)
Getting started
Clone the repository:
bash
   git clone https://github.com/milanov1c/booking-api.git
   cd booking-api
Set your database connection string in Booking.API/appsettings.json.
Create the database:
bash
   dotnet ef database update --project Booking.DataAccess --startup-project Booking.API
Optionally seed amenities by running insert_amenities.sql against the database.
Run the API:
bash
   dotnet run --project Booking.API

The API runs on http://localhost:5064.

API documentation

In the Development environment, Swagger UI is available at:

http://localhost:5064/swagger

To call protected endpoints, log in to get a token, click Authorize and enter Bearer {your token}.

Frontend

The Angular frontend runs on http://localhost:4200. The API allows CORS requests from that origin and serves uploaded images from wwwroot/uploads.
