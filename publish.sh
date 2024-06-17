#!/usr/bin/env bash
cd $(dirname $0)
dotnet publish -a arm64 --self-contained
rsync -av ./bin/Release/ root@95.217.221.222:flags-game
rsync -av wwwroot root@95.217.221.222:flags-game/net8.0/linux-arm64/
rsync -av flags.db root@95.217.221.222:flags-game/net8.0/linux-arm64/


