using System.IO;
using Map_Generator.Parsing;
using Map_Generator.Parsing.Json;
using Newtonsoft.Json;

namespace Map_Generator.UserControls.CreateTests;

public static class RoomsDecoder
{
    public static void WriteRooms()
    {
        var outputPath = PathHandler.TestsPath;

        var outputString = JsonConvert.SerializeObject(Program.PositionedRooms, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        });
        File.WriteAllText(outputPath + "test2.json", outputString);
    }

    public static void LoadRooms(int saveNumber)
    {
        Program.Start(Path.Combine(PathHandler.UndermineSavePath, @$"Save{saveNumber}.json"));
    }
}