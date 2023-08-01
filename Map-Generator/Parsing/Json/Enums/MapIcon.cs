using System.Drawing;
using System.IO;
using Map_Generator.Parsing.Json.Classes;

namespace Map_Generator.Parsing.Json.Enums;

public static class MapIconExtension
{
    public static Image? GetMapImage(MapIcon mapIcon)
    {
        if (mapIcon == MapIcon.None) return null;

        string iconFileName = mapIcon.ToString() + ".png";
        string iconFilePath = Path.Combine(PathHandler.MapPath, iconFileName);

        if (!File.Exists(iconFilePath))
            throw new FileNotFoundException($"Could not find icon file: {iconFilePath}");

        return Image.FromFile(iconFilePath);
    }

    public static Color AssignColor(RoomType room)
    {
        Color color = room.MapIcon switch
        {
            MapIcon.Begin => Color.DimGray,
            MapIcon.End => Color.DimGray,
            // MapIcon.Secret => Color.Yellow,
            MapIcon.Relic => Color.Blue,
            MapIcon.Shop => Color.Yellow,
            _ => Color.DarkGray
        };
        if (room.Encounter is { Door: Door.Hidden })
            color = Color.Black;


        return color;
    }
}

public enum MapIcon
{
    None = 0,
    Begin = 1,
    Hoodie = 2,
    Relic = 3,
    Chest = 4,
    Shop = 5,
    Secret = 6,
    Altar = 7,
    Blackrabbit = 8,
    RelicAltar = 9,
    Special = 10,
    Boss = 11,
    End = 12,
}