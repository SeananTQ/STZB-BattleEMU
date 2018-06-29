
using System;
using UnityEngine;

[Serializable]
public class Army
{
    public string name;
    public float atk;
    public float def;
    public float count;
    public string color;

    public Army(string name, float atk, float def, float count)
    {
        this.name = "【" + name + "】";
        this.atk = atk;
        this.def = def;
        this.count = count;

    }

    public bool isLife
    {
        get
        {

            if (count < 1)

                return false;
            else
                return true;
        }
    }

    public float getDamage(float atk, float def, float armyCount, float rate)
    {
        float result;




        // result = 4.8f * Mathf.Sqrt(armyCount) * ((200+atk) / (200 + def)) * rate;

        result = 4.8f * (Mathf.Sqrt(armyCount) + 2.1f) * ((200 + atk) / (200 + atk + def)) * rate;

        //  float result = 4.2f * (Mathf.Sqrt(armyCount) + 2.1f) * ((1+atk) / (1+atk + def)) * rate;

        //   result = 4.7f * (Mathf.Sqrt(armyCount+10)+1f) * (( atk) / ( atk + def)) * rate;

        return result;
    }

    public string Attack(Army bbb)
    {
        float tempDamage = getDamage(this.atk, bbb.def, this.count, 1f);


        return bbb.hurt(tempDamage).ToString("0.0");
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
