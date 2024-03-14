FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TimelineService/TimelineService.csproj", "TimelineService/"]
COPY ["TimelineApplication/TimelineApplication.csproj", "TimelineApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["TimelineInfrastructure/TimelineInfrastructure.csproj", "TimelineInfrastructure/"]
COPY . .
RUN dotnet restore "TimelineService/TimelineService.csproj"
WORKDIR "/src/TimelineService"
RUN dotnet build "TimelineService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TimelineService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimelineService.dll"]
