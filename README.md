# 🚀 3D Space Exploration

An interactive 3D space exploration and educational visualization system built with Unity. The project combines real-time rendering with scientifically grounded orbital motion and contextual educational content.

## 📖 Overview

The application provides an interactive 3D representation of the Solar System, including the Sun, eight planets, selected moons, and additional deep-space objects.

Users can freely navigate through the environment, observe celestial bodies, explore their orbital motion, and access contextual information about objects they encounter.

The project focuses on combining **computer graphics, orbital simulation, real-time rendering, and interactive visualization** into a single exploration experience.

## ✨ Features

* 🌌 Interactive 3D Solar System
* 🪐 Sun, planets, selected moons, and deep-space objects
* 🚀 Free-roam 3D camera navigation
* 🔭 Simplified two-body Keplerian orbital motion
* 🌍 Planetary materials and textures
* ✨ Real-time starfield and skybox
* 💡 PBR-based rendering
* 🌅 Fresnel-based atmospheric rim lighting
* 🎥 Cinemachine camera control
* 📚 Contextual educational encyclopedia
* 📍 Proximity-based object interaction
* ⚡ Floating-origin system for large-scale environments
* 🔍 Level of Detail (LOD) for performance
* 📦 Data-driven celestial-body and encyclopedia information

## 🛠️ Technology Stack

| Technology      | Purpose                           |
| --------------- | --------------------------------- |
| Unity           | Game engine and rendering         |
| C#              | Application and simulation logic  |
| Shader Graph    | Custom shaders and visual effects |
| Cinemachine     | Camera control                    |
| Unity UI / UGUI | User interface                    |
| JSON            | Celestial and educational data    |
| Git / GitHub    | Version control                   |

## 🔬 Core Techniques

* **Keplerian Orbital Mechanics** — calculates celestial-body positions using orbital elements and time.
* **Floating Origin** — reduces floating-point precision problems across astronomical distances.
* **LOD** — adjusts object complexity based on camera distance.
* **Physically Based Rendering** — uses albedo, roughness, metallic, and lighting properties.
* **Fresnel Shading** — creates atmospheric rim-lighting effects.
* **Particle Systems** — used for starfields and environmental effects.
* **Proximity Detection** — triggers contextual information when approaching objects.
* **Skybox / Environment Mapping** — provides the space environment.

## 📁 Project Structure

```text
CGIP/
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   ├── Materials/
│   ├── Prefabs/
│   ├── Textures/
│   └── ...
├── Packages/
├── ProjectSettings/
├── .gitignore
├── ignore.conf
└── README.md
```

### Main Directories

* `Assets/` — scenes, scripts, models, materials, textures, prefabs, shaders, and project content.
* `Packages/` — Unity package dependencies.
* `ProjectSettings/` — Unity project configuration.

## 🚀 Getting Started

### Requirements

* Unity Hub
* Unity Editor version specified in `ProjectSettings/ProjectVersion.txt`

### Clone

```bash
git clone https://github.com/zelssorathiya2615/CGIP.git
cd CGIP
```

### Open in Unity

1. Open Unity Hub.
2. Select **Add / Add project from disk**.
3. Select the cloned `CGIP` folder.
4. Open the project using the Unity version specified in `ProjectSettings/ProjectVersion.txt`.
5. Open the required scene from `Assets`.

Unity will automatically regenerate folders such as `Library`, `Temp`, and `UserSettings`.

## 📌 Future Scope

* Rocket construction and customization
* Trajectory planning and simulation
* Expanded celestial-body database
* Additional deep-space environments
* More advanced orbital mechanics
* Expanded educational content

## 📄 License

This project is developed for academic purpose

# CGIP

A Unity-based computer graphics and interactive visualization project.

## Overview

This repository contains the source files, assets, scenes, scripts, and project configuration for the Unity application developed as part of the project work.

The project focuses on interactive 3D graphics, visualization, scene development, and computer graphics concepts using the Unity Engine.

## Technology Stack

- **Game Engine:** Unity
- **Programming Language:** C#
- **Development Environment:** Unity Hub
- **Version Control:** Git and GitHub
- **Unity Version Control:** Unity Version Control / Plastic SCM

## Project Structure

```text
CGIP/
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   ├── Materials/
│   ├── Prefabs/
│   ├── Textures/
│   └── ...
├── Packages/
├── ProjectSettings/
├── .gitignore
├── README.md
└── ignore.conf
```
