#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["HP.Core/HP.Core.csproj", "HP.Core/"]
COPY ["HP.Application/HP.Application.csproj", "HP.Application/"]
COPY ["HP.Shared/HP.Shared.csproj", "HP.Shared/"]
COPY ["HP.Domain/HP.Domain.csproj", "HP.Domain/"]
COPY ["HP.GeneralUI/HP.GeneralUI.csproj", "HP.GeneralUI/"]
COPY ["HP.Infrastructure/HP.Infrastructure.csproj", "HP.Infrastructure/"]
COPY ["BlazorUI/BlazorUI.csproj", "BlazorUI/"]
RUN dotnet restore "BlazorUI/BlazorUI.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "BlazorUI/BlazorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorUI/BlazorUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorUI.dll"]