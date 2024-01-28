#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Authentication.Register.Api/Authentication.Register.Api.csproj", "src/Presentation/Authentication.Register.Api/"]
COPY ["src/Application/Authentication.Interfaces/Authentication.Interfaces.csproj", "src/Application/Authentication.Interfaces/"]
COPY ["src/Application/Authenticaton.Application/Authentication.Application.csproj", "src/Application/Authenticaton.Application/"]
COPY ["src/Domain/Authentication.Domain/Authentication.Domain.csproj", "src/Domain/Authentication.Domain/"]
COPY ["src/Infrastructure/Authentication.Infrastructure/Authentication.Infrastructure.csproj", "src/Infrastructure/Authentication.Infrastructure/"]
COPY ["src/CrossCutting/Authentication.Common.Helpers/Authentication.Common.Helpers.csproj", "src/CrossCutting/Authentication.Common.Helpers/"]
COPY ["src/CrossCutting/Authentication.Common.Logging/Authentication.Common.Logging.csproj", "src/CrossCutting/Authentication.Common.Logging/"]
COPY ["src/CrossCutting/Authentication.Common.TokenManager/Authentication.Common.TokenManager.csproj", "src/CrossCutting/Authentication.Common.TokenManager/"]
RUN dotnet restore "./src/Presentation/Authentication.Register.Api/./Authentication.Register.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Authentication.Register.Api"
RUN dotnet build "./Authentication.Register.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authentication.Register.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Register.Api.dll"]