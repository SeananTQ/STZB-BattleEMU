using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SkillState
{
    CAST,
    READY_1,   
    DAMAGE,

}


public class MyTools : MonoBehaviour
{
    public static MyTools ins { get; set; }

    public Text text;

    private string showString = "";

    private void Awake()
    {
        ins = this;
    }

    private void Update()
    {

    }


    public void ttt(string str)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        showString += str + "\n";

        text.text = showString;
    }




    public void showSkill(Army army,Skill skill,SkillState state)
    {



        string temp="";
        if (army == null)
        {
  
            switch (state)
            {
                case SkillState.READY_1:
                     temp = "【军士】" + "的战法【" + skill.name + "】开始准备！";
                    break;

                case SkillState.CAST:
                    temp = "【军士】" + "发动【" + skill.name + "】！";
                    break;

                case SkillState.DAMAGE:
                    temp = "\t\t造成了" + "百分之" + skill.curDamge + "的伤害("+ skill .curTrunTargetCount+ "个目标)";
                    break;

                 
            }

        }
        else
        {

        }

        ttt(temp);
    }

    public void BattleEnd(int num)
    {
        string temp = "\n<color=" + GameConst.Color.lightColor + "____________________第" + num + "场战斗结束____________________</color>\n";

        ttt(temp);
    }

    public void BattleBegin(int num)
    {
        string temp = "\n\t\t\t<color=" + GameConst.Color.lightColor + "__________第" + num + "场战斗开始__________</color>\n";
        ttt(temp);
    }

    public void Trun(int num)
    {
        string temp="\n\t\t\t<color=" + GameConst.Color.lightColor + "__________第" + num + "回合__________</color>";

        ttt(temp);
    }


    public static void ppp(string str)
    {

        print(">>> " + str);
    }


}



