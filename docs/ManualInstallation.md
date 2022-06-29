---
permalink: /ManualInstallation.html
title: Manual Installation
description: the flat-pack Kiea instructions, written in Kerbalese, unusally present
# layout: bare
tags: installation,directions,page,kerbal,ksp,zer0Kerbal,zedK
---

<!-- ManualInstallation.md v1.1.7.0
Field Training Lab (FTL)
created: 01 Oct 2019
updated: 18 Apr 2022 -->

<!-- based upon work by Lisias -->

# Field Training Lab (FTL)

[Home](./index.md)

`BLURB`

## Installation Instructions

### Using CurseForge/OverWolf app or CKAN

You should be all good! (check for latest version on CurseForge)

### If Downloaded from CurseForge/OverWolf manual download

To install, place the `FieldTrainingLab` folder inside your Kerbal Space Program's GameData folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
  * Delete `<KSP_ROOT>/GameData/FieldTrainingLab`
* Extract the package's `FieldTrainingLab/` folder into your KSP's GameData folder as follows:
  * `<PACKAGE>/FieldTrainingLab` --> `<KSP_ROOT>/GameData/`
    * Overwrite any preexisting folder/file(s).
  * you should end up with `<KSP_ROOT>/GameData/FieldTrainingLab`

### If Downloaded from SpaceDock / GitHub / other

To install, place the GameData folder inside your Kerbal Space Program folder:

* **REMOVE ANY OLD VERSIONS OF THE PRODUCT BEFORE INSTALLING**, including any other fork:
  * Delete `<KSP_ROOT>/GameData/FieldTrainingLab`
* Extract the package's `GameData/` folder into your KSP's root folder as follows:
  * `<PACKAGE>/GameData/` --> `<KSP_ROOT>`
    * Overwrite any preexisting file.
  * you should end up with `<KSP_ROOT>/GameData/FieldTrainingLab`

## The following file layout must be present after installation

```markdown
<KSP_ROOT>
  + [GameData]
    + [FieldTrainingLab]
      + [Agencies]
        ...
      + [Compatibility]
        ...
      + [Contracts]
        ...
      + [Flags]
        ...
      + [Localization]
        ...
      + [Parts]
        ...
      + [Plugins]
        ...
      * #.#.#.#.htm
      * changelog.md
      * License.txt
      * readme.htm
      * FieldTrainingLab.version
    ...
    * [Module Manager][mm] or [Module Manager /L][mml]
  * KSP.log
  ...
```

### Dependencies

* *either*
  * [Module Manager][mm]
  * [Module Manager /L][mml]

[mm]: https://forum.kerbalspaceprogram.com/index.php?/topic/50533-*/ "Module Manager"
[mml]: https://github.com/net-lisias-ksp/ModuleManager "Module Manager /L"
