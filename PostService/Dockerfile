FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PostService/PostService.csproj", "PostService/"]
COPY ["PostApplication/PostApplication.csproj", "PostApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["PostInfrastructure/PostInfrastructure.csproj", "PostInfrastructure/"]
COPY . .
RUN dotnet restore "PostService/PostService.csproj"
WORKDIR "/src/PostService"
RUN dotnet build "PostService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PostService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostService.dll"]
