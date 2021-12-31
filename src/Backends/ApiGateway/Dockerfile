#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Backends/ApiGateway/ApiGateways.csproj", "src/Backends/ApiGateway/"]
RUN dotnet restore "src/Backends/ApiGateway/ApiGateways.csproj"
COPY . .
WORKDIR "/src/src/Backends/ApiGateway"
RUN dotnet build "ApiGateways.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateways.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateways.dll"]
