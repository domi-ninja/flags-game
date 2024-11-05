#!/usr/bin/env bash
cd $(dirname $0)
ssh root@games pkill flags-game
dotnet publish -a x64 --self-contained
rsync -av ./bin/Release/ root@games:flags-game
rsync -av wwwroot root@games:flags-game/net8.0/linux-x64/
rsync -av flags.db root@games:flags-game/net8.0/linux-x64/

ssh root@games mkdir -p ~/flags-game/net8.0/linux-x64/
ssh root@games cd ~/flags-game/net8.0/linux-arm64/ && ./flags-game
