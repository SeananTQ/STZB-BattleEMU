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

        Battle(green, red);

        text.text = showText;

    }



    public void ttt(string str)
    {
        showText += str + "\n";

      //  print(">>> " + str);

    }

    public void Battle(Army mGreen, Army mRed)
    {
        ttt("\n\t\t\t<color="+lightColor+"__________战斗开始__________</color>\n");
        int tempTrun = 1;
        while (mGreen.isLife && mRed.isLife)
        {
            ttt("\n\t\t\t<color="+lightColor+"__________第" + tempTrun + "回合__________</color>\n");
            tempTrun++;

            ttt("<color=" + mGreen.color + ">" + mGreen.name + "</color>对" + "<color=" + mRed.color + ">" + mRed.name + "</color>" + "发动普通攻击");
            ttt("\t" + "<color=" + mRed.color + ">" + mRed.name + " </color>损失" + "<color=" + yellowColor + ">" + mGreen.Attack(mRed) + "</color>" + "兵力(" + mRed.getCount().ToString("0.0") + ")");

            if (mRed.isLife == false)
            {
                break;
            }

            ttt("<color=" + mRed.color + ">" + mRed.name + "</color>对" + "<color=" + mGreen.color + ">" + mGreen.name + "</color>" + "发动普通攻击");
            ttt("\t" + "<color=" + mGreen.color + ">" + mGreen.name + " </color>损失" + "<color=" + yellowColor + ">" + mRed.Attack(mGreen) + "</color>" + "兵力(" + mGreen.getCount().ToString("0.0") + ")");


        }

        Army tempArmy;

        if (mGreen.isLife)
        {
            tempArmy = mRed;
        }
        else
        {
            tempArmy = mGreen;
        }


        ttt("\t" + "<color=" + tempArmy.color + ">" + tempArmy.name + " </color>无法再战");
        ttt("\n\t\t\t__________战斗结束__________");
    }




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

[Serializable]
public class Army
{
    public string name;
    public float atk;
    public float def;
    public float count;
    public string color;

    public Army(string name,float atk, float def, float count)
    {
        this.name ="【" + name+"】";
        this.atk = atk;
        this.def = def;
        this.count = count;

    }

    public bool isLife {
        get {

            if (count < 1)

                return false;
            else
                return true;
        }
    }

    public float getDamage(float atk, float def, float armyCount, float rate)
    {
        float result;




        //float result = 5.2f * Mathf.Sqrt(armyCount) * ((atk) / (atk + def)) * rate;

        //  float result = 4.2f *( Mathf.Sqrt(armyCount) +2.1f)* ((atk) / (atk + def)) * rate;

        //  float result = 4.2f * (Mathf.Sqrt(armyCount) + 2.1f) * ((1+atk) / (1+atk + def)) * rate;

         result = 4.7f * (Mathf.Sqrt(armyCount+10)+1f) * (( atk) / ( atk + def)) * rate;

        return result;
    }

    public string Attack( Army bbb)
    {
        float tempDamage = getDamage(this.atk, bbb.def, this.count, 1f);
        

        return   bbb.hurt(tempDamage).ToString("0.0");
    }

    public float hurt(float damage)
    {

        float tempCount = count;
       
       // damage =    (float)  Math.Round(Convert.ToDouble(damage), MidpointRounding.AwayFromZero);
     


        count -= damage;
        count = Mathf.Max(0, count);

        if (count > 0)
        {
            return damage;
        }
        else
            return tempCount;
    }

    public float getCount()
    {
        //  Debug.Log(count+"     "+Mathf.Round(count));
        //return Mathf.Round(count);
        return count;
    }

}







public class Skill
{
    public float spellRate;

}


public class Buff
{
    public int round;

}
