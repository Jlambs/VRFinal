# CSCI 4830 Final Project

Julian Lambert
Fall 2019

## Game Overview

This (unnamed) project is designed around the idea of making a game to teach children programming concepts in VR, while having a goal of never explicitly using numbers of symbology of any type during the game itself. This was attempted through the construction of "marble puzzles" in a 3-dimensional space. The player is given template of available pieces (which are essentially pipes) and must use those to construct a track to transport marbles from one destination to another. The marbles themselves possessed a color property, which could have operations done to it according to ink mixing rules (RYB), allowing for more complex goals to be added to courses, such as "turn every marble brown", or "sort these marbles into warm colors and cools colors". These more complex goals can be completed with a number of special "operation" pipes, such as switches that can be toggled, color adders/subtractors, color equality checks, accelerators/decelerators, and many more.

The struggle of balancing what pieces to design came from how many concepts could be conveyed intuitively by marbles, while all working together still making a robust "programming language" (very large air quotes). The final piece set was settled upon with a goal of being able to solve a sufficiently complex programming problem, such as sorting a randomized list of input marbles by color, but also to not be overwhelming in choices. Some liberties were taken in favoring usability and straightforwardness over generality for both the player's sake and the programmer's sake, and it should be noted that "sort these marbles by color" in an algorithmic sense is far more difficult than what would typically be expected of the target audience.

## Core Gameplay

The major elements of this game that build together to make this "programming language" are as follows:

### Marbles

Marbles can take on the following colors: white, red, yellow, blue, orange, purple, green, brown. Colors mix as you would expect them to, so red + yellow = orange, and orange + blue  = brown, and their corresponding unmixing rules, but some unusual things happen in the edge cases. For example, red + red = red, and yellow - blue = yellow. This can be broken down to Boolean logic statements, where in fact the marble's color is a 3-bit system (corresponding to 2^3 = 8 total combinations, where (0,0,0) is white and (1,1,1) is brown), and the rules for addition is a simple A OR B operation, however subtraction is more complicated, A OR !(A AND B). This system of "color math" unfortunately doesn't lend itself well to translating to our traditionally 1-bit thinking of numbers, but could still be a useful learning tool for children nonetheless.

### Adder/Subtractor

These are simply pipes with either a '+' pattern or '-' pattern on it (I know I violated my no-symbols rule, but I was rather uncreative when it came to creating these original assets, as you will see is a recurring trend). Depending on their color, they will add or subtract that color from any marble that rolls through them. This represents basic addition/subtraction operators used in normal programming, with their *specific* definitions discussed above.

### Switches

Switches are undoubtedly needed in some capacity to represent control flow, but the exact implementation was difficult to decide on. The two major candidates are a switch which can either be set one direction *or* the other, or a switch which can only be toggled. In light of the 'Condition' piece to be discussed further down, a pure toggle format was chosen in order to be able to implement simple counting systems if needed. For example, if the user wanted to check every third ball (regardless of color), they could simply have balls flow through a series of two toggle-only switches (0: NN, 1: YN, 2:, NY, 3: YY->is checked). Using the Condition pieces alone would be surprisingly cumbersome to recreate this behavior otherwise, however to make a conditional switch you just simply need to toggle the switch *again* after a positive input to reset it.

### Accelerator/Decelerator and Jumps

These pieces are of underrated importance, as without an explicit 'GOTO' operation (for example, a teleporter of some kind), implementing complex control flow and/or loops becomes only possible with the use of Generators (also discussed below), which would be somewhat bad practice regardless due to creating many junk marbles for 'meta'-computations. The actual performance of these pieces proved to be somewhat problematic, as their design was a strict velocity multiplier, which could very quickly start to send balls too quickly for the physics engine to handle. Some other arial pieces were experimented with to encourage this mechanic, namely trampolines, however more time will need to be spent on designing pieces for actually catching marbles in the future (aligning everything to a grid is surprisingly restrictive when dealing with precise jumps in 3-dimensional space). A future piece akin to a 'cannon' would also be useful for more consistent jumps to be able to be made.

### Generator

This simply creates a new marble of a specified color. The 'create marble' action is triggered by a Condition, whereas the color is set by a 'Color Setter' (also to be defined below). There is no punishment for generating new marbles, but they will begin to physically fill the floor of the play environment if an excess is created. These "dead marbles" could be used in a scoring system in the future.

### Conditions

These were one of the least satisfying pieces to design, but simultaneously possibly one of the most important for the robustness of then programming language. It is simply a check for a marble's color (represented by a colored portal surrounding a pipe), that will either check for exact equality (==, square portal), or "does the marble contain this color?" (>=, circle portal). The marble simply rolls through this check regardless of passing or failing, but if the condition is met then a signal is sent out to any connected objects (also defined by the player). A Condition can trigger many pieces, as well as other Conditions, and a piece may be triggered by many different Conditions.

The reason why this implementation felt unsatisfying is because the concept is somewhat unintuitive already, and is an incomplete function set due to missing <= (which of course could easy be included by "...does not contain...", but is very clunky to try and mentally figure deduce subtraction of colors and edge case rules). The player can toggle arrows physically connecting these elements, however if there were some way to physically represent this entire concept without the need of arbitrary connections that would be vastly preferable.

### Color Setters

Similar to Conditions, these send out a signal to other pieces to change their current color (e.g. change the color of an adder to set a new value that it will add). Unlike the Condition however, its own color is set when a marble rolls through it, and instead the trigger is propagated by a *Condition*. So marble->Condition=trigger,but marble->Color Setter=set color of color setter. These two elements could have possibly both been lumped together as one item (just don't set a color if the piece doesn't have one, e.g. a switch would still just be toggled instead), however it seemed to be merging the ideas of a comparison operator, which returns a boolean, and an actual function, which in this case is returning a color.

### Gate

The final piece was originally born as a timing component, but is actually quite useful for logical control in general. It's simply a pipe that has a barrier up until it is triggered by a Condition, and then it will let exactly 1 marble through. Additionally, you can initialize a set number of marbles you would like to go through at the start (hopefully this can be restricted to just 1 and 0 though).

### Normal pipes

Not to be underestimated, normal pipes (straight pieces, curves, etc) actually represent a very important function too: the linking of statements into a sequence. Gravity roughly then acts as an operation step, and with the use of generators, gates, and switches, some crude "parallel" applications can be designed too!


## Results, Conclusions, and Future Work

Overall the completed product turned out better than originally expected, thanks in part to the availability Unity ProBuilder, which allowed for easy creation of custom geometries, however this also proved to be its downfall: the high polygon count in all the circular pipes involved with the demonstration alone severely lagged any phone attempting to run it. This was actually more devastating that originally anticipated, because it turns out Unity's physics begin to behave differently at very low frame rates, and the difference between using Update() and FixedUpdate() become very apparent. Running on a PC this ended up not being too big of an issue however, so future implementations will want to prioritize stable running environments before making the leap to VR capabilities. When consistent physics could be expected however, the implementations of the pieces worked surprisingly consistently, and the next steps would be focusing on removing some of the tedium in creating large level designs over moving singular pieces at a time.

Once all of the mechanical kinks are figured out, focusing on expanding the scope of this game to going beyond just being a puzzle game would be very important. Kids are hypersensitive to learning experiences veneered in gameplay, and in its current state it feels more like a difficult puzzle than a fun game. Ideas with how to make the output more exciting, including translating these ideas into musical concepts (a-la Minecraft's red stone and note-blocks) could have very strong potential, for example.

## Asset Credits

https://assetstore.unity.com/packages/audio/sound-fx/horror-elements-112021
https://assetstore.unity.com/packages/3d/props/horror-assets-69717
https://assetstore.unity.com/packages/3d/props/furniture/horror-school-props-112589
https://assetstore.unity.com/packages/3d/environments/swamp-house-1-153759

## Game Trailer

<coming soon!>