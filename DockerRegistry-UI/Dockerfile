FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DockerRegistry-UI/DockerRegistry-UI.csproj", "DockerRegistry-UI/"]
RUN dotnet restore "DockerRegistry-UI/DockerRegistry-UI.csproj"
COPY . .
WORKDIR "/src/DockerRegistry-UI"
RUN dotnet build "DockerRegistry-UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerRegistry-UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerRegistry-UI.dll"]