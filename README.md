# Neos Without SSAO

A [MelonLoader](https://melonwiki.xyz/)-based mod for [NeosVR](https://neos.com/) to
disable the game's awful Screen-space Ambient Occlusion (SSAO) post-processing effect.

## How to use

1. Follow the [MelonLoader instructions](https://melonwiki.xyz/#/README) to install it for Neos.exe
2. Clone this repository and build it with Visual Studio
3. Go fish out `NeosWithoutSsao.ddl` in the build directory and copy it into the `Mods/` dir by Neos.exe.

If you have trouble with Neos freezing on boot, try the `--quitfix` command line option.

## Screenshots

Neos default:

![Default settings](img/before.jpg)

SSAO disabled:

![ssao disabled](img/after.jpg)
