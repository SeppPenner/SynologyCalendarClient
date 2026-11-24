# Project rules for Claude

## What this is

SynologyCalendarClient is a client library for the Synology calendar web API (`SYNO.API.Auth`,
`SYNO.API.Info`, `SYNO.Cal.Cal`, `SYNO.Cal.Event`, `SYNO.Cal.Todo`). It is published as the NuGet
package `HaemmerElectronics.SeppPenner.SynologyCalendarClient` (`GeneratePackageOnBuild`, pushed by
`BuildAndPushPackage.bat`). It is **not** finished: the official API documentation is poor, creating
events does not work reliably, and the author moved on to CalDAV
([CalDAVNet](https://github.com/SeppPenner/CalDAVNet),
[CalDavSynologySyncer](https://github.com/SeppPenner/CalDavSynologySyncer)). Keep that in mind
before promising that a method works, only the ones listed in the `README.md` were ever tested
against a real disk station.

One solution `src/SynologyCalendarClient.sln` with three projects:

- `src/SynologyCalendarClient/SynologyCalendarClient.csproj`, the library and the only packed
  project.
- `src/SynologyCalendarClient.Test/SynologyCalendarClient.Test.csproj`, MSTest, deserialization
  tests only. They feed captured JSON responses into `JsonConvert` and check the mapped properties,
  no network, no disk station.
- `src/SynologyCalendarClient.IntegrationTest/SynologyCalendarClient.IntegrationTest.csproj`, an
  `Exe` that talks to a real disk station. It contains hardcoded placeholder credentials
  (`https://example.com/calendars`, `username`, `password`), so it does nothing useful unless you
  edit it. Never commit real credentials into it.

Layout inside `src/SynologyCalendarClient`:

- `Client/`: the API surface, one `partial class SynologyCalendarClient` split by API area
  (`.Auth`, `.Info`, `.Calendar`, `.Event`, `.Task`). `SynologyCalendarClient.cs` holds the
  constructor, the `HttpClient` and the `SynoToken` property. New endpoints go into the partial of
  their area, not into a new class.
- `Constants/ApiEndpoints.cs`: the endpoint templates, each with `{0}` for the API version, filled
  by `string.Format`. `Constants/SystemGlobals.cs`: byte formatting helpers.
- `Data/`: the response models, grouped per area, plus `Result.cs` (the generic envelope),
  `ErrorData.cs`, `HeaderKeys.cs` (every query parameter name) and the error code enums.
- `Extensions/`: `EnumExtensions.GetEnumMemberValue` reads the `EnumMember` attribute,
  `ObjectExtensions.IsEmptyOrNull` checks enumerables.
- `QueryParameters.cs`: the query string builder, `DecimalMath.cs`: decimal math helpers.
- `GlobalUsings.cs`: all usings of the project.

Repository root: `README.md` (the only user documentation), `Changelog.md`, `Updating.md` (the
release steps), `License.txt` (MIT), `Icon.png` and `Synology.ico`, the two batch files and `doc/`
with the official API PDF, a Postman collection and a link file. There is no `.github` folder, no
pipeline file and no `Directory.Build.props`.

## Build

```powershell
dotnet build src/SynologyCalendarClient.sln -c Release
```

```powershell
dotnet test src/SynologyCalendarClient.sln
```

- The library multi-targets `net10.0;net11.0`, so building needs the .NET 11 SDK. The two other
  projects are single target `net11.0`, the newest framework the library targets. That is the
  convention of the sibling repositories in `D:\Projekte\Github\CSharpUndVB`, keep the three in sync
  when a framework is added or dropped.
- All build properties live directly in the three `.csproj` files and are duplicated there. There is
  **no** `Directory.Build.props` in this repository.
- `TreatWarningsAsErrors` is enabled in all three projects, so every warning breaks the build, NuGet
  warnings (`NU****`) from restore included. A clean build reports zero warnings, keep it that way.
- `NU1803` (HTTP source usage during restore) is the one warning suppressed via `NoWarn`. Fix
  warnings instead of extending that list. `NuGetAudit` and `NuGetAuditMode=all` are on, so a
  vulnerable transitive package fails the build too.
- `GenerateDocumentationFile` is on for the library, so a malformed XML doc comment (`CS1570`) or a
  missing one on a public member (`CS1591`) is a build error, not a hint.
- Versions come from GitVersion.MsBuild out of the git tags, for example `1.0.2-1` for the first
  commit after tag `1.0.1`. Never edit a version property or an assembly version by hand.
- Building the library also writes the `.nupkg` and the `.snupkg` into
  `src/SynologyCalendarClient/bin/Release`, `GeneratePackageOnBuild` is on. That is what
  `BuildAndPushPackage.bat` uploads, it does not build a package of its own.
- `Delete-BIN-OBJ-Folders.bat` wipes every `bin` and `obj` below `src`. `BuildAndPushPackage.bat`
  does the same before it builds, so a stale `bin` never reaches nuget.org.
- The tests are MSTest, 9 of them in `src/SynologyCalendarClient.Test`, and they need no network and
  no disk station. `dotnet test` runs them in about a second. Never claim a test run happened
  without running it.

## Code conventions

Follow the surrounding code, it is consistent throughout every file:

- File header comment block with `<copyright file="..." company="Hämmer Electronics">` and a
  `<summary>`, then the file-scoped namespace.
- XML doc comments on every type and every member, private members included, no exceptions. Public
  methods additionally document `<param>`, `<returns>` and the `<exception>`s they throw.
- `Nullable`, `ImplicitUsings` and `LangVersion latest` are enabled.
- New `using` directives go into the `GlobalUsings.cs` of the respective project, inside the
  existing `#pragma warning disable IDE0065` block, never at the top of a file. The editorconfig
  requires usings inside the namespace (`csharp_using_directive_placement=inside_namespace:warning`),
  which global usings cannot satisfy, that is what the pragma is for. Do not add other pragmas. The
  comment text in that block is German because Visual Studio generated it, leave it alone.
- Fields, properties, methods and events are always accessed with `this.` qualification
  (`dotnet_style_qualification_for_*` at severity `warning`).
- `src/.editorconfig` also enforces braces everywhere, no multiple blank lines, four spaces, CRLF,
  UTF-8, file scoped namespaces, `System` usings sorted first and `IDE0005` as warning. Analyzer
  warnings are fixed, not silenced.
- Every tracked text file is UTF-8 **without** BOM with CRLF line endings, the only exception is
  `src/SynologyCalendarClient.sln`, which Visual Studio writes with a BOM. Keep both as they are
  when editing with a script.

## Known quirks

Do not silently "clean up" these, they are existing behaviour:

- **Every API method takes the API version as its first parameter.** The endpoint templates put it
  into the URL, and the parameter blocks are guarded by `if (apiVersion >= n)`, so an older version
  simply sends fewer parameters. The minimum differs per area: the calendar methods demand `>= 2`,
  everything else `>= 1`. Do not "simplify" those guards away, they mirror the version table of the
  API documentation in `doc/`.
- **`AddIfNotNull` throws on a duplicate key.** `QueryParameters.AddIfNotNull` checks
  `ContainsKey` **before** it checks the value for null, so adding the same key twice throws an
  `InvalidOperationException` even when the second value is null. Adding the same parameter under
  the wrong key therefore breaks a method completely instead of only sending a wrong value.
- **`bool` parameters travel as `"True"`/`"False"`.** The call sites pass `someBool.ToString()`, and
  `AddIfNotNull` maps exactly those two strings to lowercase `true` and `false`. Anything that
  produces a differently cased string bypasses that mapping.
- **The `SynoToken` is only sent on `SendAsync`.** `Login` stores the token from the response into
  the protected `SynoToken` property, and every method that builds an `HttpRequestMessage` adds it
  as the `X-SYNO-TOKEN` header. `Login`, `Logout` and `GetApiInformation` use `GetAsync` instead and
  therefore send no token, which is intended, they run before or without a session.
- **The session cookie comes from the `CookieContainer`.** The constructor puts a `CookieContainer`
  into the `HttpClientHandler`, so a cookie based login (`LoginFormat.Cookie`) keeps working without
  any explicit cookie handling. That is why the client keeps one `HttpClient` for its whole
  lifetime.
- **Deserialization errors are swallowed.** Every method wraps `JsonConvert.DeserializeObject` in a
  `try`/`catch`, logs through the injected Serilog `ILogger` and returns `null`. A `null` result
  therefore means "the response could not be parsed", while a non-null result with
  `Success == false` means "the disk station reported an error". Callers have to check both, the
  `README.md` sample does.
- **The HTTP status code is never checked.** No method calls `EnsureSuccessStatusCode`, the body is
  read and parsed no matter what came back. A 404 from a wrong base url ends up as a
  deserialization error, not as an HTTP error.
- **`DecimalMath.cs` is a copy.** The same file exists as its own repository and NuGet package
  (`D:\Projekte\Github\CSharpUndVB\DecimalMath`). Here it is vendored, and `SystemGlobals` uses it
  for a single logarithm in `GetHumanReadableBytes`. Do not "fix" its doc comments (`Epsilon` is
  documented as "Represents PI"), they are inherited from the original.
- **`SystemGlobals` is dead weight in this library.** Nothing inside the client calls
  `GetHumanReadableBytes`, it is public API for consumers. Same for large parts of `DecimalMath`.
- **The alias `SynologyClient`.** The namespace `SynologyCalendarClient.Client` and the type
  `SynologyCalendarClient` repeat the assembly name, which makes the fully qualified name unreadable.
  Both the integration test (in its `GlobalUsings.cs`) and the `README.md` sample therefore alias it
  to `SynologyClient`. The alias is not part of the library, consumers declare it themselves.
- **The test data comes from the API guide.** The JSON literals in `DeserializationTests.cs` are the
  example responses of `doc/Synology_Calendar_API_Guide_enu.pdf`. When a test disagrees with the
  model, check the guide before changing either side: the field name `cal_privilege` and the
  `notify_alarm_by_*` flags were decided that way.
- **AppVeyor badge without CI in the repository.** `README.md` links an AppVeyor build that is
  configured outside of this repository. There is no `.github` folder and no pipeline file here.
- **`.gitattributes` sets `* text=auto`**, every rule of the Visual Studio template below it is
  commented out. Binary files that must not be normalized need their own rule.

## Releasing

**Every new version is released completely.** Changelog, README, nuget.org, GitHub Packages and a
GitHub release belong to every version, none of these steps is optional and none of them waits for
a separate request.

`Updating.md` describes the same steps for a human, keep both in sync when the process changes.

1. Make the change.
2. Add an entry at the top of `Changelog.md` in the existing format:
   `* **Version 1.0.2.0 (2026-08-18)** : Short description.`
3. Update `PackageReleaseNotes` in `src/SynologyCalendarClient/SynologyCalendarClient.csproj` to the
   same text, in its own format: `Version 1.0.2.0 (2026-08-18): Short description.` It is the only
   place in the build files that names a version, and it is duplicated from the changelog by hand.
4. Update `README.md` for every version. The `## Available for` list has to match the
   `TargetFrameworks` of the library, the `## Basic usage` sample has to match the current API, the
   list of tested methods under `## Issues` must not claim more than was tested against a real disk
   station, and nothing there may describe a state the package has left behind.
5. Commit that.
6. Tag the commit with the plain version number, no `v` prefix (`1.0.1`, `1.0.0`). The existing tags
   are lightweight tags, create new ones the same way.
7. Push the commits and the tag.
8. Only then build the package, and build it while the tag is the newest commit. GitVersion takes
   the version from the tag plus the distance to it, so a build before the tag or after another
   commit produces a prerelease version like `1.0.3-3+Branch.master.Sha...`, and that version would
   end up on nuget.org. `git describe --tags` has to answer with the plain tag.
9. Publish to nuget.org with `BuildAndPushPackage.bat`, started from the repository root, its first
   line is `cd .\src`. It deletes every `bin` and `obj` below `src`, builds `-c Release` and pushes
   the `.nupkg` and the `.snupkg` to nuget.org with `%NUGET_API_KEY%` and to the NuGet source named
   `github` with `%GITHUB_API_KEY%`. Its push lines carry a leading `@`, keep it, `ECHO` is on there
   and cmd would otherwise print the expanded API keys. It ends with `PAUSE`, so it wants a console.
   Call it with its full path: the Claude Code shell sets `NoDefaultCurrentDirectoryInExePath=1`, so
   cmd does not find a bare `BuildAndPushPackage.bat` in the current directory. Because it always
   rebuilds, it is only correct as long as no commit sits on top of the tag. If one does, push the
   package that was built from the tagged state instead, by its file name and not with the
   wildcard, because `GeneratePackageOnBuild` leaves a `.nupkg` of every intermediate build in
   `bin`:
   `dotnet nuget push HaemmerElectronics.SeppPenner.SynologyCalendarClient.1.0.2.nupkg -s "nuget.org" --skip-duplicate -k %NUGET_API_KEY%`
   in `src/SynologyCalendarClient/bin/Release`. That command takes the matching `.snupkg` with it.
   A push to nuget.org cannot be taken back, the version is burned even after unlisting. nuget.org
   needs a few minutes to validate a new version before it is listed.
10. Publish the same `.nupkg` to GitHub Packages,
    `https://nuget.pkg.github.com/SeppPenner/index.json`. The script only reaches it if a NuGet
    source named `github` is registered on the machine, `dotnet nuget list source` shows that. On
    2026-11-24 only nuget.org was registered here, so the two `github` lines of the script fail and
    the push has to be done by hand in `src/SynologyCalendarClient/bin/Release`:
    `dotnet nuget push HaemmerElectronics.SeppPenner.SynologyCalendarClient.1.0.2.nupkg -s https://nuget.pkg.github.com/SeppPenner/index.json --skip-duplicate --api-key <token>`.
    The API key is a GitHub token with the scope `write:packages`, for example from
    `gh auth token`. If `gh auth status` lists no `write:packages`, the user has to run
    `gh auth refresh -h github.com -s write:packages,read:packages`, it needs a browser login.
    GitHub Packages takes no symbol packages, push the `.nupkg` only.
11. Create the GitHub release:
    `gh release create 1.0.2 -R SeppPenner/SynologyCalendarClient --verify-tag --latest --title "Version 1.0.2.0 (2026-08-18)" --notes-file <file>`.
    The notes are the changelog sentence plus the line
    `NuGet: https://www.nuget.org/packages/HaemmerElectronics.SeppPenner.SynologyCalendarClient/1.0.2`.
    Write credits as `` `@name` `` in backticks, a bare `@name` notifies that person again.

The version in the `Changelog.md` has four parts (`1.0.2.0`), the tag has three (`1.0.2`).

nuget.org and GitHub Packages hold 1.0.1 and 1.0.2, the tag 1.0.0 was never published under the
current package id. On 2026-11-24 no tag had a GitHub release yet.

## Git

- **Never amend a commit.** No `git commit --amend`, not for a typo in the message, not to add a
  forgotten file, not even when the commit is still local. Write a follow-up commit instead. The
  release versions come from tags on exact commits, an amended commit leaves its tag pointing at a
  commit that no longer exists in the branch.

## Writing style

- Commit messages are written **in English only**: short, precise subject line, explanatory body
  when needed.
- Code comments and comments in project files such as `.csproj` are **always English**, regardless
  of the language used in the conversation.
- **No em dashes or en dashes** (`—`, `–`), neither in prose, commit messages, code comments nor
  documentation. Use a regular hyphen, comma, colon, parentheses or a separate sentence.
- German texts (documentation, chat replies) always use real umlauts and ß, never ASCII
  transliterations such as `ae`, `oe`, `ue` or `ss`. Identifiers, file names and configuration keys
  stay unchanged where umlauts are technically undesirable.
