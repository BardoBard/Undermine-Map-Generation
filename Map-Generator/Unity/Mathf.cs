namespace Map_Generator.Unity;

public static class Mathf
{
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }

    public static float Abs(float f) => System.Math.Abs(f);
    public static int Abs(int value) => System.Math.Abs(value);
}