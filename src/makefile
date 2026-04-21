.PHONY: build clean restore test run publish show_installed_templates template_install_local template_uninstall_local template_install_nuget template_uninstall_nuget template_pack template_publish

TEMPLATE_SOURCE_PATH := .
TEMPLATE_PACKAGE_ID := KarateJB.AspNetCore.LiteTemplate

clean:
		dotnet clean ./app.sln -nologo -consoleLoggerParameters:NoSummary -verbosity:quiet

restore:
		dotnet restore ./app.sln

build: restore
		dotnet build ./app.sln --no-restore /clp:ErrorsOnly

test:
		dotnet test ./tests/app.test/app.test.csproj --no-build /clp:ErrorsOnly

run: build
		dotnet run --project ./webapi/webapi.csproj --launch-profile "Dev" --property:Configuration=Debug

publish:
		dotnet build ./app.sln --no-restore --configuration Release /clp:ErrorsOnly
		dotnet publish ./webapi/webapi.csproj -c Release

show_installed_templates:
		dotnet new uninstall

template_install_local:
		dotnet new install $(TEMPLATE_SOURCE_PATH) --force

template_uninstall_local:
		dotnet new uninstall $(TEMPLATE_SOURCE_PATH)

template_install_nuget:
		# dotnet new install ../artifacts/KarateJB.AspNetCore.LiteTemplate.1.0.0.nupkg
		dotnet new install $(TEMPLATE_PACKAGE_ID)

template_uninstall_nuget:
		dotnet new uninstall $(TEMPLATE_PACKAGE_ID)

template_pack:
		dotnet pack ../template-pack/AspNetCore.LiteTemplate.TemplatePack.csproj -c Release -o ../artifacts

template_publish:
		dotnet nuget push ../artifacts/KarateJB.AspNetCore.LiteTemplate.*.nupkg -k $$NUGET_API_KEY -s https://api.nuget.org/v3/index.json
