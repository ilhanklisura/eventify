# Eventify

Eventify is a web application that connects event organizers with attendees. Organizers manage events, venues, and tickets; users discover events and make bookings.

- **Backend:** C# .NET 8 Web API, Entity Framework Core, SQL Server, JWT, FluentMigrator, SignalR, Swagger
- **Frontend:** Vue 3, TypeScript, Vite, Vuetify
- **API:** REST + SignalR hubs, documented with OpenAPI (Swagger)

---

## Features

- **Authentication:** JWT login and registration; role-based access (attendee, organizer, admin)
- **RBAC:** Roles (`Roles`), permissions (`Permissions`), role-permission and user-role mappings; `[Authorize("claim:permission:...")]`
- **CRUD:** Users, Events, Categories, Venues, Tickets, Bookings (with Add/Edit/Delete UI)
- **Permissions:** Claim-based authorization (e.g. `event_list`, `event_create`, `user_list`)
- **Audit:** Created/Updated/Deleted tracking (BaseModel), AutoHistory `HistoryLog`
- **Real‑time:** SignalR hub (`/hubs/notifications`) + frontend toast notifications

---

## Tech stack

| Layer     | Technology |
|----------|------------|
| Backend  | .NET 8, ASP.NET Core, EF Core, SQL Server, FluentMigrator, BCrypt, SignalR, Swagger |
| Frontend | Vue 3, TypeScript, Vite, Vuetify, Vue Router, Vue I18n, VueUse, vue3-toastify |
| API      | REST (JSON), JWT Bearer, SignalR hubs |

---

## Prerequisites

- **.NET 8 SDK**
- **Node.js 18+** (for frontend)
- **SQL Server** (LocalDB or full SQL Server instance)

---

## Backend setup and run

1. **Connection string**  
   Edit `backend/appsettings.json` under `AppOptions:ConnectionStrings:Strings` and set your SQL Server connection:

   ```json
   "Value": "Server=(localdb)\\mssqllocaldb;Database=Eventify;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```

   For a named instance or remote server, change `Server=...` accordingly.

2. **JWT (optional)**  
   Under `AppOptions:TokenOptions` you can override:

   - `SigningKey` – secret for signing tokens (min 32 characters; use a strong value in production)
   - `Issuer` – token issuer (e.g. `eventify_api`)
   - `TokenDurationHours` – token lifetime (default 8)

3. **Run backend**

   ```bash
   cd backend
   dotnet run
   ```

   By default the API is at **http://localhost:5030**. Swagger UI: **http://localhost:5030/swagger**.

   On first run, FluentMigrator applies migrations and creates the database (including seed data for roles and permissions).

---

## Frontend setup and run

1. **API URL**  
   For local development the frontend is configured to call the backend at `http://localhost:5030/api` (see `frontend/.env.development`).  
   To use a different URL, set `VITE_API_URL` in `.env.development`.

2. **Install and run**

   ```bash
   cd frontend
   npm install
   npm run dev
   ```

   The app is served at **http://localhost:5173** (Vite default).  
   For production build: `npm run build` (output in `frontend/dist`).

---

## Running everything with Docker

The repo contains a `docker-compose.yml` that starts:

- **db** – SQL Server 2022 container
- **backend** – .NET API (host port `5030`)
- **frontend** – Vue + Nginx (host port `5173`)

Run:

```bash
docker compose up --build
```

Once all containers are running:

- Frontend: `http://localhost:5173`
- Backend Swagger: `http://localhost:5030/swagger`

The connection string and other AppOptions for containers are overridden via environment variables in `docker-compose.yml`.

---

## Configuration reference

### Backend (`backend/appsettings.json`)

- **AppOptions:ConnectionStrings**
  - `Default` – name of the default connection (e.g. `"Default"`)
  - `Strings` – list of `{ "Name", "Type", "Value" }`; `Type` must be `sqlserver` for this project.

- **AppOptions:TokenOptions**
  - `SigningKey` – JWT signing key (min 32 chars; keep secret in production)
  - `Issuer` – issuer claim (e.g. `eventify_api`)
  - `TokenDurationHours` – token validity in hours

### Frontend (env)

- `VITE_API_URL` – base URL of the API (e.g. `http://localhost:5030/api` in dev, or `/api` with proxy)
- `VITE_TOKEN_KEY`, `VITE_USER_KEY`, `VITE_PERMISSIONS_KEY`, `VITE_REMEMBER_KEY` – localStorage keys (defaults in `.env` / `.env.development`)

---

## Project structure (after refactor)

```
eventify/
├── backend/                 # .NET 8 Web API
│   ├── Config/              # Data, Auth, Mvc, Swagger, Option, Dependency
│   ├── Common/              # Auth (Token, Hash), Extensions, Services, Attributes
│   ├── Controllers/         # API controllers
│   ├── Hubs/                # SignalR hubs (NotificationsHub)
│   ├── Models/
│   │   ├── Data/            # DataContext, Entities, Migrations (FluentMigrator), Util
│   │   ├── Option/          # ConnectionStrings, TokenOptions, AppOptions
│   │   ├── Request/         # Request DTOs
│   │   └── Response/        # Response DTOs
│   ├── Services/            # Business logic (Default/, Result/, Codebook, etc.)
│   ├── Mapping/             # Mappers
│   └── Security/            # SecurityFilters, ISecurityHandler
│
└── frontend/                # Vue 3 SPA
    ├── src/
    │   ├── app/             # Main, auth (SignIn, Register), home (Dashboard, events, categories, …)
    │   ├── components/      # layout, forms, pages, dialogs (ConfirmDialog)
    │   └── lib/             # api (resources), auth, core (router, vuetify, toastify), validation, settings, signalr
    ├── .env.development     # VITE_API_URL etc.
    └── index.html
```

---

## Tests

Backend test project:

- `backend/backend.Tests` (xUnit) – currently covers `TokenCodec` (encode/decode round‑trip and invalid signature)

Run:

```bash
cd backend
dotnet test backend.sln -c Release
```

You can add your own unit and integration tests to this project.

---

## CI/CD

**CI:**  
`.github/workflows/ci.yml`:

- Backend job:
  - `dotnet restore`, `dotnet build`, `dotnet test` for `backend/backend.sln`
- Frontend job:
  - `npm ci`, `npm run build` in `frontend/`

**Deploy (DigitalOcean Apps):**  
`.github/workflows/deploy.yml` triggers DigitalOcean App deploy for backend and frontend:

- uses repo secrets: `DO_APP_ID_BACKEND`, `DO_APP_ID_FRONTEND`, `DO_API_TOKEN`

---

Thank you for using Eventify.
