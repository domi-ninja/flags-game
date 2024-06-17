#!/usr/bin/env bash
cd $(dirname $0)
ssh root@games.domi.ninja pkill flags-game
dotnet publish -a arm64 --self-contained
rsync -av ./bin/Release/ root@games.domi.ninja:flags-game
rsync -av wwwroot root@games.domi.ninja:flags-game/net8.0/linux-arm64/
rsync -av flags.db root@games.domi.ninja:flags-game/net8.0/linux-arm64/


ssh root@games.domi.ninja tmux cd flags-game/net8.0/linux-arm64/ && ./flags-game