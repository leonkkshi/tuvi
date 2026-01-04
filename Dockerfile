# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY Backend/Backend.csproj Backend/
RUN dotnet restore Backend/Backend.csproj

# Copy everything else and build
COPY Backend/ Backend/
WORKDIR /src/Backend
RUN dotnet build Backend.csproj -c Release -o /app/build
RUN dotnet publish Backend.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the app
ENTRYPOINT ["dotnet", "Backend.dll"]
