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

# Screenshots
Splash Screen:
  
![Splash Screen](https://i.imgur.com/ow6v5un.png)

Pokémon Options:
  
![Pokémon Options](https://i.imgur.com/ALRr8un.png)

Trainer Options:
  
![Trainer Options](https://i.imgur.com/6cm8QLw.png)

Overworld Options:
  
![Overworld Options](https://i.imgur.com/jPW39Kk.png)
