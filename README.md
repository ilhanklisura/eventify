# Eventify

Eventify is a single-page web application that connects event organizers with potential attendees. Organizers can manage their events, track ticket sales, and view analytics, while users can discover, search, and book events.

---

## Overview

Eventify is built using PHP with the FlightPHP framework for the backend and MySQL for the database. The frontend is implemented with HTML, CSS, and JavaScript, and leverages AJAX for dynamic content updates. The application is designed as a single-page app (SPA) to ensure seamless user experience, with a clear separation between the backend REST API and the frontend user interface. Secure user authentication is provided via JSON Web Tokens (JWT) along with role-based access control.

---

## Features

- **Role-Based Access:**  
  - **Event Organizers:** Access a dedicated dashboard to create, update, and delete events, manage venues, and view ticket sales and analytics.  
  - **Attendees:** Browse and search for events on a public main page, book tickets, and leave reviews.
  
- **CRUD Operations:**  
  - Full Create, Read, Update, Delete support for at least six key entities:
    - **Users**
    - **Events**
    - **Venues**
    - **Categories**
    - **Bookings**
    - **Tickets**

- **Responsive & Dynamic UI:**  
  - Single-page application with AJAX-driven navigation and updates.
  - Mobile-friendly design using Bootstrap for a consistent user experience across devices.

- **Secure Authentication:**  
  - JWT-based authentication ensures secure login and session management.
  - Role-based authorization to differentiate between organizer and attendee access.

- **RESTful API:**  
  - Backend services built using the FlightPHP framework.
  - API endpoints documented with OpenAPI (Swagger) for easy integration and testing.

---

## Tech Stack

- **Backend:**  
  - PHP with FlightPHP framework  
  - PHP PDO for secure database interactions
- **Frontend:**  
  - HTML, CSS, JavaScript  
  - AJAX for dynamic content updates  
  - Bootstrap for responsive design
- **Database:**  
  - MySQL
- **Authentication:**  
  - JSON Web Tokens (JWT)
- **Documentation:**  
  - OpenAPI (Swagger) for API documentation

---

## Database Schema

### **Entities:**
1. **Users**
2. **Events**
3. **Venues**
4. **Categories**
5. **Bookings**
6. **Tickets**

---

## Setup & Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/eventify.git
   cd eventify
   ```

2. **Backend Setup:**
    - Navigate to the `backend` directory.
    - Create a MySQL database and import the provided SQL schema (`database.sql`).
    - Update the database credentials in your configuration file.
    - Ensure that the FlightPHP framework is properly installed and initialized.

3. **Frontend Setup:**
    - Navigate to the `frontend` directory.
    - Open `index.html` in your browser to run the SPA locally.

4. **Configure JWT:**
    - Set your JWT secret keys in the backend configuration to secure authentication.

5. **Running the Application:**
    - Use a local server environment (such as XAMPP, WAMP, or Docker) to serve both the backend and frontend.
    - Access the application via your local domain (e.g., http://localhost/eventify/frontend).

---

## API Documentation

The RESTful API is documented using the OpenAPI standard. After setting up the backend, you can access the API documentation at:
```
http://localhost/eventify/backend/public/v1/docs/
```
or
```
https://seahorse-app-jta9z.ondigitalocean.app/backend/public/v1/docs/
```
This Swagger UI page provides details on all available endpoints, request/response formats, and authentication methods.

---

## Milestones

Eventify is developed and deployed through 5 major milestones:

- **Milestone 1:**
    - Project setup and static frontend development.
    - Draft ERD for the six key entities.

- **Milestone 2:**
    - Creation of the MySQL database schema.
    - Implementation of the DAO layer with CRUD operations for all entities.

- **Milestone 3:**
    - Full CRUD functionality for all entities.
    - Business logic implementation and dynamic content rendering via FlightPHP.
    - API documentation with OpenAPI.

- **Milestone 4:**
    - Implementation of middleware for authentication, request validation, and error handling.
    - JWT-based user authentication and role-based access control.
    - Dynamic frontend updates to reflect user roles (organizers vs. attendees).

- **Milestone 5:**
    - Final frontend refactoring (MVC pattern) and security enhancements.
    - Deployment of the application to a public hosting platform.
    - CI/CD integration and complete project documentation.

---

## Deployment

Eventify is designed to be deployed on any modern hosting platform (e.g., Heroku, DigitalOcean, AWS). Once deployed, the live application can be accessed at:
```
https://seahorse-app-jta9z.ondigitalocean.app/
```

---

Thank you for checking out Eventify! Enjoy exploring the project and feel free to contribute or provide feedback.