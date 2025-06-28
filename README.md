# SocialMediaFeed

A full-stack social media feed application built with:

- **Backend:** ASP.NET Core (C#)
- **Frontend:** React (TypeScript, Vite)
- **Database:** Entity Framework Core (EF Core)

## Features
- View, add, update, and delete social media posts
- Like posts
- Modern, responsive UI
- RESTful API

## Project Structure
- `SocialMediaFeed.BLL/` - Business logic layer (services, DTOs)
- `SocialMediaFeed.DAL/` - Data access layer (entities, EF Core context, migrations)
- `SocialMediaFeed.Server/` - ASP.NET Core Web API server
- `socialmediafeed.client/` - React frontend (Vite, TypeScript)

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)

### Backend Setup
1. Navigate to the solution root:
   ```sh
   cd SocialMediaFeed
   ```
2. Restore and build the backend:
   ```sh
   dotnet restore
   dotnet build
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update --project SocialMediaFeed.DAL --startup-project SocialMediaFeed.Server
   ```
4. Run the server:
   ```sh
   dotnet run --project SocialMediaFeed.Server
   ```

### Frontend Setup
1. Navigate to the client folder:
   ```sh
   cd socialmediafeed.client
   ```
2. Install dependencies:
   ```sh
   npm install
   ```
3. Start the development server:
   ```sh
   npm run dev
   ```

### Accessing the App
- Frontend: [http://localhost:5173](http://localhost:5173)
- Backend API: [http://localhost:5000](http://localhost:5000) (default)

## Development
- Backend code: `SocialMediaFeed.BLL/`, `SocialMediaFeed.DAL/`, `SocialMediaFeed.Server/`
- Frontend code: `socialmediafeed.client/src/`

## License
MIT

---

Live demo: https://socialmediafeed.paweldywan.com/
