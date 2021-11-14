#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["GlobalBlue.Api/GlobalBlue.Api.csproj", "GlobalBlue.Api/"]
COPY ["GlobalBlue.Dtos/GlobalBlue.Dtos.csproj", "GlobalBlue.Dtos/"]
COPY ["GlobalBlue.Infrastructure/GlobalBlue.Infrastructure.csproj", "GlobalBlue.Infrastructure/"]
RUN dotnet restore "GlobalBlue.Api/GlobalBlue.Api.csproj"
COPY . .
WORKDIR "/src/GlobalBlue.Api"
RUN dotnet build "GlobalBlue.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GlobalBlue.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlobalBlue.Api.dll"]