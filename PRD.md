# 🧩 Product Requirements Document — RoleWorld Engine

## 📘 Project Summary

**RoleWorld Engine** is a handcrafted 2D engine built in C# using Silk.NET. It is designed to create living, systemic role-playing games (RPGs) where kingdoms, characters, and events interact in a persistent, GUI-only world. Inspired by simulations like *Mount & Blade*, *Crusader Kings*, and *Dwarf Fortress*, the engine serves as a sandbox for emergent storytelling and strategic freedom.

---

## 🎯 Goal

To build a modular, libre engine enabling developers to craft:

* **Living simulated worlds**
* **Player freedom** with no predefined class or script
* **Graphical-only interaction** (no 3D rendering)
* **Persistent systems** with deep entity interconnections

---

## 🧠 Design Principles

* 🎲 **Systemic Simulation**: Every entity acts, evolves, and reacts independently.
* 🧭 **Interactive Strategic Map**: The world is navigated through a zoomable 2D map.
* 🎭 **Role Freedom**: No mandatory objectives or win states — pure sandbox.
* 💬 **Complete GUI**: All interaction is through windows, dialogs, panels, HUD.
* 🔁 **Persistence & Replayability**: All states are saved and reloadable.
* 📚 **Emergent Narrative**: Stories emerge from systems, not scripts.

---

## 🛠️ Tech Stack

* **Language**: C# (.NET 8+)
* **Library**: Silk.NET (OpenGL, GLFW, Input)
* **Rendering**: Custom 2D GUI (not Unity, not Godot)
* **Architecture**: Fully custom, built from the ground up

---

## 👤 Target Audience

* Solo developers (like Sofiane) who want to create deep simulations
* Player-programmers who love building systems and watching them evolve
* Not aimed at the general public — no drag-and-drop, no prefab content

---

## ❌ Not Intended For

* ❌ 3D games
* ❌ Game creators looking for premade scenes or assets
* ❌ Action/arcade/platformer genres
* ❌ Visual editors or WYSIWYG tools

---

## 🧩 Core Modules (Planned)

* `SimulationEngine`: updates world state over time
* `EntitySystem`: characters, factions, locations, traits
* `EventSystem`: dynamic, emergent events
* `MapRenderer`: interactive map + fog of war
* `DecisionSystem`: AI and player choices
* `GUIFramework`: windows, panels, dialogues
* `SaveSystem`: full world persistence (JSON / binary)

---

## 🧪 MVP Prototype Roadmap

* [ ] Render zoomable world map (2D)
* [ ] Create 2 factions with basic goals
* [ ] Simulate population, movement, events
* [ ] Create a basic GUI window with entity info
* [ ] Enable save/load of simulation state

---

## 🔮 Inspirations

* *Mount & Blade*: Map-based freedom, evolving world
* *Crusader Kings*: Dynastic simulation, social depth
* *Dungeons & Dragons*: Open-ended roleplay

---

## 📜 Conclusion

RoleWorld Engine is a long-term, personal creation sandbox for systemic, strategic, GUI-driven RPGs. It doesn't impose any predefined rules — it offers tools to simulate, visualize, and interact with a fully living world.
