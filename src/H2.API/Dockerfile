# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["H2.API/H2.API.csproj", "H2.API/"]
COPY ["H2.Application/H2.Application.csproj", "H2.Application/"]
COPY ["H2.Domain/H2.Domain.csproj", "H2.Domain/"]
COPY ["H2.Infrastructure/H2.Infrastructure.csproj", "H2.Infrastructure/"]
RUN dotnet restore "./H2.API/H2.API.csproj"
COPY . .
WORKDIR "/src/H2.API"
RUN dotnet build "./H2.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./H2.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "H2.API.dll"]