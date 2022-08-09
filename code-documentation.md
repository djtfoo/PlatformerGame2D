# Code Design

The 2D Platformer game is developed in Unity. Thus, the game makes use of Components provided by Unity, and the code design follows the Entity Component System.

## Player

The player is a GameObject in the Scene. Movement and collisions are handled by Unity's Physics2D engine.

The player has the `Rigidbody2D` and `BoxCollider2D` Components, which are handled by the game physics, and the `PlayerState` and `PlayerControl` Components.

### PlayerState

`PlayerState` holds the current state of the player, such as:
1. `score`: the points earned during the level (resets after death)
2. `numLives`: the number of lives remaining
3. `isDead`: whether the player is currently dead
4. `hasWon`: whether the player achieved the win condition

### PlayerControl

`PlayerControl` defines the controls for moving the player. These are the player controls:
1. The `"Horizontal"` Input Axis is used for detecting user input for walking.
2. The `Jump` Button is used for detecting the jump, and can only jump.

To perform a jump, the player needs to be in contact with the ground. This is checked using a `ContactFilter2D` to check that the player object is in contact with a `Collider2D` in the direction of the ground's normal angle.

The following variables are exposed in the Unity Inspector for adjusting the player movement:
1. `moveSpeed`: the magnitude of the X velocity to be set for walking
2. `jumpForce`: the magnitude of force to apply upwards when doing a jump

## Camera

The sidescrolling tagalong movement of the camera is controlled by `CameraController`.

The camera tracks a target Transform object in the Scene. In this case, the object being tracked is the player. The camera moves forward if the tracked object has moved forward in the positive X-axis past its last maximum X-axis position.

### Boundary

A child object with a `BoxCollider2D` is attached to the camera and placed outside the viewport of the camera to prevent the player from leaving the camera view and going to a previous part of the map.

(Future to-do: lock the Camera movement up to the end of the map (i.e. not allowed to scroll past the end of the map))

## Objects

![Object Class Diagram](images/class-diagram.png?raw=true "Class Diagram")

All instantiated non-player entities in the game are derived from the `Object` MonoBehaviour abstract class. There are 4 (only 3 implemented thus far) types of `Objects`:
1. `Tile`: A collidable object which blocks the player's movement, thus acts as walkable platforms or obstructions.
2. `Consumable`: A trigger-collidable object which triggers `Effects` when the player comes into contact with it. The naming does not reflect that the Object is necessarily destroyed after its effect is triggered.
3. `WinGameObject`: An object which provides a win condition. There is only one implemented example, `Checkpoint`.

`InteractableTile` is a derived class of `Tile`, and defines a `Tile` which the player can trigger `Effects` if it comes into contact with the bottom edge of the `Tile` (i.e. by jumping).

### Effect

The behaviour of `InteractableTile` and `Consumable` objects can be customised by triggering 1 or more `Effects`. `Effect` is a MonoBehaviour class with an abstract function, `TriggerEffect(Collision2D)`. The following `Effects` have been implemented thus far:
1. `AppearOnHit`: Disables a target `SpriteRenderer` until this `Effect` is triggered. Used by the 'Invisible Tile' in the demo.
2. `GainPoints`: Increases the Player's `score` by a variable amount. Used by the 'Cherry' in the demo.
3. `HurtPlayer`: Reduces the Player's life by 1 and triggers their death. Used by the 'Spike' in the demo.
4. `DestroyObject`: Destroys the `Object` after triggering the `Effect`. Used by the 'Cherry' `Consumable` in the demo.

## GameStateManager

The `GameStateManager` tracks the overall state of the game, and triggers the game over screen when the player(s) have won or lost the game.

Some game state information is exposed and can be accessed by other classes. As there can only be one `GameStateManager`, it is a singleton.

Other classes can access the `GameStateManager` singleton object via:

```
GameStateManager.Instance
```

## Map

### Global Map Data

![GlobalMapData ScriptableObject](images/global-map-data-so.png?raw=true "GlobalMapData")

`GlobalMapData` is a ScriptableObject to save shared map information used by the game and map editor.
- `gridXSize`: the width of a single grid object, in Unity's coordinate system
- `gridYSize`: the height of a single grid object, in Unity's coordinate system
- `mapHeight`: the fixed map height for all maps in terrms of number of grids
- `objectData`: the object/tile set for generating the map

A single `ObjectData` consists of:
- `id`: character to represent the Object in the map data
- `obj`: reference to the `Object` Prefab for instantiating new instances
- `sizeX`: the width of the sprite in terms of number of grids
- `sizeY`: the height of the sprite in terms of number of grids

### Map Data

The map consists of a series of grids, with an `Object` (or none) occupying a grid. This grid occupancy represents the map, and is represented as a 2-dimensional character array that is stored in a text file.

![Example of the map data](images/mapdata-example.png?raw=true "Map data represented in 2d array")

Each grid is represented by the `id` of the `Object` that resides in it, or '0' if the grid is unoccupied. Every Object only occupies one grid regardless of the size of the sprite. Thus, an Object could visually look like it occupies more than one grid. For Objects that are visually larger than a single grid, its position is snapped to the bottom of the grid.

In the Map Editor, the `MapEditor` Component maintains a copy of this character array, and updates it when an Object is added or removed from a grid. When saving edited maps, this maintained character array is written to the specified file path.