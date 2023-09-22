using System.Drawing;
using System.IO;

namespace Map_Generator.Parsing.Json.Enums
{
    public enum ItemIcon
    {
        None = 0,

        //goldmine
        Barrel01 = 1,
        BarrelExploding01 = 2,
        Crate01 = 3,
        Crate02 = 4,
        RockGold01 = 5,
        OreWall01 = 6,
        Plant01 = 7,
        SkeletonSpawner = 8,
        CrawlSpace = 9,
    }

    public static class ItemIconExtension
    {
        public static Image? GetEnemyImage(this ItemIcon icon)
        {
            if (icon == ItemIcon.None) return null;

            string iconFileName = icon.ToString() + ".png";
            string iconFilePath = Path.Combine(PathHandler.ItemPath, iconFileName);

            if (!File.Exists(iconFilePath))
                throw new FileNotFoundException($"Could not find icon file: {iconFilePath}");

            return Image.FromFile(iconFilePath);
        }
    }
}