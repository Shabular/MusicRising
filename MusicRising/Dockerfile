FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MusicRising/MusicRising.csproj", "MusicRising/"]
RUN dotnet restore "MusicRising/MusicRising.csproj"
COPY . .
WORKDIR "/src/MusicRising"
RUN dotnet build "MusicRising.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicRising.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicRising.dll"]
