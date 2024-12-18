FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

# Используем образ SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PublisherMQTT/PublisherMQTT.csproj", "./"]
RUN dotnet restore "./PublisherMQTT.csproj"
COPY . .
RUN dotnet build "PublisherMQTT.csproj" -c Release -o /app/build

# Используем образ Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "PublisherMQTT.dll"]


