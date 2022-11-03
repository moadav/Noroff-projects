FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /app
RUN ls -a
COPY ./HvZ_API/*.csproj ./
RUN dotnet restore
COPY ./HvZ_API ./
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
RUN apk add --no-cache icu-libs
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "HvZ_API.dll"]
