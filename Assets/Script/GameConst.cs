

public static class GameConst
{
    public static int curShowTextConut=0;
    public static int maxShowTextConut = 5;

    public static bool CanShowText()
    {
        if (curShowTextConut < maxShowTextConut)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static class Color
    {
        public static string redColor = "#FF786E>";
        public static string greenColor = "#6EFF90>";
        public static string yellowColor = "#FFC700>";
        public static string lightColor = "#FFEC9C>";

    }


}