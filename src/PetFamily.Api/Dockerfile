FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PetFamily.Api/PetFamily.Api.csproj", "PetFamily.Api/"]
COPY ["PetFamily.Infrastructure/PetFamily.Infrastructure.csproj", "PetFamily.Infrastructure/"]
COPY ["PetFamily.UseCases/PetFamily.UseCases.csproj", "PetFamily.UseCases/"]
COPY ["PetFamily.Domain/PetFamily.Domain.csproj", "PetFamily.Domain/"]
RUN dotnet restore "PetFamily.Api/PetFamily.Api.csproj"
COPY . .
WORKDIR "/src/PetFamily.Api"
RUN dotnet build "PetFamily.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetFamily.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetFamily.Api.dll"]
