---
permalink: /HowItWorks.html
title: HowItWorks
description: the flat-pack Kiea instructions, written in Kerbalese, unusally present
# layout: bare
tags: installation,directions,page,kerbal,ksp,zer0Kerbal,zedK
---

<!-- HowItWorks.md v1.1.0.0
MOD-NAME (ABBV)
created: 01 Oct 2019
updated: 02 Mar 2022 -->

# MOD-NAME (ABBV)

[Home](./index.html)

## How it works

* Vessel must be landed (sounds familiar :D)
* Each command capsule (and probe core of course) now has Logistics Module on board (switched off by default)
* As soon as module is enabled ship becomes a part of logistics network where each vessel share same resource pool
* Resources spread evenly across all resource capacitors over network with priority to less capacitive ones. That means that resource will be drained from bigger ones first, while preserving resources in smaller ones.

![How It Works](https://i.imgur.com/FygUhoD.png)

---

* Resources spread in physical range from active vessel (~2400 meters)
* Ships with no command module (debris) always become part of network
* **WARNING!!!** This does not work in background, only on active vessel and nearby ones.
* Vessel that is not the part of network may still request resources from pool
* Compatible with Community Resource Pack, so every resource is shareable as soon it is transferable

### Dependencies

* [Module Manager][mm]
* [ToolbarController][tbc]

[mm]:  https://github.com/net-lisias-ksp/ModuleManager "Module Manager /L"
[tbc]: https://forum.kerbalspaceprogram.com/index.php?/topic/169509-* "ToolbarController"

<!-- this file CC BY-NC-ND 4.0 by zer0Kerbal -->
