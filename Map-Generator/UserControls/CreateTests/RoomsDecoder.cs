using System.IO;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json;
using Newtonsoft.Json;

namespace Map_Generator.UserControls.CreateTests;

public static class RoomsDecoder
{
    public static void WriteRooms()
    {
        var outputPath = PathHandler.TestsPath + MapType.GetMapName() + "/" + "test2.json";

        var outputString = JsonConvert.SerializeObject(Program.PositionedRooms, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        });
        PathHandler.WriteAllToFile(outputPath, outputString);
    }

    public static void LoadRooms(int saveNumber)
    {
        Program.Start(PathHandler.SavePath(saveNumber));
    }
}