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
