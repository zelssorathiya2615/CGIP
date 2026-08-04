# 🚀 3D Space Exploration

> **Computer Graphics and Image Processing — CMP513 + CMP514**
>
> An interactive 3D space exploration and educational visualization system that combines real-time, visually rich rendering with scientifically grounded orbital motion and contextual, curiosity-driven educational content.

---

## 📖 Abstract

Existing space-simulation software tends to fall into one of two categories: scientifically rigorous tools with limited visual polish (Spaceflight Simulator, Kerbal Space Program), or visually accomplished tools designed primarily for data browsing rather than guided learning (NASA's Eyes on the Solar System).

This project proposes an interactive 3D space exploration and educational visualization system that combines real-time, visually rich rendering with scientifically grounded orbital motion and contextual, curiosity-driven educational content. Users can freely navigate a curated 3D representation of the Solar System — including the Sun, all eight planets, selected moons, and additional deep-space set-piece objects — with celestial bodies following simplified two-body Keplerian orbital motion for scientific authenticity.

---

## 👥 Team

| Sr. No. | Enrollment No. | Name              |
| ------- | -------------- | ----------------- |
| 1       | 24000636       | Sampatti Dave     |
| 2       | 24000859       | Zels Sorathiya    |
| 3       | 24000925       | Hriday Joshi      |

**Date of Submission:** 20-07-2026

---

## 📁 Project Structure

```
CGIP/
├── docs/                   # Documentation and reports
├── src/
│   ├── gui/                # GUI module
│   ├── processing/         # Image processing module
│   └── main.py             # Entry point — Hello World pipeline test
├── tests/                  # Unit tests
├── requirements.txt        # Python dependencies
├── .gitignore              # Git ignore rules
└── README.md               # This file
```

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/<your-username>/CGIP.git
cd CGIP
```

### 2. Create a Virtual Environment

```bash
# Using venv
python -m venv venv

# Activate (Windows)
venv\Scripts\activate

# Activate (macOS/Linux)
source venv/bin/activate
```

### 3. Install Dependencies

```bash
pip install -r requirements.txt
```

### 4. Run the Hello World Pipeline Test

```bash
python src/main.py
```

**Expected Output:** A window titled *"Pipeline Test"* displaying edge-detected text "CG & IP Pipeline OK" — confirming that OpenCV, NumPy, and GUI rendering are all working correctly.

---

## 🔧 Technology Stack

| Component              | Selected Tool                          |
| ---------------------- | -------------------------------------- |
| Programming Language   | C#                                     |
| Rendering Framework    | Unity / Godot                          |
| Shader Authoring       | Unity Shader Graph                     |
| Camera System          | Cinemachine (Unity Package)            |
| GUI                    | Unity UI Toolkit / UGUI                |
| IDE                    | VS Code (or other alternatives)        |
| Version Control        | GitHub                                 |

### Python Dependencies (for pipeline testing)

| Library          | Purpose                        |
| ---------------- | ------------------------------ |
| `opencv-python`  | Image processing & display     |
| `PyOpenGL`       | OpenGL rendering               |
| `PyQt5`          | GUI framework                  |
| `numpy`          | Numerical computing            |
| `matplotlib`     | Data visualization & plotting  |

---

## 🎯 Objectives

1. Render the Sun, eight planets, and a curated set of moons in real time with physically based materials and scientifically informed relative scale
2. Implement two-body Keplerian orbital motion so that all celestial bodies move along scientifically accurate paths over time
3. Implement free-roam 3D camera navigation allowing unrestricted movement through the environment
4. Implement a floating-origin and level-of-detail (LOD) system to maintain rendering precision and performance across astronomical distances
5. Implement custom shader-based visual effects, including Fresnel-based atmospheric rim-lighting and a real-time starfield/skybox
6. Implement a contextual encyclopedia system that displays curated educational content when the user approaches a celestial object
7. Implement a data-driven content architecture (JSON-based celestial body and encyclopedia definitions) to support future extensibility
8. Architect core systems so that a future rocket-building and trajectory-simulation module can be integrated without a fundamental redesign

---

## 🔬 Algorithms & Techniques

- Kepler two-body orbital mechanics (position from orbital elements as a function of time)
- Floating-origin technique for large-scale coordinate precision
- Level-of-detail (LOD) selection based on camera distance
- Physically based rendering (PBR): albedo / roughness / metallic material model
- Fresnel-effect shading (via Shader Graph) for atmospheric rim-lighting
- Real-time shadow mapping
- Particle system simulation for starfield and nebula effects
- Camera interpolation and blending (Cinemachine) for smooth transitions
- Skybox / environment mapping for the background starfield
- Proximity and trigger-volume detection for the object-approach interaction system

---

## 📊 Work Breakdown

| Activity | Sampatti | Zels | Hriday |
| -------- | :------: | :--: | :----: |
| Literature Survey | ✔ | | |
| Data Layer & Orbital Mechanics | ✔ | ✔ | |
| Scale Manager (floating origin, LOD) | ✔ | ✔ | |
| Rendering & Shaders (PBR, Fresnel, particles, post-processing) | | ✔ | ✔ |
| Camera System & Interaction (Cinemachine, approach/focus) | | ✔ | ✔ |
| Encyclopedia Content (research, writing, data entry) | ✔ | | ✔ |
| UI (menu, HUD, info panel) | ✔ | ✔ | ✔ |
| Integration & Testing | ✔ | ✔ | ✔ |
| Documentation (SRS/SDD) | ✔ | | |
| Demo Video & Presentation | ✔ | ✔ | ✔ |

---

## 📅 Timeline (10 Weeks)

| Week | Activity |
| :--: | -------- |
| 1 | Environment & tooling setup; finalize JSON schema for celestial body and encyclopedia data |
| 2 | Core scene setup and free-roam camera navigation controller |
| 3 | Orbital Motion Calculator (Kepler two-body); populate celestial body data |
| 4 | Scale Manager: floating-origin system and LOD implementation |
| 5 | Rendering Engine: PBR materials, Fresnel atmospheric rim-lighting shader, skybox/starfield |
| 6 | Interaction Manager and Encyclopedia Content Manager |
| 7 | Encyclopedia content population and deep-space set-piece destinations (black hole, nebula) |
| 8 | Visual polish: post-processing pipeline (bloom, color grading, depth of field), particles, Cinemachine |
| 9 | Integration, full playtesting, bug fixing, performance testing |
| 10 | Demo video recording and final presentation preparation |

---

## 🌿 Git Branching Strategy

- **`main`** — Protected branch, requires PR review before merge
- **`feature/ui-setup`** — GUI development
- **`feature/image-loader`** — Image loading & processing
- Individual feature branches for each team member

### Branch Naming Convention

```
feature/<short-description>
bugfix/<short-description>
hotfix/<short-description>
```

## 🤝 Workflow

1. Create a feature branch from `main`
2. Implement your changes
3. Push your branch and open a Pull Request (PR)
4. Get a peer code review from at least one team member
5. Merge into `main` after approval

---

## 📝 License

This project is for academic purposes as part of the CG & IP coursework (CMP513 + CMP514).
