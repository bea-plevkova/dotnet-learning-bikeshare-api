FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY API/*.csproj ./API/
COPY Database/*.csproj ./Database/
COPY Services/*.csproj ./Services/

WORKDIR /app/API
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app/
COPY API/. ./API/
COPY Database/. ./Database/
COPY Services/. ./Services/

WORKDIR /app/API

RUN dotnet publish -c Release -o out

# test application -- see: dotnet-docker-unit-testing.md
#FROM build AS testrunner
#WORKDIR /app/tests
#COPY tests/. .
#ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/API/out ./
ENTRYPOINT ["dotnet", "API.dll"]