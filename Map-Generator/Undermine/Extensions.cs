namespace Map_Generator.Undermine;

public static class Extensions
{
    public static unsafe int MyGetHashCode(this string str)
    {
        fixed (char* chPtr1 = str)
        {
            int num1 = 5381;
            int num2 = num1;
            int num3;
            for (char* chPtr2 = chPtr1; (num3 = (int)*chPtr2) != 0; chPtr2 += 2)
            {
                num1 = (num1 << 5) + num1 ^ num3;
                int num4 = (int)chPtr2[1];
                if (num4 != 0)
                    num2 = (num2 << 5) + num2 ^ num4;
                else
                    break;
            }

            return num1 + num2 * 1566083941;
        }
    }
}