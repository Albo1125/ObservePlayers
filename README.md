# ObservePlayers
ObservePlayers is a resource for FiveM by Albo1125 that allows administrators to accurately and discreetly observe other players.

## Installation & Usage
1. Download the latest release.
2. Unzip the ObservePlayers folder into your resources folder on your FiveM server.
3. Add the following to your server.cfg file:
```text
ensure ObservePlayers
```

4. Add relevant code to disable blips/overhead names and any other functionality you use that can reveal an observer in `sv_ObservePlayers.lua`.
5. Customise the whitelist in `vars.lua`.
6. Optionally, customise the commands in `sv_ObservePlayers.lua`.

## Commands & Controls
* /observe ID - Observes the player with ID.
* /stopobserve - Stops observing.
* Spacebar/X to enter/exit the vehicle that the observed player is in.

## Improvements & Licencing
Please view the license. Improvements and new feature additions are very welcome, please feel free to create a pull request. As a guideline, please do not release separate versions with minor modifications, but contribute to this repository directly. However, if you really do wish to release modified versions of my work, proper credit is always required and you should always link back to this original source and respect the licence.

## Libraries used (many thanks to their authors)
* [CitizenFX.Core.Client](https://www.nuget.org/packages/CitizenFX.Core.Client)