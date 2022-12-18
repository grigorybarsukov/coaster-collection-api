FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/CoasterCollection.sln"
RUN dotnet build "src/CoasterCollection.sln" -c Debug -o /app/build --no-restore
RUN dotnet publish "src/WebApi/WebApi.csproj" -c Debug -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build publish/ ./
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "WebApi.dll"]
