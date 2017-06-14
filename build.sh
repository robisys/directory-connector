#!/usr/bin/env bash
# dotnet restore && dotnet build
# dotnet new --info
# dotnet  --info
# dotnet  new --help
# dotnet  test --help
# cd src/Core
# cd src/Mail
cd src/App
#dotnet sln  bitwarden-directory-connector-noinstaller.sln  list
dotnet restore 
dotnet build
#dotnet build -f 461
 dotnet run
