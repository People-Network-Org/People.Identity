# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source/People.Shared

COPY ./People.Shared/*.csproj ./
RUN dotnet restore

COPY ./People.Shared ./
RUN dotnet pack --configuration Release

WORKDIR /source/People.Identity

# copy csproj and restore as distinct layers
COPY ./People.Identity/*.sln .
COPY ./People.Identity/People.Identity.Api/*.csproj ./People.Identity.Api/
COPY ./People.Identity/People.Identity.Application/*.csproj ./People.Identity.Application/
COPY ./People.Identity/People.Identity.Contracts/*.csproj ./People.Identity.Contracts/
COPY ./People.Identity/People.Identity.Domain/*.csproj ./People.Identity.Domain/
COPY ./People.Identity/People.Identity.Infrastructure/*.csproj ./People.Identity.Infrastructure/

RUN dotnet restore

# copy everything else and build app
COPY ./People.Identity ./
WORKDIR /source/People.Identity/People.Identity.Api
RUN dotnet publish -c Release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./

RUN apt-get update
RUN apt-get --yes install curl

ENV ASPNETCORE_URLS=http://+:5279
HEALTHCHECK CMD curl --fail http://localhost:5279/healthz || exit

EXPOSE 5279

ENTRYPOINT ["dotnet", "People.Identity.Api.dll"]