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

### Supports Environment variable Configuration

Use double curly braces (e.g. `{{ variable }}`) in appsettings.*.json to inject the environment variable value into the configuration.
In development, you can set the values in "src/webapi/Properties/launchSettings.json" or by user secret.

### Clean Architecture

Project dependencies:
```
WebAPI (Presentation)
  ↓ (depends on)
Application (Contracts: IMemberRepository, IMemberService)
  ↓ (depends on)
Infrastructure (Implementations: MemberRepository)
  ↓ (depends on)
Domain (business entities, value objects, and domain logic)
```


---
## Demo

### Prerequisite

1. Create the table in PostgresSQL database.

```sql
CREATE TABLE "Members"
(
    "Id" UUID DEFAULT gen_random_uuid(),
    "Name" VARCHAR(50),
    "Birthday" TIMESTAMPTZ,
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
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"123 Main St","phone":"555-0100","isEnabled":true}'
```

To export the Memeber ID from the response:

```bash
export MEMBER_ID=$(curl -X POST 'https://localhost:5001/api/Members' -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"123 Main St","phone":"555-0100","isEnabled":true}' | tail -n1 | jq -r ".id")
```

#### Update member

```bash
curl -X PUT "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"456 Oak Ave","phone":"555-0101","isEnabled":true}'
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
  - Git push to `main` branch.
  - Push tag 'v*', e.g. `git tag v1.0.0 && git push origin v1.0.0`
