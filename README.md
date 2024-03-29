```
██╗   ██╗███╗   ██╗██████╗ ███████╗██████╗ ███╗   ███╗██╗███╗   ██╗███████╗     ███╗   ███╗ █████╗   ███████╗
██║   ██║████╗  ██║██╔══██╗██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔════╝     ████╗ ████║ ██╔══██╗ ██╔══██╗
██║   ██║██╔██╗ ██║██║  ██║█████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║█████╗       ██╔████╔██║ ███████║ ██████╔╝
██║   ██║██║╚██╗██║██║  ██║██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══╝       ██║╚██╔╝██║ ██╔══██║ ██╔═══╝
╚██████╔╝██║ ╚████║██████╔╝███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██████╗      ██║ ╚═╝ ██║ ██║  ██║ ██║
 ╚════╝  ╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═════╝      ╚═╝     ╚═╝ ╚═╝  ╚═╝ ╚═╝
```

![build workflow](https://github.com/Bardobard/Undermine-Map-Generation/actions/workflows/Testing.yml/badge.svg)

## Undermine Map Generator

This is a map generator for the game Undermine. It is written in C# desktop application.
It simulates the game's map generation algorithm and returns the fastest possible path to the end of the game, given
specific parameters.

Used for the Undermine speedrunning community.

## Download

Download [here](https://github.com/BardoBard/Undermine-Map-Generation/releases/latest), extract the zip file and run the
executable.

### Packages

- newtonsoft.json v13.0.3, net472
- (Testing) NUnit.3.5.0

## todo list

- sandworm encounter check, when does it spawn?

- dynamically update floor number and zone number (also requirements)

- look at all the doors from encounters, they might not all be correct
- no need to do some dumb shit with floornumber == 4 && ishidden, just rename the maps
- noexit check
- fix branchweight for encounters if possible
- A-Star (better pathfind algorithm)
- beforebogentrance branchweight
- change crawlspace, it's kinda shitty atm, from bool to list
- fix the layout pls, center it
- **figure out when sprites are being used?!?!?!?**
