FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

WORKDIR /src
COPY flags-game.csproj ./
RUN dotnet restore flags-game.csproj

COPY . .
RUN dotnet publish flags-game.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS runtime

RUN apt-get update \
    && apt-get install --yes --no-install-recommends curl libgdiplus \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish ./
COPY flags.db ./flags.seed.db
COPY --chmod=755 docker-entrypoint.sh /usr/local/bin/docker-entrypoint.sh

RUN mkdir /data && chown "$APP_UID:$APP_UID" /data

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

USER $APP_UID
ENTRYPOINT ["docker-entrypoint.sh"]
