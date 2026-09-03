# flags-game

A .NET 8 and HTMX flag guessing game.

The Docker image builds the application with the .NET SDK, then runs it on the
smaller ASP.NET runtime image. `pp deploy` publishes it to p3 at:

- `https://flags.p3.domi.ninja/` and the original singular alias
- `https://games.domi.ninja/flags/`, ready for the DNS migration

The SQLite database persists at `/data/pp/flags-game/flags.db` on p3. On the
first start, the container copies the checked-in database there as its seed.
