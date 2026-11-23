# 🌌 3D Space Exploration & Solar System Simulation (CGIP)

An interactive, scientifically grounded 3D space exploration and educational visualization system built with **Unity (URP)** for the **Computer Graphics & Image Processing (CGIP)** curriculum. The project combines real-time physically based rendering with **NASA JPL analytical Keplerian orbital mechanics**, continuous astronomical **Julian Date (JD)** simulation, and interactive scientific data visualization.

---

## 📖 Overview

The application provides an interactive 3D representation of the Solar System, including the Sun, eight major planets, Earth's Moon, planetary rings, and deep-space starfields. 

Rather than relying on simplified circular approximations or hardcoded animations, planetary trajectories are computed in real time from **NASA Jet Propulsion Laboratory (JPL) orbital elements**, accounting for orbital eccentricity, inclination, axial tilt, and secular variations over centuries. Users can freely explore the Solar System, inspect multi-tab scientific encyclopedias, accelerate time across centuries, or jump directly to any historical or future epoch.

---

## ✨ Features

- 🪐 **Scientifically Accurate Planetary Orbits:** Positions computed dynamically using NASA JPL secular variation rates at epoch $J2000.0$.
- ⏱️ **Continuous Julian Date Engine:** Real-time astronomical clock supporting live time, pause, forward, reverse, and variable acceleration rates (from seconds/sec to years/sec).
- 📅 **Interactive Date & Epoch Picker:** Jump directly to any historical or future date and time (`DD/MM/YYYY HH:MM`) via the interactive UI panel.
- 📐 **Numerical Kepler Equation Solver:** Solves the transcendental equation ($M = E - e \sin E$) down to $10^{-10}$ tolerance using the Newton-Raphson iteration algorithm.
- 🌕 **Tidally Locked Lunar Physics:** Earth-Moon orbital inclination ($5.145^\circ$) with real-time rotational alignment so the Moon permanently faces Earth.
- 🪐 **Procedural Saturn Rings:** Mathematically generated ring mesh with radial UV mapping, custom textures, and alpha transparency blending.
- 🧭 **Dual-Mode Camera Kinematics:** Smooth 6-DOF free-flight exploration plus fly-to transitions and orbital tracking around any selected celestial body.
- 📚 **Scientific Encyclopedia:** Multi-tab contextual panel (Overview, Physical Properties, Orbital Properties, Atmosphere, Structure, and Satellites) driven by data-rich ScriptableObjects.
- 🎨 **Modern PBR Graphics:** Built on Unity's Universal Render Pipeline (URP 17.5) with physically based materials, high-resolution surface textures, and real-time starfields.

---

## 🔬 Core Techniques & CGIP Concepts

This project demonstrates core **Computer Graphics and Image Processing** fundamentals:

### 1. Coordinate Space Transformations & Pipeline Math
- **Orbital Plane to Heliocentric Ecliptic Space:** Converts 2D orbital polar coordinates $(r, \nu)$ into 3D Cartesian space via rotation matrices for inclination ($I$), argument of perihelion ($\omega$), and ascending node ($\Omega$).
- **Astronomical to World Space Mapping:** Scales astronomical units (AU) to Unity world units ($1\text{ AU} = 20.0\text{ units}$) and transforms coordinate axes from astronomical right-handed ($Z\text{-Up}$) to Unity's left-handed convention ($Y\text{-Up}$).
- **Local vs. World Hierarchy:** Hierarchical scene graph transformations for parent-child bodies (e.g., Sun $\to$ Earth $\to$ Moon).

### 2. Numerical Methods & Scientific Simulation
- **Root-Finding Iteration:** Solves Kepler's non-linear transcendental equation using the **Newton-Raphson method**, achieving sub-millimeter precision within 3–5 iterations.
- **Continuous Time Integration:** Continuous astronomical calendar discretization via **Jean Meeus's Julian Date formula**, avoiding leap year and Gregorian calendar discontinuities.

### 3. Procedural Geometry Generation
- **Dynamic Ring Mesh Synthesis (`SaturnRings.cs`):** Procedurally constructs vertex buffers, index buffers, normals, and UV coordinates for concentric ring discs at runtime without external 3D model files.
- **Parametric Curve Rendering (`OrbitVisualizer.cs`):** Samples true anomaly $\nu \in [0, 2\pi)$ across $N$ segments to generate dynamic, closed elliptical orbit paths via GPU line renderers.

### 4. Camera Kinematics & Orientation Mathematics
- **Quaternion Interpolation (`Slerp`):** Smooth, gimbal-lock-free rotational transitions during camera navigation.
- **Critically Damped Motion (`SmoothDamp`):** Velocity-based smoothing for following celestial targets during high simulation speeds.
- **Tidal Locking Look-At Rotation:** Dynamically evaluates relative position vectors and orbital plane normal vectors to compute real-time face-locking orientation:
  $$\vec{R}_{\text{target}} = \text{LookRotation}(\vec{p}_{\text{Earth}} - \vec{p}_{\text{Moon}}, \hat{n}_{\text{orbit\_plane}})$$

### 5. Physically Based Rendering (PBR) & Shading
- **PBR Material Workflow:** Albedo, surface normals (e.g., Mars MOLA topographic relief), roughness, and metallic properties under URP.
- **Custom Shaders & Shader Graph:** Custom solar corona shader graph for stellar emission and radial alpha-blended transparency for rings and atmospheres.

---

## 📐 Mathematical & Astronomical Simulation Engine

```text
Simulation Clock (DateTime)
         │
         ▼  [JulianDate.cs]
Julian Date (JD)  ──►  Julian Centuries since J2000.0:  T = (JD - 2451545.0) / 36525.0
         │
         ▼  [KeplerSolver.cs + PlanetData.cs]
Instantaneous Orbital Elements:  a(T), e(T), I(T), L(T), ϖ(T), Ω(T)
         │
         ▼
Kepler's Equation:  M = E - e · sin(E)  ──►  Newton-Raphson Solver  ──►  Eccentric Anomaly (E)
         │
         ▼
True Anomaly (ν) & Heliocentric Distance:  r = a(1 - e · cos(E))
         │
         ▼
Orbital to Ecliptic Cartesian Coordinates:  [x, y, z]
         │
         ▼
Scale & Coordinate Swizzle to Unity Space:  Vector3(x, z, y) * 20.0
```

### Key Formulas:

1. **Julian Date (Meeus Algorithm):**
   $$\text{JD} = \lfloor 365.25(Y + 4716) \rfloor + \lfloor 30.6001(M + 1) \rfloor + d + B - 1524.5$$

2. **JPL Secular Variation Rate:**
   $$p(T) = p_0 + \dot{p} \cdot T \quad \text{where} \quad T = \frac{\text{JD} - 2451545.0}{36525.0}$$

3. **Kepler Equation Solver (Newton-Raphson):**
   $$E_{k+1} = E_k - \frac{E_k - e \sin(E_k) - M}{1 - e \cos(E_k)}$$

4. **True Anomaly ($\nu$):**
   $$\nu = 2 \arctan2\left(\sqrt{1 + e}\sin\frac{E}{2},\; \sqrt{1 - e}\cos\frac{E}{2}\right)$$

---

## 🎮 Navigation & Controls

| Input | Action |
|---|---|
| **Right Mouse Button (Hold & Drag)** | Look around / Rotate camera view |
| **W / A / S / D** | Fly forward, left, backward, right |
| **Q / E** | Fly downward / upward |
| **Left Shift (Hold)** | Boost flight speed ($5\times$) |
| **Mouse Scroll Wheel** | Zoom in / Zoom out |
| **Left Click on Planet / UI Button** | Select planet and open scientific encyclopedia |
| **Fly To / Follow Button** | Smoothly transition to and orbit around selected celestial body |
| **Timeline Controls** | Pause, reverse, play forward, accelerate, or jump to custom date/time |

---

## 📁 Project Structure

```text
SolarSystem_Prototype/
├── Assets/
│   ├── Data/
│   │   ├── CelestialBodyInfo/       # ScriptableObject encyclopedia data (Mass, Gravity, etc.)
│   │   └── Planets/                 # ScriptableObject JPL Keplerian orbital elements
│   ├── Materials/
│   │   ├── Orbits/                  # Custom materials for orbital trajectory lines
│   │   └── *.mat                    # Planetary PBR surface and atmospheric materials
│   ├── Scenes/
│   │   └── MainSolarSystem.unity    # Primary simulation scene with UI, lights, and bodies
│   ├── Scripts/
│   │   ├── Data/
│   │   │   ├── CelestialBodyInfo.cs # Scientific info data structure
│   │   │   └── PlanetData.cs        # JPL orbital parameters data structure
│   │   ├── Interaction/
│   │   │   ├── PlanetButton.cs      # UI button interaction and binding
│   │   │   └── PlanetSelectionManager.cs # Multi-tab scientific encyclopedia manager
│   │   ├── Player/
│   │   │   ├── CameraNavigationController.cs # Smooth fly-to and target-following logic
│   │   │   └── FreeFlyCamera.cs     # 6-DOF free flight and orbital zoom camera
│   │   ├── SolarSystem/
│   │   │   ├── CelestialBody.cs     # Axial tilt and sidereal rotation controller
│   │   │   ├── JulianDate.cs        # Calendar to Julian Date astronomical converter
│   │   │   ├── KeplerSolver.cs      # Newton-Raphson solver and coordinate transformer
│   │   │   ├── MoonOrbitController.cs # Inclined lunar orbit and tidal locking
│   │   │   ├── OrbitController.cs   # Planet orbital positioning per simulation frame
│   │   │   ├── OrbitVisualizer.cs   # Procedural trajectory line generator
│   │   │   ├── SaturnRings.cs       # Procedural ring disc mesh generator
│   │   │   ├── TimeController.cs    # Simulation clock and speed controller
│   │   │   └── TimeControls.cs      # Play/Pause/Slider UI event handler
│   │   └── UI/
│   │       └── DateInputController.cs # Custom timeline epoch input controller
│   ├── Settings/                    # Universal Render Pipeline (URP) configuration
│   └── Textures/                    # Surface, ring, and starfield textures
├── Packages/                        # Unity URP, New Input System, and TMP dependencies
├── ProjectSettings/                 # Unity engine build and project settings
├── .gitignore                       # Git exclusion rules for Unity cache and large assets
└── README.md                        # Project documentation
```

---

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| **Unity (URP 17.5)** | Real-time rendering, scene hierarchy, and lighting pipeline |
| **C# (.NET Standard)** | Scientific calculations, orbital physics, and UI controllers |
| **Unity New Input System** | Keyboard and mouse camera navigation bindings |
| **TextMesh Pro & UGUI** | Scalable astronomical data interfaces and timeline HUD |
| **ScriptableObjects** | Data-driven architecture for Keplerian and physical properties |
| **Shader Graph** | Custom solar emissive shader and procedural materials |
| **Git & GitHub** | Version control with tailored Unity ignore rules |

---

## 🚀 Getting Started

### Prerequisites
- **Unity Hub**
- **Unity Editor** (Version specified in `ProjectSettings/ProjectVersion.txt`, e.g. Unity 6 / 2023.3+)

### Installation
1. **Clone the repository:**
   ```bash
   git clone https://github.com/zelssorathiya2615/CGIP.git
   cd CGIP
   ```
2. **Open in Unity:**
   - Launch **Unity Hub**.
   - Click **Add** $\to$ **Add project from disk**.
   - Select the cloned project folder.
3. **Launch the Simulation:**
   - In the Project window, open `Assets/Scenes/MainSolarSystem.unity`.
   - Press the **Play** button at the top of the Unity Editor.

---

## 📌 Future Scope

- 🚀 **3D Space Flight Simulator (3D SFS):**
  - Interactive spacecraft construction with staged rockets, thrusters, and fuel consumption.
  - Realistic trajectory planning (Hohmann transfer orbits, gravity assists, and orbital insertions).
- 🌌 **N-Body Gravitational Physics:**
  - Transition from two-body Keplerian approximations to full numerical N-body integration (Runge-Kutta 4th order / Verlet) for gravitational perturbations.
  - Simulation of Lagrange points ($L_1$ to $L_5$) and three-body chaotic systems.
- 🌠 **Expanded Deep-Space & Minor Bodies:**
  - Main Asteroid Belt and Kuiper Belt particle systems.
  - Comets with dynamic ion and dust tails shaped by solar wind vectors.
- 🌅 **Advanced Atmospheric Optics:**
  - Volumetric Rayleigh and Mie atmospheric scattering shaders for realistic planetary sunsets.
  - Real-time planetary ring shadowing onto planet surfaces and eclipse shadows.
- 🎮 **VR / XR Space Walk Support:**
  - Immersive first-person headset exploration and educational tour modes.

---

## 📄 License & Attribution

Developed for academic purposes within the **Computer Graphics & Image Processing (CGIP)** curriculum. Planetary data and ephemerides derived from **NASA/JPL Solar System Dynamics**.
