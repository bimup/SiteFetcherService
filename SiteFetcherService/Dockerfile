# https://hub.docker.com/_/microsoft-dotnet   
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Копируем .sln и все .csproj файлы
COPY *.sln ./
COPY **/*.csproj ./

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем весь исходный код
COPY . .

# Публикуем приложение
WORKDIR /source/SiteFetcherService
RUN dotnet publish -c Release -o /app --no-restore

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SiteFetcherService.dll"]