# asp.net core Lite Template

Template identity details:
- Identity: `KarateJB.AspNetCore.LiteTemplate.CSharp`
- Short name: `aspnetcore-lite`
- nuget package: https://www.nuget.org/packages/KarateJB.AspNetCore.LiteTemplate


## How to use

Install the dotnet template.
```bash
dotnet new install KarateJB.AspNetCore.LiteTemplate
```

Create a new project by
```bash
dotnet new aspnetcore-lite [--name myapp]
```

---
## Features

- **Clean Architecture** with `webapi`, `application`, `infrastructure`, `domain`, `shared`, and `tests` projects.
- **Environment-aware configuration** with `appsettings.json`, `appsettings.{Environment}.json`, environment variables, command-line arguments, and User Secrets in development.
- **Environment variable templating in JSON config** by using double curly braces such as `{{ postgres_dbconnection }}` in `appsettings.*.json`.
- **Feature flags** via `Microsoft.FeatureManagement`.
- **OpenAPI/Swagger** enabled in development for API discovery and testing.
- **NLog-based application and HTTP request logging** with a built-in action filter for request and response logs.
- **MediatR and AutoMapper** prewired in the application layer for CQRS-style request handling and mapping.
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
    "Phone" VARCHAR(20),
    "RegisterOn" TIMESTAMPTZ,
    "IsEnabled" BOOLEAN DEFAULT TRUE,
    CONSTRAINT "PK_Folks" PRIMARY KEY ("Id")
);
```

2. Update the DB connection string in "appsettings.Development.json"



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
curl "https://localhost:5001/api/Members/7f171575-23e6-4289-ba20-7719908b6f3f" -k --include
```

#### Delete member

```bash
curl -X DELETE "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include
```

---
## Notes

- This repository includes a workflow that will publish the template to nuget.org either when:
  - Git push to `main` branch.
  - Push tag 'v*', e.g. `git tag v1.0.0 && git push origin v1.0.0`
