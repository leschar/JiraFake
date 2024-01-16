#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./JiraFake.Api/JiraFake.Api.csproj", "JiraFake.Api/"]
COPY ["JiraFake.Application/JiraFake.Application.csproj", "JiraFake.Application/"]
COPY ["JiraFake.Domain/JiraFake.Domain.csproj", "JiraFake.Domain/"]
COPY ["JiraFake.Data/JiraFake.Data.csproj", "JiraFake.Data/"]
RUN dotnet restore "./JiraFake.Api/./JiraFake.Api.csproj"
COPY . .
WORKDIR "/src/JiraFake.Api"
RUN dotnet build "./JiraFake.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JiraFake.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JiraFake.Api.dll"]