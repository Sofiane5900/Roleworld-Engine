<div align="center">

<img src="https://i.imgur.com/266JDTr.png" alt="RoleWorld Banner" width="600"/>

**A handcrafted 2D engine in C# (.NET 8+) for procedural world generation.**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Silk.NET](https://img.shields.io/badge/Silk.NET-OpenGL-blue)](https://github.com/dotnet/Silk.NET)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

</div>

---

## Overview

RoleWorld Engine generates procedural worlds (maps, biomes, terrain) from a seed, with a minimalist 2D pixel-art rendering pipeline built entirely from scratch.

---

## Example Output

<div align="center">
<img src="https://i.imgur.com/crVR5GF.png" alt="Procedural map example" width="500"/>

<sub>A procedurally generated map rendered by the engine.</sub>
</div>

---

## Features

- Procedural strategic map generation (islands, biomes, terrain) from a seed
- Immediate-mode 2D rendering via Silk.NET (OpenGL, GLFW)

---

## Tech Stack

| Component | Detail |
|---|---|
| Language | C# on .NET 8+ |
| Low-level API | Silk.NET (OpenGL, GLFW, Input) |
| Architecture | Fully handcrafted, no external engine dependency |
| Rendering | Immediate-mode 2D, minimalist pixel-art style |

---

## Getting Started

```bash
git clone https://github.com/Sofiane5900/Roleworld-Engine.git
cd Roleworld-Engine/Roleworld
dotnet run
```
