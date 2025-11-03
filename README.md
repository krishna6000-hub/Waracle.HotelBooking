# Brief

Create a hotel room booking API using ASP.NET Core and Entity Framework EF Core, 
the solution must be written in C# following RESTful principles. 

# Business Rules 
- Hotels have 3 room types: single, double, deluxe. 
- Hotels have 6 rooms. 
- A room cannot be double booked for any given night. 
- Any booking at the hotel must not require guests to change rooms at any point 
  during their stay. 
- Booking numbers should be unique. There should not be overlapping at any 
  given time. 
- A room cannot be occupied by more people than its capacity. 


# Completed by Krishna Reddy

SwaggerUI Documentation Link:

https://hotelbookingsystem-esc8fjfdeyd5c2h3.uksouth-01.azurewebsites.net/swagger/index.html

Swagger JSON Endpoint:

https://hotelbookingsystem-esc8fjfdeyd5c2h3.uksouth-01.azurewebsites.net/swagger/v1/swagger.json
  
  This solution includes the following features:

  # Waracle.HotelBooking

  - This is the core of the application containing the main business logic, data models, and interfaces.

  # Waracle.HotelBooking.WebAPI

  - This project hosts the ASP.NET Core Web API, exposing endpoints for hotel room booking operations.
  - It leverages the core Waracle.HotelBooking project to handle requests and responses.
  - Swagger/OpenAPI is integrated for API documentation and testing.
  - The Web API follows RESTful principles for resource management.
  - Dependency Injection is used for service management and decoupling components.
  - The application uses Entity Framework Core for data access and management.
  - The application is deployed to an Azure Web App for accessibility.
  - the application contains the EF Core migrations for setting up the database schema.
  - The application used Azure SQL Database for data storage in Azure.
  - The application can use SQL server when run on local machine.

  # Waracle.HotelBooking.WebAPI.Test

  - This project contains unit tests for the Web API and application objects, ensuring the correctness of the implemented functionality.
  - The solution uses Entity Framework Core with an in-memory database for simplicity and ease of testing.
  - The unit test coverage coverage is approximately 80% for the Web API and application objects although 100% would have been ideal.
  
  # Key Features Implemented
  
  - Find a hotel based on its name.
  - Find available rooms between two dates for a given number of people.
  - Book a room with unique booking reference generation.
  - Retrieve booking details based on a booking reference.
  - Data seeding and resetting functionality for testing purposes.
  - Swagger/OpenAPI documentation for easy testing and exploration of the API.
  
  # Getting Started
  
  - Clone the repository to your local machine.
  - Navigate to the Waracle.HotelBooking.WebAPI project directory.
  - Run the application using `dotnet run`.
  - Access the Swagger UI at `https://localhost:{port}/swagger` to explore and test the API endpoints.

  # Note
 
 - This solution is designed for demonstration purposes and may require further enhancements for production use such logging, error handling, and security considerations.
	


