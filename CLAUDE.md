# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working in this repository.

## Repository visibility: PUBLIC

Kept public on purpose (Will wants some projects publicly visible for engagement) — confirmed 2026-09-16. This means: **never put anything sensitive in an issue, PR, or commit here** (client names, deal terms, keys, personal contact info). Flag before adding anything that wouldn't be fine for a stranger to read. See `willherr/will-i-am.dev`#30/#34 for why this matters in practice — a client negotiation posted to a public issue there got scraped by bounty-bot spam within days.

## Project overview

"Just a bunch of useful (and useless) stuff" (Will's own description) — a 2020-era Blazor WebAssembly app (`netstandard2.1`, Blazor packages pinned to `3.2.0`, both long past support). Hosted via GitHub Pages, published from `main`'s `/docs` folder. Live at the default Pages URL `willherr.github.io/WillsTools/`. It used to also be reachable at `will-i-am.dev/WillsTools/` via `willherr/will-i-am.dev`'s custom-domain path routing; that path is now dead since that repo moved its own hosting to Cloudflare Workers (`will-i-am.dev`#34) — not a regression here, an accepted tradeoff made when that migration happened.

**Not under active day-to-day development** — see the rewrite plan in the backlog below before making any non-trivial change here; a small fix might be better spent on the rewrite instead.

**Backlog** (GitHub Issues, not this file):
- **#1** — rewrite as a real Flutter app (Android, possibly Windows too), matching the stack Cassandra's 10-Key and Cassandra's Cookbook already use; once there's a Flutter web build, move web hosting to Cloudflare Workers on its own subdomain (`tools.will-i-am.dev` proposed — `will-i-am.dev`'s zone is already on Cloudflare, so this needs only a new DNS record, no nameserver work). Confirmed 2026-09-16, not urgent. Unlike Will's other apps, likely stays free with no clear paid-unlock angle — ads are a possible later addition, not a launch requirement.

This file will need a real pass once that rewrite actually starts — don't write detailed Architecture for a Blazor structure that's about to be replaced.

## Dev workflow

- Default branch is `main`.
- Feature/content work branches off `main` (pull latest first), gets a real PR even solo — never committed straight to `main`.
- Branch naming: `issue#<N>/PascalTitle` (literal `#`, issue number, `/`, then a Pascal-case short title). Quote branch names containing `#` in shell commands.
- Backlog/direction lives in GitHub Issues, not an internal markdown doc.
- **Exception: non-application changes — CLAUDE.md updates chief among them — can be committed straight to `main`, no branch/PR needed** (same carve-out every other repo of Will's uses).
- **Issue labels**: `brainstorming`/`needs decision`/`priority: high/medium/low` — the cross-repo convention, see `~/.claude/notes/issue-labels.md` for the full scheme. This repo also still has GitHub's original default label set (`bug`, `enhancement`, `question`, etc.) from before this convention existed — that's a different, non-competing axis (what kind of issue vs. how ready/urgent it is), left as-is rather than reconciled away.
- No PR gets created, and no PR gets merged, without Will's explicit confirmation first — same global rule as every other repo (`~/.claude/CLAUDE.md`).

## Commands

No CI — the app targets an unsupported `netstandard2.1`/Blazor `3.2.0` toolchain that isn't worth wiring a build check for on a repo slated for a full rewrite. Publishing is manual, per `README.md`: `dotnet publish -c Release`, then hand-copy the publish output into `docs/` (a custom MSBuild `AfterPublish` target also runs `DevOps\publish.ps1`). Don't invent a real build/test pipeline here before the rewrite in #1 lands.

## Notes

- Audit this file for drift whenever a backlog issue closes, same as every other Will repo.
