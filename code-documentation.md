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
1. AppearOnHit: Disables a target `SpriteRenderer` until this `Effect` is triggered. Used by the 'Invisible Tile' in the demo.
2. GainPoints: Increases the Player's `score` by a variable amount. Used by the 'Cherry' in the demo.
3. HurtPlayer: Reduces the Player's life by 1 and triggers their death. Used by the 'Spike' in the demo.
4. `DestroyObject`: Destroys the `Object` after triggering the `Effect`. Used by the 'Cherry' `Consumable` in the demo.

## GameStateManager

The `GameStateManager` tracks the overall state of the game, and triggers the game over screen when the player(s) have won or lost the game. As there can only be one `GameStateManager`, it is a singleton, and exposed game state information can be accessed by other classes.

## Map

### Global Map Data

A Unity ScriptableObject is used to store the shared map information:
- The map height (side-scrolling only, not vertically)
- The object/tile set for generating the map

A single ObjectData consists of:
- reference to the Object Prefab
- size X, Y of the Object sprite (in terms of no. grids)

### Map Generation

The map data is loaded from a text file.

Each grid consists of an Object. If the grid is unoccupied, it is '0'. Every Object only occupies the space of one grid regardless of the size of the sprite. Thus, an Object could visually look like it occupies more than one grid. For Objects that are visually larger than a single grid, its position is snapped to the bottom of the grid.

### Map Editor

The Map Editor stores an occupancy grid as a 2-dimensional character array. When an Object is added or removed from a grid, the occupancy grid is updated.

On saving, the occupancy grid is written to a text file.