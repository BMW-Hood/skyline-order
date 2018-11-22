FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Provider/Providers.csproj", "Provider/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
ENV MYSQL_CONNECTIONSTRING  server=skyline-db;port=3306;database=skyline;user=root;password=123456;
ENV JAEGER_COLLECTOR_URL  http://skyline-tracing:14268/api/traces
ENV INFLUXDB_URL  http://skyline-influxdb:8086
ENV INFLUXDB_DATABASE  webapi_metric
ENV LOGSDIR  /temp/skyline-webapi
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAPI.dll"]