FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

#copy csproj and restore as distinct layers
COPY *.csproj ./MicroTest.FileApi/
WORKDIR /app/MicroTest.FileApi
RUN dotnet restore

#copy everything else and build
WORKDIR /app
COPY . ./MicroTest.FileApi/
WORKDIR /app/MicroTest.FileApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app/MicroTest.FileApi
COPY --from=build /app/MicroTest.FileApi/out ./
ENTRYPOINT [ "dotnet", "MicroTest.FileApi.dll" ]