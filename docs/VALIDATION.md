# Documentation validation

Reviewed on 2026-10-06 against `main` commit `409768b10d568291f148b0c26d0e0e5a2a1f2e0d`.

## Checks completed

- Confirmed Unity 2020.3.14f1 and the package versions reported in the README from project metadata.
- Confirmed 11 enabled scenes: the main menu, four cutscenes, three gameplay levels, and three game-over scenes. Checked scene paths against the tracked tree.
- Reviewed player movement, weapon switching, reload and melee inputs, pickups, projectile damage, enemy chasing, health, UI, escape events, and scene-transition scripts.
- Confirmed keyboard movement mappings in the legacy Input Manager and documented controls from the player and event scripts. Did not infer jumping from the presence of a Jump axis.
- Checked README links against tracked repository paths and checked the documentation diff for whitespace errors.
- Restricted changes to README, these validation notes, and ignore rules for future local logs and editor preferences. Existing tracked logs and preferences remain in the repository.

## Runtime checks still needed

The Unity Editor, automated Unity tests, video playback, and a desktop build were not run during this review. The following is a manual validation plan, not a report of passing tests:

1. Open the complete project in Unity 2020.3.14f1, allow imports to finish, and inspect the Console for errors.
2. Start at `Main`, watch the opening video, and confirm the next button leads into level 1.
3. In each level, exercise movement, mouse look, sprint, objectives, weapon pickups and switching, melee, shooting, reloading, ammo pickups, and health pickups.
4. Check enemy navigation and attacks, boss health, updated objectives, and escape prompts. Complete all three levels and the victory route.
5. Test pause/resume, restart, returning to the menu, and each level's death/retry route.
6. Build for the intended desktop target and repeat the cutscene and level-transition checks outside the Editor.

## Source observations to verify

- `UIController.quitLevel()` requests `"main"`, while the configured scene is `Main.unity`. Verify the return-to-menu route on the intended build target.
- The cutscene and game-over scripts subscribe to `VideoPlayer.loopPointReached` inside `Update()`. Check for repeated callbacks and next/retry button behavior.
- Several player input paths, including reload and weapon switching, are outside the pause-panel guard. Verify their behavior while paused.
- The escape trigger condition in `EventController.OnTriggerEnter()` combines `&&` and `||` without grouping the boss flags. Check that non-player colliders cannot activate escape prompts.

These existing behaviors were preserved. This documentation review does not certify runtime correctness, asset licensing, or repository history safety.
