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
    public static int currentShowIndex = 0;

    public Text text;

    private string showString;
    private List<string> showStringList;



    private void Awake()
    {
        ins = this;
        showStringList = new List<string>();
    }

    private void Update()
    {

    }


    public void Append(string str)
    {

        showString += str + "\n";


    }

    public void Insert(string str)
    {
        showStringList[0] = str + "\n" + showStringList[0];
    }

    public void NextBattle()
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }

        showStringList.Add(showString);
        GameConst.curShowTextConut = showStringList.Count;
        showString = null;
    }


    public void AllShow()
    {
        //text.text = showString.Substring(0,Mathf.Min(16383,showString.Length));

        if (currentShowIndex > showStringList.Count - 1)
        {
            currentShowIndex = showStringList.Count - 1;
        }

        text.text = showStringList[currentShowIndex];
    }

    public void NextIndex()
    {
        currentShowIndex++;
        AllShow();
    }

    public void LastIndex()
    {
        currentShowIndex--;
        if (currentShowIndex < 0)
        {
            currentShowIndex = 0;
        }
        AllShow();
    }

    public void FirstIndex()
    {
        currentShowIndex = 0;
        AllShow();
    }


    public void ShowSkill(Army army, Skill skill, SkillState state)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }


        string temp = "";

        switch (state)
        {
            case SkillState.READY_1:
                temp = "<color=" + GameConst.Color.greenColor + "【" + army.name + "】</color>的战法【" + skill.name + "】开始准备！";
                break;

            case SkillState.INSTANT_CAST:
                temp = "<color=" + GameConst.Color.greenColor + "【" + army.name + "】</color>发动【" + skill.name + "】！";
                break;

            case SkillState.DAMAGE:
                temp = "\t\t造成了" + "百分之" + skill.curDamge + "的伤害(" + skill.curTrunTargetCount + "个目标)";
                break;


        }

        Append(temp);
    }


    public void ShowHurt(Army army, float damage)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }

        string temp = "\t\t" + "<color=" + GameConst.Color.redColor + "【" + army.name + "】</color>损失了" + "<color=" + GameConst.Color.yellowColor + damage.ToString("0.0") + "</color>" + "兵力 (" + army.getCount().ToString("0") + ")";

        Append(temp);
    }

    public void ShowDotHurt(Army defender, DotBuff buff)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        string temp = "" + "<color=" + GameConst.Color.redColor + "【" + defender.name + "】 </color>由于" + buff.skill.army.name +
            "【" + buff.skill.name + "】施加的" + buff.effectName + "效果损失了<color=" + GameConst.Color.yellowColor + buff.dotDamage.ToString("0.0") +
            "</color>" + "兵力 (" + defender.getCount().ToString("0") + ") [剩" + buff.currentRound + "回合]";
        Append(temp);
    }

    public void ShowHitBuff(Army army, BuffClass buff, int type)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        string temp = "";
        switch (type)
        {
            case 0:
                temp = "\t\t" + "<color=" + GameConst.Color.redColor + "【" + army.name + "】</color>的" + "<color=" + GameConst.Color.yellowColor +
                        buff.effectName + "</color>" + "效果无法施加，存在一个相同或更强的效果" ;
                break;
            case 1:
                temp = "\t\t" + "<color=" + GameConst.Color.redColor + "【" + army.name + "】</color>的" + "<color=" + GameConst.Color.yellowColor +
                        buff.effectName + "</color>" + "效果已施加 (来自 " + buff.skill.name + " )";
                break;
            case 2:
                temp = "\t\t" + "<color=" + GameConst.Color.redColor + "【" + army.name + "】</color>的" + "<color=" + GameConst.Color.yellowColor +
                        buff.effectName + "</color>" + "效果被刷新了！ (来自 " + buff.skill.name + " )";
                break;
            case 3:

                break;
        }



        Append(temp);
    }

    public void ShowRemoveBuff(Army army, BuffClass buff)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }

        string temp = "";

        switch (buff.buffType)
        {
            case BuffType.DOT:

                temp = "" + "<color=" + GameConst.Color.redColor + "【" + army.name + "】 </color>的来自" + "<color=" + GameConst.Color.greenColor + buff.skill.army.name + "</color>【" + buff.skill.name + "】的" + buff.effectName + "效果消失了";
                break;

        }

        Append(temp);
    }


    public void BattleEnd(int num)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        string temp = "\n<color=" + GameConst.Color.lightColor + "____________________第 " + num + " 场战斗结束____________________</color>\n";

        Append(temp);
    }

    public void BattleBegin(int num)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        string temp = "\n<color=" + GameConst.Color.lightColor + "############第 " + num + " 场战斗开始#############</color>\n";
        Append(temp);
    }

    public void ShowTrun(int num)
    {
        if (GameConst.CanShowText() == false)
        {
            return;
        }
        string temp = "\n\t\t\t<color=" + GameConst.Color.lightColor + "__________第 " + num + " 回合__________</color>";

        Append(temp);
    }

    public void ShowSkillAverageDamage(Army army, float testCount, Skill skill, float averageDamage)
    {

        string temp = "" + "<color=" + GameConst.Color.greenColor + army.name + " </color>的技能【" + skill.name + "】平均施放了" + "<color=" + GameConst.Color.yellowColor + (skill.totalCastCount / testCount).ToString("0.00") + "</color>次，平均造成了" + "<color=" + GameConst.Color.yellowColor + (averageDamage / testCount).ToString("0.00") + "</color>点伤害";

        Insert(temp);



        Regex aa = new Regex(@"<.+?>");

        string str2 = aa.Replace(temp, "");


        ppp(str2);
    }


    public static void ppp(string str)
    {

        print(">>> " + str);
    }


    public float getRandom(float min, float max)
    {
        float rand = Random.Range(min, max + 1);


        return rand;
    }


}



