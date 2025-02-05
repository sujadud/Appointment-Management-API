# Appointment Management API Documentation

## Project Overview

The **Appointment Management API** is a RESTful service built with .NET 8, designed to facilitate the management of doctors and appointments in a healthcare setting. It provides secure endpoints for creating, reading, updating, and deleting doctor profiles and appointments. The API leverages JWT authentication for secure access and includes comprehensive documentation for easy integration.

## Features

-   **Doctor Management**: Create, retrieve, update, and delete doctor profiles.
    
-   **Appointment Scheduling**: Manage appointments between patients and doctors.
    
-   **User Authentication**: Secure API endpoints with token-based authentication.
    
-   **Validation and Error Handling**: Robust input validation and meaningful error responses.
    
-   **Asynchronous Operations**: Improve performance with asynchronous programming.
    

## Technologies Used

-   **.NET 8**
    
-   **ASP.NET Core**

-   **RESTful API**
    
-   **EF Core**

-   **Fluent API**
    
-   **MS SQL Server**
    
-   **JWT Authentication**

-   **Fluent Validation**
    
-   **XUnit for Testing**
    

## Prerequisites

-   **.NET 8 SDK** installed on your machine.
    
-   **SQL Server** (or any compatible relational database).
    
-   **Visual Studio 2022** or **Visual Studio Code** for development.
    
-   **Postman** or any API testing tool for testing endpoints.
    

## Installation

### Clone the Repository

bash

```
git clone https://github.com/sujadud/appointment-management-api.git

```

### Navigate to the Project Directory

bash

```
cd appointment-management-api

```

### Restore Dependencies

bash

```
dotnet restore

```

### Configure the Database

-   Update the **connection string** in `appsettings.json` to point to your SQL Server instance.
    

json

```
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AppointmentDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```

### Apply Migrations and Update Database

bash

```
dotnet ef database update

```

### Run the Application

bash

```
dotnet run

```

The API will be accessible at `https://localhost:5001` or `http://localhost:5000`.

## Usage

### Authentication

The API uses JWT Bearer authentication. To access secured endpoints, you need to:

1.  **Obtain a JWT Token**: Send a request to the authentication endpoint with valid credentials.
    
2.  **Include the Token**: Add the token to the `Authorization` header of your requests.
    

Example:

```
Authorization: Bearer YOUR_JWT_TOKEN_HERE

```

### API Endpoints

#### Doctor Endpoints

##### Get All Doctors

-   **Endpoint**
    
    ```
    GET /api/Doctors
    
    ```
    
-   **Description**
    
    Retrieve a list of all doctors.
    
-   **Authorization**
    
    -   `AllowAnonymous` (No authentication required)
        
-   **Response**
    
    -   **200 OK**: Returns a list of doctor objects.
        
-   **Sample Request**
    
    http
    
    ```
    GET /api/Doctors HTTP/1.1
    Host: localhost:5000
    
    ```
    

##### Get Doctor by ID

-   **Endpoint**
    
    ```
    GET /api/Doctors/{id}
    
    ```
    
-   **Description**
    
    Retrieve details of a specific doctor by their unique ID.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the doctor.
        
-   **Authorization**
    
    -   `AllowAnonymous` (No authentication required)
        
-   **Response**
    
    -   **200 OK**: Returns the doctor object.
        
    -   **404 Not Found**: Doctor not found.
        
-   **Sample Request**
    
    http
    
    ```
    GET /api/Doctors/123e4567-e89b-12d3-a456-426614174000 HTTP/1.1
    Host: localhost:5000
    
    ```
    

##### Create Doctor

-   **Endpoint**
    
    ```
    POST /api/Doctors
    
    ```
    
-   **Description**
    
    Add a new doctor to the system.
    
-   **Request Body**
    
    json
    
    ```
    {
      "id": "GUID (optional)",
      "name": "Dr. Jane Smith",
      "specialization": "Cardiology"
    }
    
    ```
    
-   **Authorization**
    
    -   `AllowAnonymous` (No authentication required)
        
-   **Response**
    
    -   **201 Created**: Returns the created doctor object.
        
    -   **400 Bad Request**: Validation errors.
        
-   **Sample Request**
    
    http
    
    ```
    POST /api/Doctors HTTP/1.1
    Host: localhost:5000
    Content-Type: application/json
    
    {
      "name": "Dr. Jane Smith",
      "specialization": "Cardiology"
    }
    
    ```
    

##### Update Doctor

-   **Endpoint**
    
    ```
    PUT /api/Doctors/{id}
    
    ```
    
-   **Description**
    
    Update an existing doctor's information.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the doctor to update.
        
-   **Request Body**
    
    json
    
    ```
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Dr. John Doe",
      "specialization": "Neurology"
    }
    
    ```
    
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Returns the updated doctor object.
        
    -   **400 Bad Request**: ID mismatch or validation errors.
        
    -   **404 Not Found**: Doctor not found.
        
-   **Sample Request**
    
    http
    
    ```
    PUT /api/Doctors/123e4567-e89b-12d3-a456-426614174000 HTTP/1.1
    Host: localhost:5000
    Content-Type: application/json
    Authorization: Bearer YOUR_JWT_TOKEN_HERE
    
    {
      "id": "123e4567-e89b-12d3-a456-426614174000",
      "name": "Dr. John Doe",
      "specialization": "Neurology"
    }
    
    ```
    

##### Delete Doctor

-   **Endpoint**
    
    ```
    DELETE /api/Doctors/{id}
    
    ```
    
-   **Description**
    
    Remove a doctor from the system.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the doctor to delete.
        
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Doctor deleted successfully.
        
    -   **404 Not Found**: Doctor not found.
        
-   **Sample Request**
    
    http
    
    ```
    DELETE /api/Doctors/123e4567-e89b-12d3-a456-426614174000 HTTP/1.1
    Host: localhost:5000
    Authorization: Bearer YOUR_JWT_TOKEN_HERE
    
    ```
    

#### Appointment Endpoints

##### Get All Appointments

-   **Endpoint**
    
    ```
    GET /api/Appointments
    
    ```
    
-   **Description**
    
    Retrieve a list of all appointments.
    
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Returns a list of appointment objects.
        

##### Get Appointment by ID

-   **Endpoint**
    
    ```
    GET /api/Appointments/{id}
    
    ```
    
-   **Description**
    
    Retrieve details of a specific appointment by its unique ID.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the appointment.
        
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Returns the appointment object.
        
    -   **404 Not Found**: Appointment not found.
        

##### Create Appointment

-   **Endpoint**
    
    ```
    POST /api/Appointments
    
    ```
    
-   **Description**
    
    Schedule a new appointment.
    
-   **Request Body**
    
    json
    
    ```
    {
      "patientName": "John Doe",
      "patientContactInformation": "john.doe@example.com",
      "appointmentDateTime": "2025-02-05T11:30:30.341+06:00",
      "doctorId": "123e4567-e89b-12d3-a456-426614174000"
    }
    
    ```
    
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **201 Created**: Returns the created appointment object.
        
    -   **400 Bad Request**: Validation errors.
        

##### Update Appointment

-   **Endpoint**
    
    ```
    PUT /api/Appointments/{id}
    
    ```
    
-   **Description**
    
    Update an existing appointment's details.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the appointment to update.
        
-   **Request Body**
    
    json
    
    ```
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "patientName": "Jane Doe",
      "patientContactInformation": "jane.doe@example.com",
      "appointmentDateTime": "2025-03-10T10:00:00.000Z",
      "doctorId": "123e4567-e89b-12d3-a456-426614174000"
    }
    
    ```
    
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Returns the updated appointment object.
        
    -   **400 Bad Request**: ID mismatch or validation errors.
        
    -   **404 Not Found**: Appointment not found.
        

##### Delete Appointment

-   **Endpoint**
    
    ```
    DELETE /api/Appointments/{id}
    
    ```
    
-   **Description**
    
    Cancel an appointment.
    
-   **Parameters**
    
    -   `id` (GUID, required): The unique identifier of the appointment to delete.
        
-   **Authorization**
    
    -   `[Authorize]` (Requires authentication)
        
-   **Response**
    
    -   **200 OK**: Appointment deleted successfully.
        
    -   **404 Not Found**: Appointment not found.
        

### Error Handling

The API uses standard HTTP status codes to indicate the success or failure of an API request. Error responses include a message explaining the reason for the error.

-   **400 Bad Request**: The request was invalid or cannot be served.
    
-   **401 Unauthorized**: Authentication credentials were missing or incorrect.
    
-   **403 Forbidden**: The request is understood, but it has been refused.
    
-   **404 Not Found**: The requested resource could not be found.
    
-   **500 Internal Server Error**: An error occurred on the server.
    

### Validation Rules

-   **Doctor Creation/Update**:
    
    -   **Name**: Required, maximum length of 100 characters.
        
    -   **Specialization**: Required, maximum length of 100 characters.
        
-   **Appointment Creation/Update**:
    
    -   **Patient Name**: Required, maximum length of 100 characters.
        
    -   **Patient Contact Information**: Required, valid email or phone number.
        
    -   **Appointment DateTime**: Must be a future date and time.
        
    -   **DoctorId**: Must be a valid GUID of an existing doctor.
        

## Testing

### Running Tests with XUnit

The project includes XUnit tests to ensure the API endpoints function as expected.

**To run the tests:**

bash

```
dotnet test

```

### Sample Test for Doctor Endpoints

csharp

```
[Fact]
public async Task GetAllDoctors_ReturnsOkResult()
{
    // Arrange
    // (Initialize test client and any required data)

    // Act
    var response = await _client.GetAsync("/api/Doctors");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var content = await response.Content.ReadAsStringAsync();
    content.Should().NotBeNullOrEmpty();
}

```

_(Include additional test cases as appropriate.)_

### Testing with Postman

Import the provided Postman collection to quickly test all API endpoints. The collection includes pre-configured requests and environment variables for your convenience.

## Contributing

We welcome contributions! Please follow these guidelines:

1.  **Fork the Repository**: Create your own fork of the project.
    
2.  **Create a Branch**: For new features or bug fixes.
    
    bash
    
    ```
    git checkout -b feature/your-feature-name
    
    ```
    
3.  **Commit Your Changes**: Provide clear and concise commit messages.
    
    bash
    
    ```
    git commit -m "Add new feature"
    
    ```
    
4.  **Push to the Branch**:
    
    bash
    
    ```
    git push origin feature/your-feature-name
    
    ```
    
5.  **Open a Pull Request**: Describe your changes and submit for review.
    

### Coding Standards

-   Follow **C# conventions** for code style and naming.
    
-   Write **XML documentation** for public methods and classes.
    
-   Ensure all **unit tests pass** before submitting.
    
-   Maintain **consistency** in indentation and formatting.
    

### Code of Conduct

Please note that this project adheres to a Code of Conduct. By participating, you are expected to uphold this code.

## License

This project is licensed under the **MIT License**. See the LICENSE file for details.

## Contact Information

For questions, issues, or suggestions, please contact:

-   **Name**: Md. Sujad-ud Doula
    
-   **Email**: sujad.ud@hotmail.com
    
-   **GitHub**: sujadud
    

## Acknowledgements

-   Special thanks to all contributors and the open-source community.
    
-   Built with ❤️ using .NET technologies, Happy Coding.
