using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackFun : MonoBehaviour
{
    public Text text; 
    public string showText="";

    private string redColor = "#FF786E";
    private string greenColor = "#6EFF90";
    private string yellowColor = "#FFC700";

    // Use this for initialization
    void Start()
    {

        Army green=new Army("军士",48+12,28+8,100);
        Army red= new Army("军士",48 , 28 , 100);

        green.color = greenColor;
        red.color = redColor;

        Battle(green, red);

        text.text = showText;

        //print(">>>小号打大号伤害: " + getDamage2(48+12, 28, 100, 1f));
        //print(">>>大号打小号伤害: " + getDamage2(48, 28+8, 67, 1f));
        //print(">>>小号打大号伤害: " + getDamage2(48 + 12, 28, 77, 1f));
        //print(">>>大号打小号伤害: " + getDamage2(48, 28 + 8, 37, 1f));
        //print(">>>小号打大号伤害: " + getDamage2(48 + 12, 28, 54, 1f));
        //print(">>>大号打小号伤害: " + getDamage2(48, 28 + 8, 11, 1f));

    }



    // Update is called once per frame
    void Update()
    {

    }


    public double oneArmyAttack = 1; //单兵伤害加成
    public double hundredArmyAttack = 1;//每百兵伤害加成
    public double thousandArmyAttack = 1;//每千兵伤害加成


    public void ttt(string str)
    {
        showText += str + "\n";

      //  print(">>> " + str);

    }



    public void Battle(Army aaa, Army bbb)
    {
        ttt("\n\t\t\t__________战斗开始__________\n");
        int tempTrun = 1;
        while (aaa.isLife && bbb.isLife)
        {
            ttt("\n\t\t\t__________第" + tempTrun + "回合__________\n");
            tempTrun++;

            ttt("<color=" + aaa.color + ">" + aaa.name + "</color>对" + "<color=" + bbb.color + ">" + bbb.name + "</color>" + "发动普通攻击");
            ttt("\t" + "<color=" + bbb.color + ">" + bbb.name + " </color>损失" + "<color=" + yellowColor + ">" + aaa.Attack(bbb) + "</color>" + "兵力(" + bbb.getCount() + ")");

            if (bbb.isLife == false)
            {
                break;
            }

            ttt("<color=" + bbb.color + ">" + bbb.name + "</color>对" + "<color=" + aaa.color + ">" + aaa.name + "</color>" + "发动普通攻击");
            ttt("\t" + "<color=" + aaa.color + ">" + aaa.name + " </color>损失" + "<color=" + yellowColor + ">" + bbb.Attack(aaa) + "</color>" + "兵力(" + aaa.getCount() + ")");


        }

        Army tempArmy;

        if (aaa.isLife)
        {
            tempArmy = bbb;
        }
        else
        {
            tempArmy = aaa;
        }


        ttt("\t" + "<color=" + tempArmy.color + ">" + tempArmy.name + " </color>无法再战");
        ttt("\n\t\t\t__________战斗结束__________");
    }




    public double getDamage(float atk, float def, float armyCount,float rate)
    {
        double result;

        result = (oneArmyAttack + hundredArmyAttack + thousandArmyAttack + (atk - def) * 0.00005f) * armyCount;


        return result;
    }

    public double getDamage2(float atk, float def, float armyCount, float rate)
    {
        // double result = 4.8f * Mathf.Sqrt(armyCount) * ((200 + atk) / (200 + def)) * rate;
        //if (armyCount < 67)
        //    armyCount = 67;

      double result =7f * Mathf.Sqrt(armyCount) * ((atk) / (atk+ def)) * rate;


        return result;
    }

    public double getDamage3(Army aaa, Army bbb)
    {
        var temp = getDamage2(aaa.atk, bbb.def, aaa.count, 1f);
        return temp;
     }

}


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
        // double result = 4.8f * Mathf.Sqrt(armyCount) * ((200 + atk) / (200 + def)) * rate;
        //if (armyCount < 67)
        //    armyCount = 67;

        float result = 5.2f * Mathf.Sqrt(armyCount) * ((atk) / (atk + def)) * rate;


        return result;
    }

    public float Attack( Army bbb)
    {
        float tempDamage = getDamage(this.atk, bbb.def, this.count, 1f);

 

        return   bbb.hurt(tempDamage);
    }

    public float hurt(float damage)
    {
        float tempCount = count;

        damage = Mathf.Round(damage);

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
