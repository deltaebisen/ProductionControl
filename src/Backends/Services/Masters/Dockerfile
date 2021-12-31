#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 1000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Backends/Services/Masters/Masters.csproj", "src/Backends/Services/Masters/"]
COPY ["src/Backends/BasicBuildingBlocks/BasicBuildingBlocks.csproj", "src/Backends/BasicBuildingBlocks/"]
RUN dotnet restore "src/Backends/Services/Masters/Masters.csproj"
COPY . .
WORKDIR "/src/src/Backends/Services/Masters"
RUN dotnet build "Masters.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Masters.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Masters.dll"]