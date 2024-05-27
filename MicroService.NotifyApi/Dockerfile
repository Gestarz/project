#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroService.NotifyApi/MicroService.NotifyApi.csproj", "MicroService.NotifyApi/"]
RUN dotnet restore "./MicroService.NotifyApi/MicroService.NotifyApi.csproj"
COPY . .
WORKDIR "/src/MicroService.NotifyApi"
RUN dotnet build "./MicroService.NotifyApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroService.NotifyApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm /app/appsettings.json
COPY ../Docker/Config/notifyapi.appsettings.json /app/appsettings.json
ENTRYPOINT ["dotnet", "MicroService.NotifyApi.dll"]