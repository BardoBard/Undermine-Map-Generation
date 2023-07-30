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

- check blackrabbit rooms in general (mine is good), I changed the json
- sandworm encounter check, when does it spawn?


- add requirements for maps
- dynamically update map, floor number and zone number (also requirements)

- core shop add rooms and default
- bog unlocked check
- extra shop rooms market baby door (-1?) halls
- extra shop rooms market baby sequence halls
- halls 4 assassin check max (for all assassins tbh)
- look at all the doors from encounters, they might not all be correct
- add enemies in json, not sure what I was thinking
- no need to do some dumb shit with floornumber == 4 && ishidden, just rename the maps



- **figure out when sprites are being used?!?!?!?**