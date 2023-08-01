using System.Collections.Generic;
using System.Linq;
using Map_Generator.Math;

namespace Map_Generator;

public class Grid
{
    public List<GridSquare> GridSquares { get; private set; }
    public const int CellSize = 40;
    public const int GapSize = 10;
    public int GridWidth => 5;
    public int GridHeight => 5;
    public int Width { get; set; } = 0;
    public int Height { get; set; } = 0;
    public Vector2Int GridTranslation { get; private set; }

    public Grid(int width, int height, List<GridSquare> gridSquares, Vector2Int? gridTranslation = null)
    {
        GridTranslation = gridTranslation ?? Vector2Int.One;
        this.GridSquares = gridSquares;
        this.Width = width;
        this.Height = height;
    }
}