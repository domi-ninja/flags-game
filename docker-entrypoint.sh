#!/bin/sh

set -eu

if [ ! -f /data/flags.db ]; then
    cp /app/flags.seed.db /data/flags.db
fi

exec dotnet /app/flags-game.dll
