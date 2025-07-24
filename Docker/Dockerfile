# Мультистейдж-сборка для микросервиса на .NET 9
ARG PROJECT=Auth.Web
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Копируем только csproj и выполняем restore отдельным слоем (для кэширования)
COPY ../src/${PROJECT}/${PROJECT}.csproj src/${PROJECT}/
COPY ../src/Auth.Core/Auth.Core.csproj src/Auth.Core/
COPY ../src/Auth.UseCases/Auth.UseCases.csproj src/Auth.UseCases/
COPY ../src/Auth.Infrastructure/Auth.Infrastructure.csproj src/Auth.Infrastructure/
COPY ../src/Auth.ServiceDefaults/Auth.ServiceDefaults.csproj src/Auth.ServiceDefaults/
RUN dotnet restore src/${PROJECT}/${PROJECT}.csproj
# Копируем остальной код и публикуем
COPY .. .
WORKDIR /src/src/${PROJECT}
RUN dotnet publish "${PROJECT}.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
# Проверка работоспособности контейнера
HEALTHCHECK --interval=30s --timeout=5s --start-period=30s --retries=3 CMD curl --fail http://localhost:80/health || exit 1
ENTRYPOINT ["dotnet", "Auth.Web.dll"] 