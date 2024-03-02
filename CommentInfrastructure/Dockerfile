FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommentInfrastructure/CommentInfrastructure.csproj", "CommentInfrastructure/"]
RUN dotnet restore "CommentInfrastructure/CommentInfrastructure.csproj"
COPY . .
WORKDIR "/src/CommentInfrastructure"
RUN dotnet build "CommentInfrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CommentInfrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommentInfrastructure.dll"]
