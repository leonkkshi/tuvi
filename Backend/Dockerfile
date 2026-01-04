# Sử dụng .NET 9.0 (10.0 chưa release)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj và restore
COPY ["Backend.csproj", "./"]
RUN dotnet restore "Backend.csproj"

# Copy tất cả và build
COPY . .
RUN dotnet build "Backend.csproj" -c Release -o /app/build
RUN dotnet publish "Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Backend.dll"]
