# Stage 2: Сборка проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем только .csproj для восстановления NuGet
COPY ["SiteFetcherService/SiteFetcherService.csproj", "SiteFetcherService/"]
RUN dotnet restore "./SiteFetcherService/SiteFetcherService.csproj"

# Копируем остальные файлы
COPY . .
WORKDIR "/src/SiteFetcherService"

# Вывод текущего пути и списка файлов
RUN pwd && ls -la

# Сборка
RUN dotnet build "./SiteFetcherService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Публикация проекта
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Публикация + вывод списка файлов после публикации
RUN dotnet publish "./SiteFetcherService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN ls -la /app/publish

# Stage 4: Production-образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копирование + вывод содержимого /app
COPY --from=publish /app/publish .
RUN ls -la /app

ENTRYPOINT ["dotnet", "SiteFetcherService.dll"]