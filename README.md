# ASP.NET Core Lite Template

Template identity details:
- Default target framework: net10.0
- Identity: `KarateJB.AspNetCore.LiteTemplate.CSharp`
- Short name: `aspnetcore-lite`
- nuget package: https://www.nuget.org/packages/KarateJB.AspNetCore.LiteTemplate


## How to use

Install the dotnet template.
```bash
dotnet new install KarateJB.AspNetCore.LiteTemplate [--force]
```

Create a new project by
```bash
dotnet new aspnetcore-lite [--name myapp]
```

---
## Features

- **Clean Architecture**.
- **Environment-aware configuration** with `appsettings.json`, `appsettings.{Environment}.json`, environment variables, command-line arguments, and User Secrets in development.
- **Environment variable templating in JSON config** by using double curly braces such as `{{ postgres_dbconnection }}` in `appsettings.*.json`.
- **Feature flags** via `Microsoft.FeatureManagement`.
- **OpenAPI/Swagger** enabled in development for API discovery and testing.
- **NLog-based application and HTTP request logging** with a built-in action filter for request and response logs.
- **Dapper + Npgsql PostgreSQL data access** in the infrastructure layer as the sample repository implementation.
- **Sample CRUD Members API** to use as a starting point for new endpoints and application flows.
- **NUnit test project** included as a starting point for automated tests.

Project dependencies:
```
**webapi** (presentation layer that exposes HTTP endpoints, configures middleware)
  ↓ (depends on)
**application** (contains use cases, CQRS handlers, service contracts, and object mappings.)
  ↓ (depends on)
**infrastructure** (implements external concerns such as persistence and repository access.)
  ↓ (depends on)
**domain** (business entities, value objects, and domain logic)

→ **shared** (shared cross-project types and configuration models used by multiple layers.)
```

---
## Demo

### Prerequisite

1. Create the table in PostgreSQL database.

```sql
CREATE TABLE "Members"
(
    "Id" UUID DEFAULT gen_random_uuid(),
    "Name" VARCHAR(50),
    "Birthday" DATE,
    "Address" VARCHAR(100),
    "Phone" VARCHAR(20) NOT NULL,
    "RegisterOn" TIMESTAMPTZ,
    "IsEnabled" BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT "PK_Folks" PRIMARY KEY ("Id")
);
```

2. Update the PostgreSQL DB connection string either by 
  - Environment variable `postgres_dbconnection` in "src/webapi/Properties/launchSettings.json" 
  - Use user secret in webapi project, e.g.  `dotnet user-secrets set "ConnectionStrings:PgConnection" "Host=localhost;Port=5432;Database=demo;Username=user;Password=pwd;"`


### Default API

Base URL: `https://localhost:5001`

#### Create member

```bash
curl -X POST 'https://localhost:5001/api/Members' -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20","address":"123 Main St","phone":"555-0100","isEnabled":true}'
```

To export the Member ID from the response:

```bash
export MEMBER_ID=$(curl -X POST 'https://localhost:5001/api/Members' -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20","address":"123 Main St","phone":"555-0100","isEnabled":true}' | tail -n1 | jq -r ".id")
```

#### Update member

```bash
curl -X PUT "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20","address":"456 Oak Ave","phone":"555-0101","isEnabled":true}'
```

#### Find member

```bash
curl "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include
```

#### Delete member

```bash
curl -X DELETE "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include
```


---
## Notes

- This repository includes a workflow that will publish the template to nuget.org either when:
  - Git push to `master` branch.
  - Push tag 'v*', e.g. `git tag v1.0.0 && git push origin v1.0.0`
