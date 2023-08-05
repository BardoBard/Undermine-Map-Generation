using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Map_Generator.Parsing.Json.Enums
{
    public enum EnemyIcon
    {
        None = 0,
        Assassin = 1,
        Bat = 2,
        Bingbong = 3,
        Bishfish = 4,
        Bobo = 5,
        Buglightning = 6,
        Cauldron = 7,
        Churchbell = 8,
        Corruptedpilfer = 9,
        Corruptedpilferboss = 10,
        Crossbowman = 11,
        Crystalant = 12,
        Crysalfly = 13,
        Crystalspider = 14,
        Crystalwitch = 15,
        Cursedjar = 16,
        Cursedjarwisp = 17,
        Firebat = 18,
        Firewitch = 19,
        Flybloat = 20,
        Flyranged = 21,
        Flyranged02 = 22,
        Flyranged03 = 23,
        Footman = 24,
        Gargoyle = 25,
        Glimmerweed = 27,
        Goblinengineer = 28,
        Goblinpoisonengineer = 29,
        Goblintickerengineer = 30,
        Golem = 31,
        Gremlin = 32,
        Gremlinfiery = 33,
        Highpriest = 34,
        Hobbledoll = 35,
        Jumperfire = 36,
        Jumperfirelarge = 37,
        Jumperoil = 38,
        Jumperoillarge = 39,
        Jumperwater = 40,
        Jumperwaterlarge = 41,
        Lurkerbasic = 42,
        Lurkercrypt = 43,
        Lurkercrystal = 44,
        Magmawormhead = 45,
        Mimicchest = 46,
        Necromancer = 50,
        Nemesis = 51,
        Ogreannihilator = 52,
        Ogrebombardier = 53,
        Ogrefirebomber = 54,
        Pilferbasic = 55,
        Pilferbomb = 56,
        Pilferfat = 58,
        Pilfergolden = 59,
        Pilferhunter = 60,
        Poisonwormhead = 61,
        Priest = 63,
        Priestinnerfire = 64,
        Queenfish = 65,
        Quilledgolem = 66,
        Ratbasic = 67,
        Ratexploding = 68,
        Ratlarge = 69,
        Ratnest = 70,
        Rattough = 71,
        Rookfish = 72,
        Sandwormlarva = 73,
        Scaletunnel = 74,
        Shaman = 75,
        Shaman02 = 76,
        Shaman03 = 77,
        Skeletonenemycrimson = 78,
        Skeletonenemynorm = 79,
        Spiderbasic = 80,
        Spiderwolf = 81,
        Throwbo = 82,
        Ticker = 83,
        Trollbombardier = 84,
        Voodoodoll = 85,
    }

    public static class EnemyIconExtension
    {
        public static Image? GetEnemyImage(this EnemyIcon icon)
        {
            if (icon == EnemyIcon.None) return null;

            string iconFileName = icon.ToString() + ".png";
            string iconFilePath = Path.Combine(PathHandler.EnemyPath, iconFileName);

            if (!File.Exists(iconFilePath))
                throw new FileNotFoundException($"Could not find icon file: {iconFilePath}");

            return Image.FromFile(iconFilePath);
        }
    }
}