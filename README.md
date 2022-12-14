# PlatformerGame2D
A simple 2D platformer game developed in Unity 2021.3.6f1.

## Development

Engine: Unity 2021.3.6f1<br>
Target platform: Windows<br>
Resolution: 848x480 (Windowed)

Read more about the code documentation <a href="./code-documentation.md">here</a>.

## Gameplay
![Gameplay Demo](./images/game-demo.gif)

### How to Play

- Use the arrow keys to move and space to jump.
- Win by reaching the checkpoint, or lose if all lives are lost.
- Collect fruits for points.
- Hit tiles from beneath for points. Some tiles are 'hidden', appearing only after being hit.
- Avoid spikes and falling out of the map, which causes the player to lose 1 life and to restart from the beginning of the map.

### Technical Features

- Map loaded from text file (StreamingAssets)
- Platformer mechanics implemented using Rigidbody2D
- InputManager for player controls
- Tagalong camera
- User interface for displaying game state
- User feedback (visual, audio)

### Making Changes

Launch the project in Unity 2021.3.6f1.

Open SampleScene (Assets/Scenes/SampleScene.unity).

### Build

The game can be built for Windows platforms.


## Map Editor
![Gameplay Demo](./images/editor-demo.gif)

### How to Run

Launch the project in Unity 2021.3.6f1. Open MapEditorScene (Assets/MapEditor/Scenes/MapEditorScene.unity).

Before playing the scene, specify the map width and map file to load (if it does not exist) in the Map Editor Component.

![Gameplay Demo](./images/map-editor-script.png)

Controls:
- Left click on an object from the top bar to select it.
- Left click on a tile space to place it on the map.
- Select the empty grid (top left) to erase existing tiles.

Click on the Save button (bottom right) to save to the file. It creates a new file if it does not exist yet.

## Credits

### Sprites
- <a href="https://rvros.itch.io/pixel-art-animated-slime">Animated Pixel Slime</a> by rvros 
- <a href="https://arks.itch.io/dino-characters">Dino Characters</a> by <a href="https://twitter.com/ArksDigital">@ArksDigital</a> on Twitter
- <a href="https://pixelfrog-assets.itch.io/pixel-adventure-1">Pixel Adventure</a> by Pixel Frog
- <a href="https://ansimuz.itch.io/mountain-dusk-parallax-background">Mountain Dusk Parallax Background</a> by ansimuz
- <a href="https://pimen.itch.io/smoke-vfx-1">Smoke Effect 01</a> by pimen
- <a href="https://omniclause.itch.io/spikes">Spikes</a> by omniclause

### Audio
- <a href="https://tallbeard.itch.io/music-loop-bundle">[Music Assets] FREE Music Loop Bundle</a> by Tallbeard Studios
- <a href="https://jdwasabi.itch.io/8-bit-16-bit-sound-effects-pack">8-bit/16-bit Sound Effects (x25)</a> Pack

### Fonts
- <a href="https://www.fontspace.com/press-start-2p-font-f11591">Press Start 2P Font</a> by codeman38