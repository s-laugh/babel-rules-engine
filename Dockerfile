FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY esdc-rules-api/*.csproj ./esdc-rules-api/
COPY esdc-rules-api.Tests/*.csproj ./esdc-rules-api.Tests/
COPY esdc-rules-classes/*.csproj ./esdc-rules-classes/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY esdc-rules-api/. ./esdc-rules-api/
#
WORKDIR /app
RUN dotnet publish -c Release -o ./publish
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app/esdc-rules-api
#
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "esdc-rules-api.dll"]