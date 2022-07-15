#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/services/CarRental.Fleet.API/CarRental.Fleet.API.csproj", "src/services/CarRental.Fleet.API/"]
RUN dotnet restore "src/services/CarRental.Fleet.API/CarRental.Fleet.API.csproj"
COPY . .
WORKDIR "/src/src/services/CarRental.Fleet.API"
RUN dotnet build "CarRental.Fleet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRental.Fleet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRental.Fleet.API.dll"]