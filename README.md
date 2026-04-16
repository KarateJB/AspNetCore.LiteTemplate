# asp.net core Lite Template

---
## Architecture

WebAPI (Presentation) 
  ↓ (depends on)
Application (Contracts: IMemberRepository, IMemberService)
  ↓ (depends on)
Infrastructure (Implementations: MemberRepository)
  ↓ (depends on)
Domain (business entities, value objects, and domain logic)

---
## Sample

## Members API

Base URL: `https://localhost:5001`

Replace `YOUR_MEMBER_ID` with an actual member ID when calling the `GET`, `PUT`, and `DELETE` endpoints.

### Create member

```bash
curl -X POST 'https://localhost:5001/api/Members' -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"123 Main St","phone":"555-0100","isEnabled":true}' | tail -n1 | jq ""
```

To export the Memeber ID from the response:
```bash
export MEMBER_ID=$(curl -X POST 'https://localhost:5001/api/Members' -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"123 Main St","phone":"555-0100","isEnabled":true}' | tail -n1 | jq -r ".id")
```

### Update member

```bash
curl -X PUT "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include \
  -H 'Content-Type: application/json' \
  -d '{"name":"Jane Doe","birthday":"1995-05-20T00:00:00+00:00","address":"456 Oak Ave","phone":"555-0101","isEnabled":true}'
```

### Find member

```bash
curl "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include
```

### Delete member

```bash
curl -X DELETE "https://localhost:5001/api/Members/${MEMBER_ID}" -k --include
```

---

### SQL Server

```sql
CREATE TABLE Members
(
    Id UNIQUEIDENTIFIER DEFAULT NEWID(),
    Name NVARCHAR(50),
    Birthday DATETIMEOFFSET,
    Address NVARCHAR(100),
    Phone VARCHAR(20),
    RegisterOn DATETIMEOFFSET,
    IsEnabled BIT DEFAULT 1,
    CONSTRAINT PK_Folks PRIMARY KEY NONCLUSTERED (Id)
)
```

### PostgreSQL

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
