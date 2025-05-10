
This is a web-based Sales and Stock Reporting System developed as part of a technical assignment for Limerick Resources Ltd. The project demonstrates backend development using ASP.NET Core Web API, Entity Framework Core, clean coding standards, proper validation, layered architecture (Service and Repository Pattern), and basic frontend integration with Bootstrap and jQuery.

🚀 Features
🔐 Authentication
User registration and secure login by JWT Role and token based authentication.

Passwords hashed using ASP.NET Core Identity standards

Protected access to Product, Sales, and Reporting APIs

Note: This application implements role-based authentication secured with JWT tokens. By default, users registered through the UI are assigned the User role.
If you wish to assign a different role to a user, you can use the add-role endpoint available in Swagger.

To make testing easier, the following sample credentials have been provided:

Username: Admin
Password: 123456
Role: Admin

Username: Rafsun
Password: 123456
Role: Client

🔐 Important: Accessing any secured API endpoint requires a valid JWT token.
To test or verify these endpoints, please use Postman and include the token in the request header as a Bearer Token.


📦 Product Management (CRUD)
Add, edit, view, and soft delete products

Fields include Name, SKU, Price, Initial Stock, and Description

Soft delete implemented via IsDeleted flag

🛒 Sales Management
Sell products by selecting quantity

Auto-calculates total sale price

Stock validation: prevents over-selling

Records sale details with timestamp

📈 Reporting
Current Stock Report: Real-time inventory overview

Date-Wise Stock Report (used here store procedure)

Opening stock before date range

Total quantity sold within range

Closing stock calculated dynamically

🔍 Search & Pagination
Search products by Name or SKU

Pagination (10 items per page) for Products and Sales lists

🧰 Technologies Used
Backend: ASP.NET Core Web API

Frontend: Bootstrap + JavaScript/jQuery (AJAX) in Asp.net core mvc

Database: SQL Server 

Authentication: JWT + ASP.NET Identity (or custom auth logic)

Architecture: Layered (Controller → Service → Repository)

ORM: Entity Framework Core
