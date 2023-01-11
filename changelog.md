# Changelog  
  
| modName    | Field Training Facility (FTF)                                      |
| ---------- | ------------------------------------------------------------------ |
| license    | GPL-3.0                                                            |
| author     | Efour and zer0Kerbal                                               |
| forum      | (https://forum.kerbalspaceprogram.com/index.php?/topic/188841-*/)  |
| github     | (https://github.com/zer0Kerbal/zer0Kerbal/FieldTrainingFacility)   |
| curseforge | (https://www.curseforge.com/kerbal/ksp-mods/FieldTrainingFacility) |
| spacedock  | (https://spacedock.info/mod/978)                                   |
| ckan       | FieldTrainingFacility                                              |

## Version 1.2.1.1-release <LatiMacciato> edition

* 03 Jul 2022
* For Kerbal Space Program [1.12.x]

### NOTICE

The 1.2.x.x-releases are the final versions of this addon.
Both *Field Training Lab* and *Field Training Facility*'s plugins (.dll) will be merged into one plugin.
Both part modules will remain in the addon and this improvement *should not* affect existing save games.

### Compatibility

* [FieldTrainingFacility.cfg] v1.1.2.0
  * wrong :NEEDS
    * is
      * @part[*]:HAS[@module[ModuleScienceLab]]:NEEDS[FieldTrainingLab]:FOR[FieldTrainingFacility]
      * should be
        * @part[*]:HAS[@module[ModuleScienceLab]]:NEEDS[FieldTrainingFacility]:FOR[FieldTrainingFacility]
  * Thank you to forum user [LatiMacciato](https://forum.kerbalspaceprogram.com/index.php?/profile/165597-latimacciato/) for pointing out the finger dyslexia issue.
  * closes #48 - [Bug 🐞]: [FieldTrainingFacility.cfg] wrong :NEEDS

### Status

* Issues
  * closes #52 - 1.2.1.1 Update Social Media
  * closes #51 - 1.2.1.1 Update Documentation
  * closes #50 - 1.2.1.1 Verify Legal Mumbo Jumbo
  * closes #49 - Field Training Facility (FTF) 1.2.1.1-release `<LatiMacciato>`
  * closes #47 - Update License to GPLv3

---

## Version 1.2.1.0-release - `<Clean Blackboards>` edition

* 28 Jun 2022
* For Kerbal Space Program [1.12.x]

### License

* Update to GPLv3
  * was Expat/MIT
* closes #47 - Update License to GPLv3

### docs/

* Add
  * [Attribution.md] v1.0.6.0
  * [ManualInstallation.md] v1.1.7.0
  * [404-petunia.md]
  * [LegalMumboJumbo.md] v1.0.5.0
  * [Localizations.md] v1.1.3.1
  * [Notices.md] v1.0.0.0
  * [Why-not.md]
  * [_config.yml]
* closes #44 - add docs/

### Convert Changelog

* Convert from .cfg to md
* Add missing information for earlier releases
* closes #42 - Convert Changelog

### Code

* Recompile for KSP 1.12.3
* Using .NET 4.6.1
  * remove
    * [InstallChecker.cs]
    * [AssemblyVersion.tt]
    * [Log.cs]
  * Add readonly
    * string[] trainingArr =
    * string[] crewListArr =
    * float[] levelUpExpTable = { 2, 6, 8, 16, 32, 0 };
    * string[] levelNumber = { "null", "1st", "2nd", "3rd", "4th", "5th" };
    * ProtoCrewMember[] crewArr = new ProtoCrewMember[8];
  * Change
    * Version.Text to Version.SText
  * Update
    * function name capitalization
  * update [Version.tt]
* closes #1 - modules info panels for parts in the VAB.

### Compatibility 1.2.1.0

* Rename
  * Patches to Compatibility
* Update
  * licenses
  * [Kerbalsim.cfg] to [Kerbalism.cfg] v1.1.1.0
    * fixes #43 - [Bug 🐞]: Kerbalism.cfg
  * [FieldTrainingFacility.cfg] v1.1.1.0
    * update :NEEDS

### Add

* Agent
* Flag
  * 512x320
  * 64x40 truecolor_scaled

### Localization

* Add
  * [readme.md] v2.1.2.0
  * [quickstart.md] v1.0.1.1
* updates #22 - Localization - Master
* closes #23 - English <us-en.cfg>
* closes #31 - Simplified Chinese (简体中文) <zh-cn.cfg>
* updates #40 - Code Localization

### Status

* Issues
  * closes #16 - Update Field Training Facility (FTF)
  * closes #18 - Field Training Facility (FTF) 1.2.1.0-release - `<Clean Blackboards>` edition
  * closes #19 - 1.2.1.0 Verify Legal Mumbo Jumbo
  * closes #20 - 1.2.1.0 Update Documentation
  * closes #21 - 1.2.1.0 Update Social Media
  * updates #6 - Suggestion: Change Mod Name to make it more distinct as to what it does.
* Closes Duplicate Issues
  * closes #10 - Localization
  * closes #12 - Localization
  * closes #13 - Localization - zh-cn.cfg - Simplified Chinese
  * closes #14 - Localization - pt-br.cfg Brazil
  * closes #15 - Localization - en-us.cfg (English)
* PR's
  * #7 - 1.2.0.0 - contributed by zer0Kerbal
  * #9 - Kerbalism Compatibility Resolves #8 - contributed by girotobial
  * #17 - Simplified chinese localization - contributed by zer0Kerbal

---

## Version 1.2.0.0 - `<New Carpets! Automation Motivation Modernization>`

* 05 Apr 2020
* KSP 1.9.1
* .NET 4.8

### Code

* update
  * Editor GetInfo() to be more informative
  * include assembly version in PAW
* Add
  * game settings page
  * ***disabled for now***
    * game settings page
    * global setting to enable/disable PAW color
    * option to globally enable/disable
    * option: use science and ratio
    * option: use reputation and ratio
    * option: use funds and ratio

---

## Version = 1.1.0.0 - `<Automation Motivation Modernization>`

* KSP 1.8.1 with .NET 4.8

* isn't that enough? :D
* started adding in JoyntMail :D

---

## Version = 1.0.3.5 - `<Automation Motivation Modernization.`

* KSP 1.7.3 with .NET 3.5

### Code and Code Related  

* updated / modernized .csproj
* this preps mod for much easier releases
  * added automation
  * [Version.tt]
  * [AssemblyVersion.tt]
* moved
  * into Properties/
  * [AssemblyVersion.tt]
* updated
  * to v2 of InstallChecker.cs
* moved Textures/
  * -> Plugins/Textures/

### Deployment and Backend

* Update
  * [Changelog.cfg]
    * to include new Kerbal Changelog features
    * [_deploy]
    * [_buildRelease]
  * [.gitattributes]
  * [].gitignore]
  * [*Readme.md]
    * automated Readme.md -> Readme.htm
    * Readme.htm now included in release
    * Releases.layout.md
* [CONTRIBUTING.md] now included in repository
* [FieldTrainingFacility.version] to be avc compliant
* Added
  * avc github checker and badge
* Added
  * json's

---

## Version 1.0.3.4

### Adoption by zer0Kerbal

### Code

* Added
  * PAW grouping (really needed for these mods)
  * a blurb in the editor getInfo{}
  * [InstallChecker.cs]

### Compatibility

* Updated
  * [FieldTrainingFacility.cfg]
    * now patches all parts with moduleScienceLab
    * changed the [TrainingFacility] to be [FieldTrainingFacility]
      * patches reflect this
* Removed
  * other patch

---

## Version  1.0.3.3

=-- ORIGINAL (outdated) --=

* for Kerbal Space Program 1.6.1
* Released on 2018-12-21

* EFour's last release
* Recompiled 1.6.0

---

## Version 1.0.3.2

* for Kerbal Space Program 1.5.1
* Released on 2018-10-30

* Recompiled for 1.5.1

---

## Version 1.0.3.1

* for Kerbal Space Program 1.3.1
* Released on 2017-11-27

* Recompiled KSP 1.3.1

---

## Version 1.0.3.0

* for Kerbal Space Program 1.2.2
* Released on 2016-11-03

* Recompiled to 1.2.1

---

## Version 1.0.2.1

* for Kerbal Space Program 1.2
* Released on 2016-10-22

* KPBS support

---

## Version 1.0.2.0

* for Kerbal Space Program 1.2
* Released on 2016-10-16

* Calculating Dead and respawned kerbalnaut

---

## Version 1.0.1.2

* for Kerbal Space Program 1.2
* Released on 2016-10-12

* fixed Message bug

---

## Version 1.0.1.1

* for Kerbal Space Program 1.2
* Released on 2016-10-12

* removing unused log
* changed minor EC related bug

---

## Version 1.0.1.0

* for Kerbal Space Program 1.2
* Released on 2016-10-12

* Co-Work with Field Training Lab Mod
* Compat patch for 1.2 release
* Tweaked EC consuming method.

---

## Version 1.0.0.1

* for Kerbal Space Program 1.2
* Released on 2016-10-12

* No changelog provided

---

## Version 1.0.0.0

* for Kerbal Space Program 1.1.3
* Released on 2016-10-11

* No changelog provided

---
