# SocialMediaFeed

A full-stack social media feed application with user authentication, post management, and interactive features.

**Live Application:** https://socialmediafeed.paweldywan.com/

## Overview

SocialMediaFeed is a modern web application that provides a complete social media posting experience. Users can create accounts, authenticate, publish posts, interact with content through likes, and manage their own posts. The application follows a clean three-tier architecture with separate layers for data access, business logic, and presentation.

## Key Features

- **User Authentication & Authorization** - Secure registration and login using ASP.NET Core Identity
- **Post Management** - Create, read, update, and delete social media posts
- **Nested Comments** - Support for replies and threaded conversations
- **Like System** - Interactive like/unlike functionality for posts
- **Real-time Updates** - Dynamic content updates without page refreshes
- **Responsive Design** - Mobile-friendly UI built with Bootstrap
- **Swagger Documentation** - Interactive API documentation for developers
- **Data Seeding** - Automatic database initialization with sample data

## Architecture

This solution follows a layered architecture pattern with clear separation of concerns:

```
SocialMediaFeed/
|
+-- socialmediafeed.client/          (Frontend - React/TypeScript)
|   +-- src/
|   +-- public/
|   +-- package.json
|
+-- SocialMediaFeed.Server/          (Presentation Layer - ASP.NET Core)
|   +-- Controllers/
|   +-- Areas/Identity/
|   +-- Pages/
|   +-- Program.cs
|
+-- SocialMediaFeed.BLL/             (Business Logic Layer)
|   +-- Services/
|   +-- Interfaces/
|   +-- Models/
|   +-- SocialMediaFeedMappingProfile.cs
|
+-- SocialMediaFeed.DAL/             (Data Access Layer)
|   +-- Entities/
|   +-- Models/
|   +-- Migrations/
|   +-- SocialMediaFeedContext.cs
|   +-- SocialMediaFeedSeeder.cs
```

### Layer Responsibilities

**Frontend (socialmediafeed.client)**
- React-based user interface
- TypeScript for type safety
- Vite for fast development and building
- Bootstrap for responsive design
- FontAwesome icons for enhanced UX

**Presentation Layer (SocialMediaFeed.Server)**
- ASP.NET Core Web API controllers
- Razor Pages for Identity UI
- Authentication and authorization middleware
- Swagger/OpenAPI documentation
- Static file serving for SPA

**Business Logic Layer (SocialMediaFeed.BLL)**
- Service implementations (PostService)
- Data Transfer Objects (DTOs)
- AutoMapper profiles for object mapping
- Business rules and validation

**Data Access Layer (SocialMediaFeed.DAL)**
- Entity Framework Core DbContext
- Entity models (Post, Like)
- Database migrations
- Data seeding logic

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) - Required for backend
- [Node.js](https://nodejs.org/) (v18 or higher) - Required for frontend
- [PostgreSQL](https://www.postgresql.org/download/) - Database server
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (recommended)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/paweldywan/SocialMediaFeed.git
   cd SocialMediaFeed
   ```

2. **Configure the database connection**
   
   Create a `appsettings.Development.json` file in `SocialMediaFeed.Server/` with your PostgreSQL connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=socialmediafeed;Username=your_username;Password=your_password"
     }
   }
   ```

3. **Restore backend dependencies**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations**
   ```bash
   cd SocialMediaFeed.Server
   dotnet ef database update --project ../SocialMediaFeed.DAL
   cd ..
   ```

5. **Install frontend dependencies**
   ```bash
   cd socialmediafeed.client
   npm install
   cd ..
   ```

### Running the Application

#### Development Mode

The easiest way to run the application is to start the backend server, which will automatically proxy the frontend:

```bash
cd SocialMediaFeed.Server
dotnet run
```

The application will be available at:
- **HTTPS:** https://localhost:7273
- **HTTP:** http://localhost:5232
- **Swagger UI:** https://localhost:7273/swagger

The SPA proxy will automatically start the Vite development server for the React frontend.

#### Running Frontend Separately (Optional)

If you want to run the frontend independently:

```bash
cd socialmediafeed.client
npm run dev
```

The frontend will be available at http://localhost:5173

#### Building for Production

Build the entire solution:

```bash
dotnet build --configuration Release
```

Build the frontend for production:

```bash
cd socialmediafeed.client
npm run build
cd ..
```

Publish the backend:

```bash
dotnet publish SocialMediaFeed.Server/SocialMediaFeed.Server.csproj --configuration Release --output ./publish
```

## Technology Stack

### Backend

- **Framework:** ASP.NET Core 8.0
- **Language:** C# 12
- **ORM:** Entity Framework Core 8.0
- **Database:** PostgreSQL (Npgsql provider)
- **Authentication:** ASP.NET Core Identity
- **API Documentation:** Swashbuckle (Swagger)
- **Object Mapping:** AutoMapper 13.0
- **Security:** Data Protection with EF Core

### Frontend

- **Framework:** React 18.3
- **Language:** TypeScript 5.6
- **Build Tool:** Vite 5.4
- **UI Framework:** Bootstrap 5.3 / Reactstrap 9.2
- **Icons:** FontAwesome 6.6
- **Linting:** ESLint 9.13

### Development Tools

- **IDE:** Visual Studio 2022
- **Package Manager:** NuGet (.NET) / npm (Node.js)
- **Version Control:** Git

## API Documentation

The API provides RESTful endpoints for managing posts and user interactions.

### Endpoints

#### Posts

| Method | Endpoint | Description | Authorization |
|--------|----------|-------------|---------------|
| GET | `/api/post` | Retrieve all posts | Required |
| POST | `/api/post` | Create a new post | Required |
| PUT | `/api/post` | Update an existing post | Required |
| DELETE | `/api/post/{id}` | Delete a post | Required |
| POST | `/api/post/{id}/like` | Toggle like on a post | Required |

### Request/Response Examples

#### Create a Post

**Request:**
```http
POST /api/post
Content-Type: application/json
Authorization: Bearer {token}

{
  "content": "This is my first post!",
  "postId": null
}
```

**Response:**
```http
HTTP/1.1 200 OK
```

#### Get All Posts

**Request:**
```http
GET /api/post
Authorization: Bearer {token}
```

**Response:**
```json
[
  {
    "id": 1,
    "content": "This is my first post!",
    "createdAt": "2024-10-30T20:38:49.000Z",
    "postId": null,
    "userName": "user@example.com",
    "likesCount": 5,
    "isLikedByCurrentUser": true,
    "posts": []
  }
]
```

### Authentication

All API endpoints require authentication using ASP.NET Core Identity. The application uses cookie-based authentication for web requests. Unauthorized requests return HTTP 401 status.

### Interactive Documentation

When running in development mode, visit https://localhost:7273/swagger to access the interactive Swagger UI for testing API endpoints.

## Project Structure

### Backend Projects

**SocialMediaFeed.Server**
- Entry point and presentation layer
- API controllers and routing
- ASP.NET Core Identity scaffolded pages
- Middleware configuration
- Dependency injection setup

**SocialMediaFeed.BLL**
- Business logic services (IPostService, PostService)
- DTOs (PostDto, PostToAdd, PostToUpdate)
- AutoMapper configuration
- Business validation rules

**SocialMediaFeed.DAL**
- Entity models (Post, Like, SimplePost)
- EF Core DbContext configuration
- Database migrations
- Data seeding utilities

### Frontend Structure

```
socialmediafeed.client/
+-- src/
|   +-- components/     (React components)
|   +-- assets/         (Images, styles)
|   +-- App.tsx         (Main application component)
|   +-- main.tsx        (Entry point)
+-- public/             (Static assets)
+-- index.html          (HTML template)
+-- vite.config.ts      (Vite configuration)
+-- tsconfig.json       (TypeScript configuration)
```

## Contributing

Contributions are welcome! Please follow these guidelines:

1. **Fork the repository**
   ```bash
   git fork https://github.com/paweldywan/SocialMediaFeed.git
   ```

2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Commit your changes**
   ```bash
   git commit -m "Add your commit message"
   ```

4. **Push to your branch**
   ```bash
   git push origin feature/your-feature-name
   ```

5. **Open a Pull Request**

### Code Style

- Follow C# coding conventions for backend code
- Use ESLint rules for frontend TypeScript/React code
- Write meaningful commit messages
- Add comments for complex logic
- Ensure all tests pass before submitting

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Author

**Pawel Dywan**

- GitHub: [@paweldywan](https://github.com/paweldywan)
- Website: https://paweldywan.com/
- Email: paweldywan@paweldywan.com

## Acknowledgments

- ASP.NET Core team for the excellent web framework
- React team for the powerful UI library
- Entity Framework Core team for the robust ORM
- PostgreSQL community for the reliable database system
- Bootstrap team for the responsive CSS framework
- All open-source contributors whose libraries made this project possible

---

**Project Repository:** https://github.com/paweldywan/SocialMediaFeed

**Live Demo:** https://socialmediafeed.paweldywan.com/
