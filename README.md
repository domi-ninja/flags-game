# flags-game

A .NET 8 and HTMX flag guessing game.

The Docker image builds the application with the .NET SDK, then runs it on the
smaller ASP.NET runtime image. `pp deploy` publishes it to p3 at:

- `https://flags.p3.domi.ninja/` and the original singular alias
- `https://games.domi.ninja/flags/`, ready for the DNS migration

The SQLite database persists at `/data/pp/flags-game/flags.db` on p3. On the
first start, the container copies the checked-in database there as its seed.
# Deployment maintenance

`deploy.sh` now delegates to `pp deploy`; it no longer stops an unrelated host process or uploads a local SQLite database. Commit reviewed changes before deploying. `deploy.yml` uses release-specific image tags and checks both the standalone game URL and the `/flags/` route after deployment. Use `pp status` to inspect the current release. Keep `.deploy/` local and preserve the existing database bind mount.
