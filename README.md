# Assignment 5: Rendering and Physics - Godot Project

### Work in progress

This project is **far from complete**, but the three main systems are partially functional:
- ✅ Custom particle shader with wave effects and color gradients
- ✅ Physics chain with joint connections hanging realistically
- ✅ Laser raycast system with player detection (mostly working)
- ❌ Player movement is glitchy and needs debugging
- ❌ Laser detection still needs refinement

---

## Overview

This is a Godot 4.x C# project for Assignment 5, demonstrating:
1. **Custom Canvas Item Shaders with Particles**
2. **Rigid Body Physics with Joints**
3. **Raycasting and Collision Detection**

---

## Part 1: Particle Shader ✅

The particle system uses a custom shader (`custom_particle.gdshader`) that:
- Applies a **wave distortion** effect based on time
- Creates a **color gradient** transitioning from orange to pink
- Animates smoothly using the TIME uniform

**Implementation**: `ParticleController.cs` loads the shader and configures 50 particles with a 3-second lifetime.

---

## Part 2: Physics Chain ✅

A chain of 5 connected rigid body segments (green squares) that:
- Hang from a static anchor point at the top
- Connect via `PinJoint2D` joints with softness of 0.5
- Swing and rotate realistically under gravity
- Stay connected without breaking apart

**Implementation**: `PhysicsChain.cs` creates and manages all segments and joints programmatically.

---

## Part 3: Laser Detector ⚠️

A laser security system that:
- Continuously casts a ray to the right
- Visualizes as a green line (red when alarm active)
- Detects when the player crosses it
- Flashes and prints "ALARM! Player detected!" when triggered

**Implementation**: `LaserDetector.cs` handles raycast updates and collision checking each frame.

---

## Known Issues

- **Player movement is buggy** — arrow keys sometimes move the laser instead of the player
- **Input handling conflicts** — need to debug input routing between Player and LaserDetector
- **Scene layering issues** — player and laser can overlap visually
- **Particles fall off-screen** — need world boundaries or constraints

---

## Files

- `ParticleController.cs` — Particle system with shader
- `PhysicsChain.cs` — Chain physics and joints
- `LaserDetector.cs` — Laser raycast and detection
- `Player.cs` — Player movement (needs fixing)
- `custom_particle.gdshader` — Wave and gradient shader
- `ChainSegment.tscn` — Chain segment prefab
- `main.tscn` — Main scene

---

## To Run

1. Open in Godot 4.x with C# support
2. Open `main.tscn`
3. Press Play (F5)
4. Try arrow keys (player movement is inconsistent — this is the main bug!)
5. Walk into the laser beam to trigger alarm

---

## What Works Well

- Particle shader effects are smooth and visually interesting
- Physics chain hangs and swings realistically
- Laser detection and alarm system work when you can move the player
- Code is well-organized and commented

---

## What Still Needs Work

- Fix player input handling (main priority)
- Add world boundaries to keep particles visible
- Improve collision detection between player and chain
- Clean up scene structure and rendering order
- Test and debug all three systems together

**Debugging and polish to come**
