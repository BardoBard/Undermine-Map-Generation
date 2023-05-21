namespace Map_Generator.Math;

public class Vector2
{
    public static readonly Vector2 Zero = new (0, 0);
    public static readonly Vector2 One = new (1, 1);
    public float x = 0;
    public float y = 0;

    public Vector2()
    {
        
    }
    public Vector2(float y, float x)
    {
        this.y = y;
        this.x = x;
    }
}