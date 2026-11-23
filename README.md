# 🚀 3D Solar System & Space Exploration

An interactive 3D Solar System exploration and educational visualization system built with **Unity (URP)**. The project bridges **Computer Graphics & Interactive Programming (CGIP)** with real-time orbital simulation, procedural graphics, and contextual planetary science.

---

## 📖 Overview

The application delivers an interactive 3D simulation of our Solar System, featuring the Sun, all eight planets, natural satellites, and realistic celestial mechanics. Users can freely explore interplanetary space, accelerate or reverse the astronomical timeline, jump to custom historical dates, and inspect detailed scientific data for any celestial body.

---

## ✨ Key Features

* 🪐 **Scientifically Grounded Orbits:** Planetary trajectories calculated using NASA JPL secular variation rates at epoch J2000.0.
* ⏱️ **Astronomical Time Engine:** Continuous Julian Date timeline supporting pause, reverse, forward, and accelerated simulation speeds (up to years per second).
* 📅 **Historical Epoch & Date Picker:** Interactive UI to jump directly to any date and time (`DD/MM/YYYY HH:MM`).
* 🌕 **Tidally Locked Lunar Physics:** Accurate Moon orbit inclination and orientation dynamically locked to face Earth.
* 🪐 **Procedural Saturn Rings:** Mathematically constructed concentric ring mesh with radial UV mapping and custom alpha transparency.
* 🎥 **Dual-Mode Camera Navigation:** Free-roam 6-DOF flight mode and smooth target-tracking orbit mode around any selected planet.
* 📚 **Interactive Celestial Encyclopedia:** ScriptableObject-driven scientific data panel with tabbed browsing (Overview, Physical, Orbital, Atmosphere & Structure).
* 🌌 **High-Fidelity Space Environment:** High-resolution starfield backdrop, realistic scale factors, and PBR planetary surface materials.

---

## 🎨 CGIP & Core Computer Graphics Techniques

| Technique | Description & Implementation |
|---|---|
| **3D Coordinate Transformations** | Translating heliocentric ecliptic coordinates to Unity's left-handed 3D world space ($X \to X$, $Z \to Y$, $Y \to Z$) scaled to 20 Unity units per AU. |
| **Procedural Mesh Generation** | Dynamic procedural construction of Saturn's ring geometry in `SaturnRings.cs`, generating vertex buffers, index arrays, normals, and UV texture coordinates at runtime. |
| **Parametric Curve Rendering** | Real-time evaluation of elliptical orbital paths rendered via `LineRenderer` by sampling true anomalies across full $[0, 2\pi]$ sweeps. |
| **Quaternions & Rotational Physics** | Planetary axial tilts and continuous sidereal rotations calculated using Quaternion math (`AngleAxis` and `Quaternion.Euler`) to prevent gimbal lock. |
| **Camera Mathematics & Interpolation** | Smooth fly-to transitions and spherical orbit tracking using `Vector3.Lerp`, `Quaternion.Slerp`, and `SmoothDamp`. |
| **PBR & Custom Shaders** | Physically Based Rendering with metallic, roughness, and normal maps (e.g., Mars MOLA elevation) alongside custom URP Shader Graph shaders. |
| **Orbital Mechanics & Numerical Solving** | Analytical NASA JPL linear rate equations paired with a 20-iteration Newton-Raphson root-finder for Kepler's transcendental equation ($M = E - e \sin E$). |

---

## 🎮 Controls & Navigation

| Input | Action |
|---|---|
| **Right Mouse Button (Hold & Drag)** | Look around / Rotate camera view |
| **W / A / S / D** | Fly forward, backward, left, right |
| **Q / E** | Fly vertically down / up |
| **Left Shift (Hold)** | Boost flight speed (5x multiplier) |
| **Mouse Scroll Wheel** | Zoom in / Zoom out |
| **Planet Button / Left Click** | Select celestial body to open scientific encyclopedia |
| **Fly To / Follow Button** | Smoothly navigate to and enter orbit around the target body |
| **Time Controls (Play / Pause / Step)** | Adjust simulation speed, reverse time, or enter a custom date |

---

## 📁 Project Structure

```text
SolarSystem_Prototype/
├── Assets/
│   ├── Data/
│   │   ├── CelestialBodyInfo/   # ScriptableObjects for encyclopedia data
│   │   └── Planets/             # ScriptableObjects for JPL orbital parameters
│   ├── Materials/               # Planet surfaces, ring shaders, and orbit materials
│   ├── Prefabs/                 # Celestial body and UI prefabs
│   ├── Scenes/
│   │   └── MainSolarSystem.unity# Primary interactive exploration scene
│   ├── Scripts/
│   │   ├── Data/                # Data structures (PlanetData, CelestialBodyInfo)
│   │   ├── Interaction/         # Planet selection, tab switching, and button events
│   │   ├── Player/              # FreeFlyCamera and CameraNavigationController
│   │   ├── SolarSystem/         # KeplerSolver, JulianDate, TimeController, Moons, Rings
│   │   └── UI/                  # DateInputController and timeline HUD controls
│   └── Textures/                # High-res albedo, normal, and starfield textures
├── Packages/                    # URP, New Input System, and TextMesh Pro dependencies
├── ProjectSettings/             # Project configuration and build settings
├── .gitignore                   # Unity and large-binary exclusion rules
└── README.md                    # Project documentation
```

---

## 🛠️ Technology Stack

* **Engine:** Unity 6 (`6000.5.6f1`)
* **Render Pipeline:** Universal Render Pipeline (URP 17.5)
* **Language:** C# (.NET Standard)
* **Input System:** Unity New Input System (`com.unity.inputsystem`)
* **UI Framework:** Unity UI (UGUI) & TextMesh Pro
* **Data Architecture:** Unity `ScriptableObject` Architecture
* **Version Control:** Git & GitHub

---

## 🚀 Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/zelssorathiya2615/CGIP.git
   cd CGIP
   ```
2. **Open in Unity Hub:**
   - Click **Add project from disk**.
   - Select the `SolarSystem_Prototype` directory.
   - Ensure **Unity 6 (6000.5.6f1)** or compatible version is selected.
3. **Run the Project:**
   - In the Project window, open `Assets/Scenes/MainSolarSystem.unity`.
   - Press **Play** in the Unity Editor.

---

## 🔭 Future Scope

* 🚀 **3D Spaceflight Simulator (3D SFS):**
  - Modular rocket staging and spacecraft assembly system.
  - Thrust-to-weight (TWR) physics, dynamic mass calculations, and fuel consumption modeling.
  - Realistic gimbaled thrusters and RCS (Reaction Control System) attitude thrusters.
* 🌌 **Orbital Trajectory & Maneuver Planning:**
  - Visual delta-V ($\Delta v$) maneuver node editor.
  - Hohmann transfer calculations between planetary orbits.
  - Gravity assist / orbital slingshot trajectory planning.
* 🌠 **Advanced Astrophysics:**
  - Full $N$-body gravitational perturbation physics (Runge-Kutta 4th order integrator).
  - Atmospheric drag modeling and re-entry thermal plasma effects.
  - Lagrangian points ($L_1$–$L_5$) simulation for orbital stations.
* 🛰️ **Historic & Deep-Space Missions:**
  - Pre-computed trajectories for historic missions (Apollo 11, Voyager 1 & 2, James Webb Space Telescope).
  - Asteroid belt (Ceres, Vesta) and Kuiper belt objects (Pluto, Charon).

---

## 📄 License & Attribution

Developed for academic purposes under the **Computer Graphics & Interactive Programming (CGIP)** curriculum.
* Planetary orbital elements and ephemerides courtesy of **NASA / JPL Solar System Dynamics**.
* Planetary surface maps and textures courtesy of **NASA / USGS / JPL**.
