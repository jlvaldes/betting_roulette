FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Api/Roulette.Api/Roulette.Api.csproj", "src/Api/Roulette.Api/"]
COPY ["src/Common/Roulette.DependencyInjection/Roulette.DependencyInjection.csproj", "src/Common/Roulette.DependencyInjection/"]
COPY ["src/Services/Roulette.Services/Roulette.Services.csproj", "src/Services/Roulette.Services/"]
COPY ["src/Common/Roulette.Model/Roulette.Model.csproj", "src/Common/Roulette.Model/"]
COPY ["src/Common/Roulette.Exceptions/Roulette.Exceptions.csproj", "src/Common/Roulette.Exceptions/"]
RUN dotnet restore "src/Api/Roulette.Api/Roulette.Api.csproj"
COPY . .
WORKDIR "/src/src/Api/Roulette.Api"
RUN dotnet build "Roulette.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roulette.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roulette.Api.dll"]