FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["BookingManagmentSystem.API/BookingManagmentSystem.API.csproj", "BookingManagmentSystem.API/"]

RUN dotnet restore "BookingManagmentSystem.API/BookingManagmentSystem.API.csproj"

COPY . .

WORKDIR "/src/BookingManagmentSystem.API"

RUN dotnet publish "BookingManagmentSystem.API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "BookingManagmentSystem.API.dll"]