FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8000

ENV ASPNETCORE_URLS=http://+:8000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Sistema/GMTasker.API/GMTasker.API.csproj", "Sistema/GMTasker.API/"]
RUN dotnet restore "Sistema/GMTasker.API/GMTasker.API.csproj"
COPY . .
WORKDIR "/src/Sistema/GMTasker.API"
RUN dotnet build "GMTasker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GMTasker.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GMTasker.API.dll"]
