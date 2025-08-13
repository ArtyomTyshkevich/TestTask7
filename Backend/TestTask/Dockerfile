
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TestTask/TestTask.Api.csproj", "TestTask/"]
COPY ["TestTask.Application/TestTask.Application.csproj", "TestTask.Application/"]
COPY ["TestTask.Domain/TestTask.Domain.csproj", "TestTask.Domain/"]
COPY ["TestTask.Infrastructure/TestTask.Infrastructure.csproj", "TestTask.Infrastructure/"]
RUN dotnet restore "./TestTask/TestTask.Api.csproj"
COPY . .
WORKDIR "/src/TestTask"
RUN dotnet build "./TestTask.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TestTask.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src /src 
ENTRYPOINT ["dotnet", "TestTask.Api.dll"]