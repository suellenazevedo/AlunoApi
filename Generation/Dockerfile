FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS http://*:80
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Generation.csproj", "."]
RUN dotnet restore "./Generation.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Generation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Generation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Generation.dll"]