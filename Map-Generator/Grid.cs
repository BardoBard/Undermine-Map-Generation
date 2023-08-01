using System.Collections.Generic;
using Map_Generator.Math;

namespace Map_Generator;

public class Grid
{
    public int Size = 32;
    public int Height = 72;
    public int Width = 32;
    public float CellSzie = 0.4f;
    // public Vector2Int LocalOrigin = new(-14.2, -7); //TODO: check if it's woth having multiple types of Vector2
    public Vector2Int Origin = new();
    public List<Vector2Int> Coordinates;
    public List<Grid> SubGrids;
    
    public class GridMap
    {
        public Vector2Int size = Vector2Int.One;
    }


    public Grid()
    {
        
    }
    public Grid(string stage)
    {
        switch (stage)
        {
            case "small":
                Height = 11;
                Width = 20;
                break;
            case "extra":
                Height = 11;
                Width = 20;
                break;
            case "large":
                Height = 13;
                Height = 20;
                break;
        }
    }
}