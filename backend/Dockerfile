# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY backend/*.sln ./
COPY backend/WebApi/*.csproj ./WebApi/
RUN dotnet restore
COPY backend/ ./
RUN dotnet publish -c Release -o out

# Production
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as production
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "WebApi.dll" ]