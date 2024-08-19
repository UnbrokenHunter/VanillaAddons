# VanillaAddons - Lethal Company Mod

## Overview

VanillaAddons is a mod for the game _Lethal Company_ that was made to maintain the feel of the 'vanilla' game, while adding quality of life enhancements and vanilla-esk features. Currently, this mod enhances the terminal with various commands, making the gameplay more interactive and providing additional information and functionalities.

## Table of Contents

- [Features](#features)
  - [Terminal Commands](#terminal-commands)
    - [General Commands](#general-commands)
    - [Ship Control Commands](#ship-control-commands)
    - [File Management Commands](#file-management-commands)
    - [Terminal Behavior Modifications](#terminal-behavior-modifications)
- [Installation](#installation)
- [Compatibility](#compatibility)
- [Notes](#notes)
- [Credits](#credits)
- [Thank You](#thank-you)

## Features

### Terminal Commands

#### General Commands

1. **ship**

   - **Category**: Other
   - **Description**: Displays the total number of scrap items currently on the ship and their combined value.
   - **Usage**: `ship`

2. **neofetch**

   - **Category**: Other
   - **Description**: Runs a custom Neofetch command, displaying system information like OS, uptime, package count, and a set of color blocks.
   - **Usage**: `neofetch`

3. **clear**

   - **Category**: Other
   - **Description**: Clears the terminal screen.
   - **Usage**: `clear`

4. **time**
   - **Category**: Other
   - **Description**: Displays the current time if you are on a moon. If not, informs you that time is irrelevant in the current location.
   - **Usage**: `time`

#### Ship Control Commands

1. **door**

   - **Category**: Ship
   - **Description**: Opens or closes the ship door.
   - **Usage**: `door`

2. **lights**

   - **Category**: Ship
   - **Description**: Toggles the ship's lights on or off.
   - **Usage**: `lights`

3. **tp**

   - **Category**: Ship
   - **Description**: Teleports the selected player back to the ship.
   - **Usage**: `tp`

4. **itp**
   - **Category**: Ship
   - **Description**: Activates the Inverse Teleporter.
   - **Usage**: `itp`

#### File Management Commands

1. **touch**

   - **Description**: Creates a new file in the terminal’s directory.
   - **Usage**: `touch <filename>`

2. **cat**

   - **Description**: Displays the contents of a specified file.
   - **Usage**: `cat <filename>`

3. **rm**

   - **Description**: Deletes a specified file.
   - **Usage**: `rm <filename>`

4. **nano**

   - **Description**: Opens a file in the nano text editor within the terminal, allowing you to edit its contents.
   - **Usage**: `nano <filename>`

5. **gpg**

   - **Description**: Encrypts or decrypts files with a password.
   - **Usage**:
     - Encryption: `gpg -c --passphrase <password> <filename>`
     - Decryption: `gpg --passphrase <password> <filename>`

6. **ls**

   - **Description**: Lists all files currently available in the terminal’s directory.
   - **Usage**: `ls`

7. **whoami**
   - **Description**: Displays the username of the current terminal user.
   - **Usage**: `whoami`

#### Terminal Behavior Modifications

- **Terminal Screen Enabling**: Allows the player to interact with the terminal immediately after opening, removing the delay.
- **Default Terminal Screen**: By default, the terminal will display Neofetch.

## Installation

1. Install BepInEx and Harmony for Unity.
2. Download the VanillaAddons mod.
3. Place the mod's files into the BepInEx `plugins` directory.
4. Launch _Lethal Company_ to activate the mod.

## Compatibility

- **Requires**: TerminalAPI v1.5.0, HookGenPatcher v0.0.5
- **Incompatible**: May not be compatible with LethalBang, BetterTerminal, MoreTerminalCommands, and Neofetch, due to very similar features.

## Notes

- The mod adds extra commands that blend seamlessly with existing terminal commands.

## Credits

- Developed by unbrokenhunter as part of my VanillaAddons series for _Lethal Company_.

## Thank You

Many of this mod's terminal commands were inspired by other mods. I want to credit and thank all of these developers.

- Thank you twi, for the clear command in [LethalBang](https://thunderstore.io/c/lethal-company/p/twi/LethalBang/)
- Thank you rooni, for removing the delay when opening the terminal in [BetterTerminal](https://thunderstore.io/c/lethal-company/p/rooni/BetterTerminal/)
- Thank you NavarroTech, for the ship item commands (lights, doors, and teleporters) in [MoreTerminalCommands](https://thunderstore.io/c/lethal-company/p/NavarroTech/MoreTerminalCommands/)
- Thank you BeansDev, for adding Neofetch to the terminal in [Neofetch](https://thunderstore.io/c/lethal-company/p/BeansDev/Neofetch/)
