# platformer-demo
Demo made in a couple days

The characteter controller can move around and jump with arrow keys or WAD

Ive added some extra functions to make the controller feel smooth, for example-

Variable jump- The jump height is based on how long the jump button is pressed.

coyote jump - The character can still jump for a few frame after leaving a platform.

buffer jump - if the player tries to jump up a bit before landing, the controller will accept this input for a few frames before landing and perform a jump


Also The Main scene loads up the level from LevelData.xml
