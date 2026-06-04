FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src


COPY SatelliteEmergencyHub.Domain/SatelliteEmergencyHub.Domain.csproj                  SatelliteEmergencyHub.Domain/
COPY SatelliteEmergencyHub.Infrastructure/SatelliteEmergencyHub.Infrastructure.csproj  SatelliteEmergencyHub.Infrastructure/
COPY Application/SatelliteEmergencyHub.Application.csproj                              Application/
COPY API/SatelliteEmergencyHub.API.csproj                                              API/

RUN dotnet restore API/SatelliteEmergencyHub.API.csproj

COPY SatelliteEmergencyHub.Domain/       SatelliteEmergencyHub.Domain/
COPY SatelliteEmergencyHub.Infrastructure/ SatelliteEmergencyHub.Infrastructure/
COPY Application/                        Application/
COPY API/                                API/

RUN dotnet publish API/SatelliteEmergencyHub.API.csproj \
    -c Release \
    -o /app/publish



FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

RUN groupadd -r satellitegroup && \
    useradd -r -g satellitegroup satelliteuser

COPY --from=build /app/publish .

USER satelliteuser

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "SatelliteEmergencyHub.API.dll"]