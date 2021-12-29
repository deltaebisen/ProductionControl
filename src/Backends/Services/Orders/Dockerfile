#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductionControl.Order/ProductionControl.Orders.csproj", "ProductionControl.Order/"]
RUN dotnet restore "ProductionControl.Order/ProductionControl.Orders.csproj"
COPY . .
WORKDIR "/src/ProductionControl.Order"
RUN dotnet build "ProductionControl.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductionControl.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductionControl.Orders.dll"]