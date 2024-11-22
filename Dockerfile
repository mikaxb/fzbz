
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

COPY . /.

WORKDIR /.

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /out .

ENTRYPOINT ["dotnet", "FzBz.Api.dll"]
