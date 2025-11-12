Full-Stack Task Management Dashboard

This project is a complete Task Management Dashboard built using a modern full-stack architecture. The entire application, including the frontend, backend API, and a placeholder database setup, is containerized using Docker Compose for easy deployment and local testing.

---

Technology Stack

| Frontend | React | Single Page Application (SPA) for the task management board and task forms, featuring drag-and-drop functionality. |
| Backend API | ASP.NET Core Web API (C#) | RESTful API handling task logic, persistence, and data validation via DTOs and Data Annotations. |
| Containerization | Docker & Docker Compose | Used to build and orchestrate the multi-container application stack. |
| API Docs | Swagger/OpenAPI | Auto-generated, interactive documentation for all backend endpoints. |

---

Running the Project

Prerequisites

You must have Docker Desktop installed and running on your system.

Startup Steps

1.  Navigate to Root: Open your terminal and navigate to the root directory of the project (where this `README.md` and `docker-compose.yml` are located).

2.  Execute Docker Compose: Run the following command. The `--build` flag ensures your latest code is compiled and packaged into fresh containers.

    ```bash
    docker compose up --build
    ```

Access Points

Once the services are running, you can access the application and documentation via these local endpoints:

| Frontend Dashboard (UI) | http://localhost:3000 |
| Backend API Documentation | http://localhost:8080/swagger |
