# Jot

*Command line journaling*

> Jot is in **very early** alpha. Please use with caution.

# Prerequisites
* [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
* Make sure [NuGet Gallery](https://nuget.org) is registered as a source in your NuGet configuration (it is by default).

```shell
$ dotnet nuget list source
Registered Sources:
  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
```

# Installation
1. Install with `dotnet tool install`.
```
$ dotnet tool install --global Andtech.Jot
```

# Usage
* Initialize a Jot repository.
```
$ jot init
```
This will create a Jot directory (`.jot`) in the current directory.

* Create a new journal page.
```
$ jot new [<date>]
```
This will create a new journal page file in the repository. The path to the file is based on the date. (Example: `2021/08-aug/01-sun.md`)

* Edit a journal page.
```
$ jot open [<date>]
```
This will launch your default text editor.

## Configuration
* Use `jot config` to configure Jot.
```
$ jot config --global editor micro
```

* Global settings are located at `$HOME/.config/jot/settings.json`.
* Local settings are located in your Jot repository at `.jot/settings.json`.

# Miscellanous
## Use Cases
* Journal: keep track of your thoughts and feelings throughout the days
* School: create a Jot repository for each class
* Meetings: organize all your minutes with Jot
* Dream journal: Jot helps you quickly jot down your dreams

## Extras
* [Learn Markdown](https://www.markdowntutorial.com/)
* [micro](https://micro-editor.github.io/)
* [atom](https://atom.io/)
* [Notepads](https://www.notepadsapp.com/)
* [tree](https://linux.die.net/man/1/tree)
