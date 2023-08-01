```
██╗   ██╗███╗   ██╗██████╗ ███████╗██████╗ ███╗   ███╗██╗███╗   ██╗███████╗     ███╗   ███╗ █████╗   ███████╗
██║   ██║████╗  ██║██╔══██╗██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔════╝     ████╗ ████║ ██╔══██╗ ██╔══██╗
██║   ██║██╔██╗ ██║██║  ██║█████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║█████╗       ██╔████╔██║ ███████║ ██████╔╝
██║   ██║██║╚██╗██║██║  ██║██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══╝       ██║╚██╔╝██║ ██╔══██║ ██╔═══╝
╚██████╔╝██║ ╚████║██████╔╝███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██████╗      ██║ ╚═╝ ██║ ██║  ██║ ██║
 ╚════╝  ╚═╝  ╚═══╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═════╝      ╚═╝     ╚═╝ ╚═╝  ╚═╝ ╚═╝
```
## Undermine Map Generator

This is a map generator for the game Undermine. It is written in C# desktop application.
It simulates the game's map generation algorithm and returns the fastest possible path to the end of the game, given
specific parameters.

Used for the Undermine speedrunning community.

### Packages

- newtonsoft.json v13.0.3, net472

## todo list

- sandworm encounter check, when does it spawn?

- dynamically update floor number and zone number (also requirements)

- halls 4 assassin check max (for all assassins tbh)
- look at all the doors from encounters, they might not all be correct
- add enemies in json, not sure what I was thinking
- no need to do some dumb shit with floornumber == 4 && ishidden, just rename the maps
- noexit check

- **figure out when rooms bug rooms happen, dungeon 1 hoody is one example**
- **figure out when sprites are being used?!?!?!?**