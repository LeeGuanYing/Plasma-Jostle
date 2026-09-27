# PJ Player Framework Patch

Base: LeeGuanYing/Plasma-Jostle
Unity target: 6000.6.0f1

This patch adds the first-person Player framework without modifying Core or Creature scripts. It uses the existing `Assets/InputSystem_Actions.inputactions` Player map (`Move`, `Look`, `Jump`, `Crouch`, `Interact`).

## Apply
1. Extract this archive into the root of a fresh Plasma-Jostle checkout, merging the `Assets` folder.
2. Open the project in Unity 6000.6.0f1 and wait for compilation.
3. Run `PJ > Build Player Framework Test`.
4. Open `Assets/PJ/Scenes/PlayerFrameworkTest.unity`.
5. Press Play.

Controls: WASD/arrows move, mouse look, Space jump, existing Crouch action crouches, existing Interact action (Hold) raycasts. Escape unlocks the cursor; left click relocks it.

The builder intentionally creates the prefab and test scene inside Unity instead of shipping hand-written Unity YAML. This avoids depending on undocumented serialized scene/prefab internals.

Verification limitation: Unity Editor is not available in this environment, so final compile/runtime verification must be performed in Unity 6000.6.0f1.
