FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Realease -o out
RUN mkdir ./out/db

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime-app
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "api.dll"]
