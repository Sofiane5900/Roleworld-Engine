# 🗺️ Map Generation System — Roleworld Engine

This document explains how the map generation works in the Roleworld Engine. It provides a clear, technical overview of the process, suitable for developers and contributors who want to understand or modify the system.

---

## 🔧 Generation Steps

### 1. Generate Falloff Map

**Purpose**: Makes the edges of the world more likely to be ocean.
* Push low elevation values near the borders
* Helps generate island-like or continent-like shapes

### 2. Generate Heightmap with Perlin Noise

**Purpose**: Creates natural-looking terrain elevation.
* A 2D Perlin Noise is used
* Noise is normalized between 0 and 1
* Falloff map is subtracted to ensure borders are lower

### 3. Determine Terrain Type from Elevation

**Purpose**: Assign terrain based on height.

| Elevation Range | Terrain Type |
|----------------|--------------|
| < 0.3          | Water        |
| < 0.4          | Sand         |
| < 0.6          | Grass        |
| < 0.8          | Rock         |
| >= 0.8         | Snow         |

### 4. Generate Voronoi Regions

**Purpose**: Define coherent biome zones.
* Voronoi partitioning divides the world into regions
* Each region can be assigned a different biome category

### 5. Combine Layers

**Purpose**: Merge noise + zones into a consistent visual map.
* Perlin controls local variation
* Voronoi (if used) enforces biome boundaries
* Final data is rendered to the screen using colored tiles

---


## 🎨 Color Assignment

Each terrain type is assigned a color for rendering:

| Terrain | Color  |
|---------|--------|
| Water   | Blue   |
| Sand    | Yellow |
| Grass   | Green  |
| Rock    | Gray   |
| Snow    | White  |

---

## 📌 Notes
* All data is generated procedurally at runtime
* This system allows infinite variation while remaining deterministic (same seed → same map)
* Biome diversity can be increased later by layering temperature, region type, or civilization data



> This document will evolve as Roleworld's world generation system expands.
