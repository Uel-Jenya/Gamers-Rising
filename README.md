# Gamers Rising

Gamers Rising is an ASP.NET Core MVC web app for managing games and tournaments, with authentication and an admin area for content management.

## Tech Stack

- ASP.NET Core MVC (.NET 7)
- ASP.NET Core Identity
- Entity Framework Core (SQL Server)
- Razor Views + Bootstrap
- Flatpickr / Tempus Dominus / Font Awesome (frontend libraries)

## Features

- User registration, login, and logout
- Identity-based authorization
- Admin-only management area (`/Admin`)
- CRUD flows for:
  - Games
  - Tournaments
  - Tournament Types
  - Tournament Formats
  - Tournament Modes
  - Tournament Content

## Repository Structure

- `GamersRisingWeb/GamersRisingWeb.sln` — solution file
- `GamersRisingWeb/GamersRising/` — main web app
  - `Areas/Admin/` — admin controllers and views
  - `Controllers/` — public/auth controllers
  - `Data/` — `ApplicationDbContext` + EF migrations
  - `Entities/` — domain models
  - `Views/` — Razor views
  - `wwwroot/` — static assets

## Prerequisites

- .NET SDK 7.0
- SQL Server (or SQL Server Express/LocalDB)

## Getting Started

1. Open a terminal at:
   `GamersRisingWeb/GamersRising`
2. Restore packages:
   `dotnet restore`
3. Configure your DB connection in `appsettings.Development.json` (or user secrets) under:
   - `ConnectionStrings:DefaultConnection`
4. Apply migrations:
   `dotnet ef database update`
5. Run the app:
   `dotnet run`
6. Open:
   - `https://localhost:7165`
   - or `http://localhost:5101`

## Admin Access

- Admin controllers require the `Admin` role.
- A migration (`20230404205135_Add_AdminAccount`) seeds an admin user and role.
- If you use seeded credentials, change the password immediately in any non-local environment.

## Notes

- `appsettings.json` currently contains a machine-specific SQL Server connection string; replace it with your local/deployment value.
- If running in team/shared environments, prefer environment variables or user secrets for sensitive configuration.