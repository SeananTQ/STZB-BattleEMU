using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackFun : MonoBehaviour
{
    public Text text; 
    public string showText="";

    [SerializeField]
    public Army green;

    [SerializeField]
    public Army red;

    private string redColor = "#FF786E";
    private string greenColor = "#6EFF90";
    private string yellowColor = "#FFC700";
    private string lightColor = "#FFEC9C>";

    // Use this for initialization
    void Start()
    {

        //Army green=new Army("军士",48+12,28+8,100);
        //Army red= new Army("军士",48 , 28 , 100);

        green.color = greenColor;
        red.color = redColor;

        //Battle(green, red);

        text.text = showText;

    }




    //public void Battle(Army mGreen, Army mRed)
    //{
    //   MyTools.ins.Append("\n\t\t\t<color="+lightColor+"__________战斗开始__________</color>\n");
    //    int tempTrun = 1;
    //    while (mGreen.isLife && mRed.isLife)
    //    {
    //        MyTools.ins.Append("\n\t\t\t<color="+lightColor+"__________第" + tempTrun + "回合__________</color>\n");
    //        tempTrun++;

    //        MyTools.ins.Append("<color=" + mGreen.color + ">" + mGreen.name + "</color>对" + "<color=" + mRed.color + ">" + mRed.name + "</color>" + "发动普通攻击");
    //        MyTools.ins.Append("\t" + "<color=" + mRed.color + ">" + mRed.name + " </color>损失" + "<color=" + yellowColor + ">" + mGreen.Attack(mRed) + "</color>" + "兵力(" + mRed.getCount().ToString("0.0") + ")");

    //        if (mRed.isLife == false)
    //        {
    //            break;
    //        }

    //        MyTools.ins.Append("<color=" + mRed.color + ">" + mRed.name + "</color>对" + "<color=" + mGreen.color + ">" + mGreen.name + "</color>" + "发动普通攻击");
    //        MyTools.ins.Append("\t" + "<color=" + mGreen.color + ">" + mGreen.name + " </color>损失" + "<color=" + yellowColor + ">" + mRed.Attack(mGreen) + "</color>" + "兵力(" + mGreen.getCount().ToString("0.0") + ")");


    //    }

    //    Army tempArmy;

    //    if (mGreen.isLife)
    //    {
    //        tempArmy = mRed;
    //    }
    //    else
    //    {
    //        tempArmy = mGreen;
    //    }


    //    MyTools.ins.Append("\t" + "<color=" + tempArmy.color + ">" + tempArmy.name + " </color>无法再战");
    //    MyTools.ins.Append("\n\t\t\t__________战斗结束__________");
    //}




    //public double getDamage(float atk, float def, float armyCount,float rate)
    //{
    //    double result;

    //    result = (oneArmyAttack + hundredArmyAttack + thousandArmyAttack + (atk - def) * 0.00005f) * armyCount;


    //    return result;
    //}

    //public double getDamage2(float atk, float def, float armyCount, float rate)
    //{
    //    // double result = 4.8f * Mathf.Sqrt(armyCount) * ((200 + atk) / (200 + def)) * rate;
    //    //if (armyCount < 67)
    //    //    armyCount = 67;

    //  double result =7f * Mathf.Sqrt(armyCount) * ((atk) / (atk+ def)) * rate;


    //    return result;
    //}

    //public double getDamage3(Army aaa, Army bbb)
    //{
    //    var temp = getDamage2(aaa.atk, bbb.def, aaa.count, 1f);
    //    return temp;
    // }

}






