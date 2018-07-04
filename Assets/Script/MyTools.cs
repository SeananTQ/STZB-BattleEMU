using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public enum SkillState
{
    MISS,//没有成功施放
    INSTANT_CAST,//立即施放
    READY_1,   
    DAMAGE,//结算伤害
    
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


    public void Append(string str)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        showString += str + "\n";


    }

    public void Insert(string str)
    {
        showString = str + "\n"+ showString;


    }

    public void AllShow()
    {

        text.text = showString;


    }


    public void ShowSkill(Army army,Skill skill,SkillState state)
    {



        string temp="";
   
            switch (state)
            {
                case SkillState.READY_1:
                     temp = "<color=" + GameConst.Color.greenColor+army.name+ "</color>的战法【" + skill.name + "】开始准备！";
                    break;

                case SkillState.INSTANT_CAST:
                    temp = "<color=" + GameConst.Color.greenColor + army.name + "</color>发动【" + skill.name + "】！";
                    break;

                case SkillState.DAMAGE:
                    temp = "\t\t造成了" + "百分之" + skill.curDamge + "的伤害("+ skill .curTrunTargetCount+ "个目标)";
                    break;

                 
            }

        Append(temp);
    }


    public void ShowHurt(Army army,float damage)
    {
        string temp ="\t\t" + "<color=" + GameConst.Color.redColor  + army.name + " </color>损失" + "<color=" + GameConst.Color.yellowColor +  damage.ToString("0.0") + "</color>" + "兵力(" + army.getCount().ToString("0") + ")";

        Append(temp);
    }

    public void BattleEnd(int num)
    {
        string temp = "\n<color=" + GameConst.Color.lightColor + "____________________第 " + num + " 场战斗结束____________________</color>\n";

        Append(temp);
    }

    public void BattleBegin(int num)
    {
        string temp = "\n<color=" + GameConst.Color.lightColor + "############第 " + num + " 场战斗开始#############</color>\n";
        Append(temp);
    }

    public void ShowTrun(int num)
    {
        string temp="\n\t\t\t<color=" + GameConst.Color.lightColor + "__________第 " + num + " 回合__________</color>";

        Append(temp);
    }

    public void ShowSkillAverageDamage(Army army,float testCount, Skill skill,float averageDamage)
    {

        string temp = "" + "<color=" + GameConst.Color.greenColor + army.name + " </color>的技能【" + skill.name + "】平均施放了"+ "<color=" + GameConst.Color.yellowColor + (skill.totalCastCount/testCount).ToString("0.00")+"</color>次，平均造成了" + "<color=" + GameConst.Color.yellowColor + (averageDamage/ testCount).ToString("0.00") + "</color>点伤害";

        Insert(temp);

        //new System.Text.RegularExpressions.Regex("<color=*>").Replace(temp,"");

        System.Text.RegularExpressions.Regex aa = new System.Text.RegularExpressions.Regex("c.r");
        string str2 = aa.Replace(temp, "");

        //string input = "1851 1999 1950 1905 2003";
        //string pattern = @"(?<=19)\d{2}\b";

        //Regex.re

        //foreach (Match match in Regex.Matches(input, pattern))
        //    ppp(match.Value);

        ppp(str2);
    }


    public static void ppp(string str)
    { 

        print(">>> " + str);
    }


    public float getRandom(float min,float max)
    {
        float rand = Random.Range(min, max+1);


        return rand;
    }
     
}



