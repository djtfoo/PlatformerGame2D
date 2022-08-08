# Code Design

The 2D Platformer game is developed in Unity. Thus, the game makes use of Components provided by Unity, and the code design follows the Entity Component System.

## Player

The player consists of 2 classes: PlayerState and PlayerControl.

The player's movement is done using the Physics2D engine. (Rigidbody2D)

## Camera

The Camera

## Objects

// insert class diagram here

The Object design is .

### Tile

A tile is a specific type of Object which has a main purpose of acting as a platform in the map. It acts as a (or to stop the )

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