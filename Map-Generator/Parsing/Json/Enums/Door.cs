using System.Drawing;
using System.IO;
using Map_Generator.Parsing.Json.Classes;

namespace Map_Generator.Parsing.Json.Enums;

public enum Door
{
    None = 0,
    Normal = 1,
    Iron = 2,
    Rock = 3,
    Unused = 4,
    Locked = 5,
    Secret = 6,
    Hidden = 7,
    Crystal = 8
}

public static class DoorExtension
{
    public static Image? GetDoorImage(this Door door)
    {
        if (door is Door.None or Door.Normal or Door.Secret or Door.Unused or Door.Hidden) return null;

        string iconFileName = door.ToString() + ".png";
        string iconFilePath = Path.Combine(PathHandler.DoorPath, iconFileName);

        if (!File.Exists(iconFilePath))
            throw new FileNotFoundException($"Could not find icon file: {iconFilePath}");

        return Image.FromFile(iconFilePath);
    }

    public static Image? GetDoorImage(this Door door, RoomType neighborRoom)
    {
        switch (door)
        {
            case Door.None or Door.Normal or Door.Secret or Door.Unused or Door.Hidden:
                return null;
            case Door.Locked when neighborRoom.Encounter is { Door: Door.Secret }:
                return null;
        }

        string iconFileName = door.ToString() + ".png";
        string iconFilePath = Path.Combine(PathHandler.DoorPath, iconFileName);

        if (!File.Exists(iconFilePath))
            throw new FileNotFoundException($"Could not find icon file: {iconFilePath}");

        return Image.FromFile(iconFilePath);
    }
}