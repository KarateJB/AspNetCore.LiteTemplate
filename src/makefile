.PHONY: build clean restore test run_webapi publish

clean:
		dotnet clean ./app.sln -nologo -consoleLoggerParameters:NoSummary -verbosity:quiet

restore:
		dotnet restore ./app.sln

build:
		dotnet build ./app.sln --no-restore /clp:ErrorsOnly

test:
		dotnet test ./tests/app.test/app.test.csproj --no-build /clp:ErrorsOnly

run_webapi: build
		dotnet run --project ./webapi/webapi.csproj --launch-profile "Dev" --property:Configuration=Debug

publish:
		dotnet build ./app.sln --no-restore --configuration Release /clp:ErrorsOnly
		dotnet publish ./webapi/webapi.csproj -c Release
