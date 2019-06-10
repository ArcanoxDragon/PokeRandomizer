# PokéRandomizer
A randomizer utility for Generation 6 Pokémon games.

This project was made possible by (and utilizes source code from) [kwsch](https://github.com/kwsch)'s [pk3DS](https://github.com/kwsch/pk3DS) and [pkNX](https://github.com/kwsch/pkNX) projects.

# Supported Games
This utility works with the following Pokémon games:

- Pokémon X & Y
- Pokémon OmegaRuby & AlphaSapphire

Support for newer games is planned in the future.

# Disclaimer
This utility is only for use with *legally obtained* copies of Pokémon X/Y or OmegaRuby/AlphaSapphire. I will not assist with ripping/dumping/otherwise obtaining a copy of any 3DS game. It is assumed that you already own a copy of one of these games and have somehow extracted the game to your own computer prior to downloading this utility.

# Using the randomizer
The user interface for the randomizer is fairly self-explanatory. When the app is first launched, it will ask you to pick a language as well as browse for a game folder. The game folder should be a folder containing the fully extracted contents of your Pokémon game that you wish to randomize (it should contain an `ExeFS` and a `RomFS` *folder*, among other files).

Once you have chosen a game folder, the options screen will open. Here you may customize all of the settings the randomizer has to offer, each affecting a different aspect of the randomizer's behavior. By hovering over an option with your mouse, you can read a detailed explanation of how that option affects the randomizer in the bottom area of the screen.

Using the buttons on the left side of the randomizer, you can import or export your settings to a `.json` file so it's easy to recall your favorite configurations in the future.

Once you have adjusted the settings to your liking, you should click the `Set output path...` button. The app will ask you to choose a folder in which to place the patch files. It is recommended that you place these files in a completely separate folder from where you extracted the game, so as not to overwrite the original game files.

Once you have chosen an output folder, click the `Randomize!` button on the left. The randomizer will begin processing the game files and outputting the patch files. Depending on the settings you chose and how fast your computer's processor and hard drive are, this may take several minutes.

After the randomizer finishes, it will display a message indicating that the randomization is complete. You will find patched game files in the output folder you chose. You can use these game files for LayeredFS patching (e.g. with Luma3DS), or copy them into an extracted game folder to rebuild it back into a ROM.

# Features & Configuration
This randomizer tries to be very versatile while remaining easy to use. The following randomizer features are supported (configurable settings listed with each feature):

### General Features

- Quick Save/Load feature for settings
  - Quickly load your favorite randomization settings from a file, or send your settings to a friend for races/co-ops
- Outputs Luma-compatible Patches!
  - Play the randomized version of your game on any 3DS console capable of running the [Luma3DS](https://github.com/AuroraWright/Luma3DS) custom firmware!
  
### Randomization Features
  
- Pokémon Ability randomization
  - Randomize which abilities each species of Pokémon can have
  - Choose to prevent "Wonder Guard" to avoid situations where a Pokémon is unbeatable
- Pokémon Type randomization
  - Randomize which types each species of Pokémon is
  - Randomize primary, secondary, or both types of each species
- Egg move randomization
  - Randomize the selection of Egg Moves a Pokémon can inherit
  - Set the chance that a move matches the type of the Pokémon to increase odds of STAB (Same-Type Attack Bonus) moves
- Level-up learnset randomization
  - Randomize the selection of moves a Pokémon can learn by leveling up (which also affects the moves a wild Pokémon will know)
  - Set the chance that a move matches the type of the Pokémon to increase odds of STAB moves
  - Ensure that every Pokémon knows at least 4 moves (one of which will be an attack move, to avoid situations where the player is stuck without one)
  - Randomize the levels at which a Pokémon learns its moves
- Randomize Starter Pokémon selection
  - Includes the first starter selection at the beginning of the game, as well as any other situation in the game where the player gets to choose an additional starter (such as in Lumiose City at the Professor's lab).
  - Choose to limit the options to only Pokémon that are starters in at least one game in the series
  - Choose to limit the options to only Pokémon with Fire/Grass/Water as one of their types (which obeys randomized types if enabled!)
  - Choose whether or not to allow Legendary Pokémon as starter choices
- Randomize Trainer parties
  - Choose to have "rival" and any other "friend" trainers keep their starters (which evolve at the proper level along the game)
  - Give each trainer a type theme so every Pokémon on their party has that type as either its primary or secondary type (this also obeys randomized types if enabled)
  - Give a type theme to every Pokémon Gym in the region so that all trainers (plus the leader) in that Gym have Pokémon with one type in common (staying in line with how the games work prior to any modification)
  - Apply a level boost multiplier to every trainer's Pokémon to increase the difficulty of the game
- Randomize Wild Pokémon encounters
  - Choose whether or not to allow Legendary Pokémon to appear in the wild
  - Give a type theme to each region (or sub-region) of the game so all Pokémon in that region share a type
  - Apply a level boost multiplier to every wild Pokémon to increase the difficulty of the game
- Randomize Item Pokéballs scattered about the world
  - Any item that can exist in the Items, Medicines, or TMs/HMs case can be found in a Pokéball!
  - Choose whether or not Master Balls can be found
  - Choose whether or not TMs can be found where they aren't supposed to be
  - Choose whether or not Mega Stones can be found in regular Pokéballs

# Screenshots
Splash Screen:
  
![Splash Screen](https://i.imgur.com/ow6v5un.png)

Pokémon Options:
  
![Pokémon Options](https://i.imgur.com/ALRr8un.png)

Trainer Options:
  
![Trainer Options](https://i.imgur.com/6cm8QLw.png)

Overworld Options:
  
![Overworld Options](https://i.imgur.com/jPW39Kk.png)
