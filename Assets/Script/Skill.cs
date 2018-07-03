using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public string name;
    public float castRate;

    public int targetLowerCount = 1;
    public int targetUpperCount = 1;

    public int curTrunTargetCount = 1;
    public int readyTime;
    private int curTime = 0;

    public float damageRate = 100;

    public float curDamge = 0;
    private float totalDamage = 0;
    private float totalCastCount = 0;

    public Skill(string name, float castRate, int readyTime)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;
    }

    public Skill(string name, float castRate, int readyTime, int targetCount, float damage)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;
        this.targetLowerCount = targetCount / 10;
        this.targetUpperCount = targetCount % 10;
        this.damageRate = damage;
    }



    public bool isReady()
    {

        if (curTime == 0)
        {
            return true;
        }
        else
        {
            curTime--;
            return false;
        }

    }

    public void Clear()
    {
        totalDamage = 0;
        totalCastCount = 0;
    }

    public void Casting()
    {


    }


    public bool IsCast()
    {
        float rand;
        //先判断是否为需要蓄力的法术
        if (readyTime > 0)
        {
            //判断是否正在蓄力
            if (curTime > 0)
            {
                //减少1回合蓄力时间
                curTime--;
                if (curTime == 0)
                {
                    //立即施法
                    MyTools.ins.showSkill(null, this, SkillState.CAST);
                    curDamge= getDamage();
                    totalDamage += curDamge;
                    MyTools.ins.showSkill(null, this, SkillState.DAMAGE);
                    totalCastCount++;
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                //没有在蓄力
                rand = Random.Range(0, 100);
                //判断随机施法成功
                if (rand < castRate)
                {
                    //开始蓄力
                    curTime = readyTime;
                    MyTools.ins.showSkill(null, this, SkillState.READY_1);
                    return false;
                }
                else
                {
                    return false;
                }

            }
        }
        else
        {

            //不是需要蓄力的法术

            //判断是否随机成功
            rand = Random.Range(0, 100);
            if (rand < castRate)
            {
                //立即施法
                MyTools.ins.showSkill(null, this, SkillState.CAST);
                curDamge = getDamage();
                totalDamage += curDamge;
                MyTools.ins.showSkill(null, this, SkillState.DAMAGE);


                totalCastCount++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int TargetCount()
    {
        if (targetUpperCount == 1)
        {
            return 1;
        }
        else
        {
            int temp = Random.Range(targetLowerCount, targetUpperCount + 1);

            return temp;
        }
    }


    private float getDamage()
    {
        float damge;

        curTrunTargetCount = TargetCount();
        damge = curTrunTargetCount * damageRate;


        return damge;
    }

    public float getTotalDamage()
    {
        return totalDamage;
    }

}
