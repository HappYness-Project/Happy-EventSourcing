#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorUI/BlazorUI.csproj", "BlazorUI/"]
COPY ["HP.Application/HP.Application.csproj", "HP.Application/"]
COPY ["HP.Shared/HP.Shared.csproj", "HP.Shared/"]
COPY ["HP.GeneralUI/HP.GeneralUI.csproj", "HP.GeneralUI/"]
RUN dotnet restore "BlazorUI/BlazorUI.csproj"
COPY . .
WORKDIR "/src/BlazorUI"
RUN dotnet build "BlazorUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorUI.dll"]