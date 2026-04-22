# Project Guidelines

## Code Style

- Target stack is C# on .NET 10 (`net10.0`) with `ImplicitUsings` and `Nullable` enabled. Follow existing patterns in `src/*/*.csproj`.
- Keep namespaces folder-aligned and lowercase where this repo already does so (for example `namespace application.Features...`).
- Preserve current DI and extension-method style for bootstrapping (`AddApplication`, `AddInfrastructure`, `AddWebApi`).
- Keep API JSON casing behavior consistent with current setup in `src/webapi/Program.cs`.

## Architecture

- This template uses layered clean architecture:
  - `src/webapi`: HTTP endpoints, middleware, filters, host setup.
  - `src/application`: use cases (commands/queries/handlers), interfaces, mapping, services.
  - `src/infrastructure`: repository and external implementation details.
  - `src/domain`: entities and value objects.
  - `src/shared`: cross-layer configuration models/constants.
- Follow project reference boundaries from `src/app.sln` and project files. Avoid introducing reverse dependencies.
- Use `Mediator` patterns already present in the project (`IRequest`, `IRequestHandler`, `IMediator`) rather than introducing a different mediator stack.

## Build And Test

- Preferred commands from repository root:
  - `dotnet restore ./src/app.sln`
  - `dotnet build ./src/app.sln --no-restore /clp:ErrorsOnly`
  - `dotnet test ./src/tests/app.test/app.test.csproj --no-build /clp:ErrorsOnly`
  - `dotnet run --project ./src/webapi/webapi.csproj --launch-profile "Dev" --property:Configuration=Debug`
- Template pack and publish flow:
  - `dotnet pack ./template-pack/AspNetCore.LiteTemplate.TemplatePack.csproj -c Release -o ./artifacts`
  - `dotnet new install ./src --force`

## Conventions

- Feature flags are first-class:
  - Register via `AddFeatureManagement`.
  - Gate endpoints with `[FeatureGate(...)]`.
  - Add constants in `src/shared/Configurations/FeatureFlags.cs`.
- Logging is NLog-based with request/response filter logging:
  - Keep NLog config in `src/webapi/NLog.config`.
  - Keep request logging behavior in `src/webapi/Filters/HttpRequestLogFilter.cs`.

## References

- Primary project and usage documentation: `README.md`
- Packaging and release automation: `.github/workflows/publish_template.yml`
