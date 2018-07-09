

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


public enum BuffType
{
    BUFF,
    DOT,
    HOT,
}

public enum TargetType
{
    SELF,//仅对自己
    ENEMY,//敌人
    FRIEND,//我方其他角色
    OUR,//我方任意角色
}

public enum SwitchRule
{
    NOT,//无法替换
    RESET,//刷新
    SWITCH,//顶替
}


public enum SkillType
{
    PASSIVE,
    HALO,
    ACTIVE,
    COMBO,
}