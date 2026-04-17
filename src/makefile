.PHONY: build clean restore test run publish template_install template_uninstall template_pack template_publish

clean:
		dotnet clean ./app.sln -nologo -consoleLoggerParameters:NoSummary -verbosity:quiet

restore:
		dotnet restore ./app.sln

build:
		dotnet build ./app.sln --no-restore /clp:ErrorsOnly

test:
		dotnet test ./tests/app.test/app.test.csproj --no-build /clp:ErrorsOnly

run: build
		dotnet run --project ./webapi/webapi.csproj --launch-profile "Dev" --property:Configuration=Debug

publish:
		dotnet build ./app.sln --no-restore --configuration Release /clp:ErrorsOnly
		dotnet publish ./webapi/webapi.csproj -c Release

template_install:
		dotnet new install . --force

template_uninstall:
		dotnet new uninstall KarateJB.AspNetCore.LiteTemplate.CSharp

template_pack:
		dotnet pack ../template-pack/AspNetCore.LiteTemplate.TemplatePack.csproj -c Release -o ../artifacts

template_publish:
		dotnet nuget push ../artifacts/KarateJB.AspNetCore.LiteTemplate.*.nupkg -k $$NUGET_API_KEY -s https://api.nuget.org/v3/index.json
