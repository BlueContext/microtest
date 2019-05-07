FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-preview5 AS build
WORKDIR /app

#copy csproj and restore as distinct layers
COPY *.csproj ./MicroTest.Api/
WORKDIR /app/MicroTest.Api
RUN dotnet restore

#copy everything else and build
WORKDIR /app
COPY . ./MicroTest.Api/
WORKDIR /app/MicroTest.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.100-preview5 AS runtime
WORKDIR /app/MicroTest.Api
COPY --from=build /app/MicroTest.Api/out ./
ENTRYPOINT [ "dotnet", "MicroTest.Api.dll" ]