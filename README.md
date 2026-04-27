# Kill capsules

## Context
This project is the result of Modules 02 and 03 of 42 school Unity Piscine : 7 modules made to learn Unity fundamentals.  
We learn about 2D environments, tiles, sprites, and UI.  

## Game Overview : 2D Tower defense
### Gameplay
- Enemies (capsules) spawn at a fixed rate at the top of the map and follow the path toward the player base (blue circle)
- Base starts with 5HP (health points) and 80 energy (money)
- Lose 1 HP for each enemy reaching the base  
- Game is over when the base reaches 0HP
- The player can defend the base by placing turrets on dedicated bases (white squares) that will shoot enemies
- Enemies start with 3HP, detroyed at 0HP
- Gain 10 energy for each enemy killed

### Turrets
- fast fire rate, low damage, cost 50
- medium fire rate, medium damage, cost 80
- low fire rate, high damage, cost 100

### Controls
- Placing turret: drag&drop from UI to empty turret base
- Pause: Escape

### Level end
- Win if no more enemies to spawn and the base still has HP
- Final rank among 6 possible ranks, based on remaining HP and energy

---

## Technical Details
### Architecture
- 2 levels : `map01` and `map02` scenes
- 3 UI Scenes : Menu / Score / EndGame
- Separation between gameplay logic and UI
- Event-driven architecture (C# Actions) to decouple systems
- A `GameManager` in each map scene: controls level end, scenes, pause, calculate rank, remove/add energy to base
- `EnemySpawner` instantiate enemies with a `EnemyController` script and initialize them with HP, speed, waypoints.

### Unity/C# Features Used
- Tile Palette
- Trigger detection
- Drag and Drop
- UI handling, TextMeshPRO
- Scenes management (Menu / Level Map / Score / EndGame)
- C# Actions (events) to communicate between scripts
- Waypoints

## Preview

### Main Menu
<img width="1335" height="752" alt="image" src="https://github.com/user-attachments/assets/4bd110be-12fc-41f0-bc51-81cf0c21ec38" />

### Map 1  
<img width="1369" height="769" alt="image" src="https://github.com/user-attachments/assets/c6bd8f66-c0a3-40ce-9a1e-999106c1c86f" />

### Map 2  
<img width="1365" height="769" alt="image" src="https://github.com/user-attachments/assets/ba542b45-1a73-4b50-86fb-cbb59a57ccae" />

### Pause screen
<img width="1370" height="770" alt="image" src="https://github.com/user-attachments/assets/f7bd8e84-c31e-44cc-bac2-df47615fdc7b" />

### Score screen
<img width="1368" height="768" alt="image" src="https://github.com/user-attachments/assets/38c728de-c4cd-420d-9142-66ff20f27016" />

### Endgame screen
<img width="1367" height="769" alt="image" src="https://github.com/user-attachments/assets/4f2b643f-72e6-4809-847e-46d35efd42d7" />




