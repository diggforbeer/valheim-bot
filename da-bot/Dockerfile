#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["da-bot.csproj", "."]
RUN dotnet restore "./da-bot.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "da-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "da-bot.csproj" -c Release -o /app/publish

FROM base AS final
ENV discordWebHook=123
ENV LOL="1"
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "da-bot.dll"]