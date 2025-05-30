# https://hub.docker.com/_/microsoft-dotnet   
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Копируем .sln и .csproj файлы правильно
COPY *.sln ./

# Переходим во внутреннюю папку с проектом
COPY SiteFetcherService/*.csproj ./SiteFetcherService/

# Восстанавливаем зависимости
RUN dotnet restore

# Копируем всё остальное
COPY . .

# Публикуем приложение
WORKDIR /source/SiteFetcherService
RUN dotnet publish -c Release -o /app --no-restore

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SiteFetcherService.dll"]