FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserService/UserService.csproj", "UserService/"]
COPY ["UserApplication/UserApplication.csproj", "UserApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["UserInfrastructure/UserInfrastructure.csproj", "UserInfrastructure/"]
COPY . .
RUN dotnet restore "UserService/UserService.csproj"
WORKDIR "/src/UserService"
RUN dotnet build "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.dll"]
