#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HP.Api/HP.Api.csproj", "HP.Api/"]
COPY ["HP.Application/HP.Application.csproj", "HP.Application/"]
COPY ["HP.Domain/HP.Domain.csproj", "HP.Domain/"]
COPY ["HP.Infrastructure/HP.Infrastructure.csproj", "HP.Infrastructure/"]
RUN dotnet restore "HP.Api/HP.Api.csproj"
COPY . .
WORKDIR "/src/HP.Api"
RUN dotnet build "HP.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HP.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HP.Api.dll"]