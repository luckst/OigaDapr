#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OigaDpr.RegisterApi/OigaDpr.RegisterApi.csproj", "OigaDpr.RegisterApi/"]
RUN dotnet restore "OigaDpr.RegisterApi/OigaDpr.RegisterApi.csproj"
COPY . .
WORKDIR "/src/OigaDpr.RegisterApi"
RUN dotnet build "OigaDpr.RegisterApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OigaDpr.RegisterApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OigaDpr.RegisterApi.dll"]