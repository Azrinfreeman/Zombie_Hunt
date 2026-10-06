# Zombie Hunt

A low-poly, first-person zombie survival game built with Unity and C#. Explore three stages, collect weapons and ammunition, defeat special infected enemies, and reach the escape point to progress through the story.

This repository contains the Unity project source. The features below were reviewed in source and scene configuration; a fresh playable build has not been verified. See [validation notes](docs/VALIDATION.md) for the review scope and suggested playthrough.

## Gameplay and engineering highlights

- **First-person movement and combat:** a `CharacterController` handles movement and mouse look, with projectile shooting and animated melee attacks.
- **Weapons and pickups:** scripts support a crowbar, Glock pistol, AK47, and machete, along with ammunition and health pickups.
- **Enemy behavior:** `NavMeshAgent` enemies chase the player, attack at close range, and update boss health and escape objectives.
- **Three-stage progression:** level objectives lead into escape events and video cutscenes, followed by a victory scene.
- **Player feedback:** health and ammunition displays, pickup messages, an objective panel, pause controls, and separate game-over scenes for each level.

## Open the project

1. Clone or download the complete repository, keeping Unity `.meta` files alongside their assets.
2. Add the repository root in Unity Hub and open it with **Unity 2020.3.14f1**, the version recorded in [ProjectVersion.txt](ProjectSettings/ProjectVersion.txt).
3. Allow Unity to import assets and restore the dependencies in [Packages/manifest.json](Packages/manifest.json). The project includes TextMesh Pro 3.0.6, Unity UI 1.0.0, and Timeline 1.4.8.
4. Open `Assets/Scenes/Main.unity`, enter Play mode, and use the start button to begin the opening cutscene.
5. For a desktop build, check **File → Build Settings** against the existing [scene list](ProjectSettings/EditorBuildSettings.asset). All 11 configured scenes are enabled; keep their order for the menu, cutscenes, levels, and game-over routes.

The scripts use Unity's legacy Input Manager. Start with the recorded editor version; an upgrade has not been tested. This review did not open the project in Unity or produce a release build.

## Controls

| Input | Action |
| --- | --- |
| W / A / S / D or arrow keys | Move |
| Mouse | Look around |
| Left mouse button | Fire or use the equipped melee weapon; hold for weapons configured for automatic fire |
| R | Reload the pistol or AK47 |
| Left Shift | Increase movement speed while held |
| 1–4 | Select a weapon slot, once that slot is available |
| Hold Tab | Show objectives |
| E | Read notes or activate an eligible escape event when prompted |
| Escape | Toggle the pause menu |

These controls come from [PlayerController.cs](Assets/Player/PlayerController.cs), [EventController.cs](Assets/Player/EventController.cs), and [InputManager.asset](ProjectSettings/InputManager.asset). Weapon slots follow pickup order. The Input Manager defines a Jump axis, but the reviewed player controller does not implement jumping.

## Level flow

| Stage | Initial objective | Progression |
| --- | --- | --- |
| Level 1 | Collect the pistol and ammunition, then head toward the garage | Defeat the special infected and activate the car escape event |
| Level 2 | Explore the blocked highway for useful supplies | Defeat the special infected and escape using the orange car |
| Level 3 | Explore the residential area | Defeat the special infected and activate the final box escape event |

The configured route is `Main → cutscene1 → level1 → cutscene1_1 → level2 → cutscene1_2 → level3 → cutscene_victory`. Player death routes to `GameOver1`, `GameOver2`, or `GameOver3`; their controller provides level retry and return-to-menu actions. These routes are documented from scripts and build settings, pending a complete playthrough.

## Source guide

| Area | Main files |
| --- | --- |
| Player movement, firing, melee, and weapon switching | [PlayerController.cs](Assets/Player/PlayerController.cs) |
| Weapon state and projectile damage | [GunManager.cs](Assets/Player/GunManager.cs), [BulletController.cs](Assets/Player/BulletController.cs) |
| Weapon, ammo, and health collection | [WeaponPickup.cs](Assets/WeaponPickup.cs), [AmmoPiickup.cs](Assets/Player/AmmoPiickup.cs) |
| Enemy chasing, attacks, and health | [EnemyController.cs](Assets/EnemyController.cs), [EnemyHealth.cs](Assets/Enemy/EnemyHealth.cs) |
| Player health and death routing | [PlayerHealthController.cs](Assets/PlayerHealthController.cs) |
| HUD, pause menu, and objectives | [UIController.cs](Assets/Player/UIController.cs), [EventController.cs](Assets/Player/EventController.cs) |
| Menu, cutscenes, and retries | [MenuController.cs](Assets/MenuController.cs), [cutscene.cs](Assets/cutscene.cs), [GameOver.cs](Assets/GameOver.cs) |

## Project status and assets

This is an existing game project being documented for portfolio review. A complete three-level playthrough, desktop build, and video playback check remain to be done. The [validation notes](docs/VALIDATION.md) also record specific source observations to check during that playthrough.

Models, animation, audio, video, and other imported assets may have separate license terms. This documentation does not establish ownership or redistribution rights for those assets or add a repository-wide license. Review the original asset licenses before redistributing a build or reusing the content.
