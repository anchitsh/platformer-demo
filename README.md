# Platformer Demo

The character controller can move around and jump with arrow keys or WAD.

Ive added some extra functions to make the controller feel smooth, for example-

Variable jump - The jump height is based on how long the jump button is pressed.

Fall multiplier - Gravity is increased for the second half of the jump when the character is falling down. 

Coyote jump - The character can still jump for a few frame after leaving a platform.

Buffer jump - if the player tries to jump up a bit before landing, the controller will accept this input for a few frames before landing and perform a jump.

Other things like squashing and stretching while jumping, particle effects, and a few more details make the character feel even better.

Also The Main scene loads up the level from LevelData.xml
