FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BasketBuddy.Api/BasketBuddy.Api.csproj", "BasketBuddy.Api/"]
COPY ["BasketBuddy.Core/BasketBuddy.Core.csproj", "BasketBuddy.Core/"]
RUN dotnet restore "BasketBuddy.Api/BasketBuddy.Api.csproj"
RUN dotnet restore "BasketBuddy.Core/BasketBuddy.Core.csproj"
COPY . .
WORKDIR "/src/BasketBuddy.Api"
RUN dotnet build "BasketBuddy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/BasketBuddy.Core"
RUN dotnet build "BasketBuddy.Core.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BasketBuddy.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet publish "BasketBuddy.Core.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketBuddy.Api.dll"]
