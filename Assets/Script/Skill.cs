using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public string name;
    public float castRate;

    public int targetLowerCount = 1;
    public int targetUpperCount = 1;

    public int curTrunTargetCount = 1;//当前回合的目标舒朗
    public int readyTime;//技能需要准备的回合数
    private int curTime = 0;//当前需要准备的回合数

    public float damageRate = 100;//伤害率

    public float curDamge = 0;
    private float totalDamage = 0;
    public float totalCastCount = 0;

    public List<BuffClass> buffList;


    public Skill(string name, float castRate, int readyTime)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;
    }

    
    /// <summary>
    /// 技能构造函数
    /// </summary>
    /// <param name="name">技能名称</param>
    /// <param name="castRate">发动概率</param>
    /// <param name="readyTime">准备回合数</param>
    /// <param name="targetCount">可攻击目标数量，十位为下限，个位为上限</param>
    /// <param name="damage">技能伤害率</param>
    public Skill(string name, float castRate, int readyTime, int targetCount, float damage)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;
        this.targetLowerCount = targetCount / 10;
        this.targetUpperCount = targetCount % 10;
        this.damageRate = damage;

        buffList = new List<BuffClass>();
    }

    //重置状态，用于开始下一场战斗
    public void ResetData()
    {
        curTime = 0;
    }


    public void AddBuff(BuffClass buff)
    {
        buffList.Add(buff);

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




    //立即施法
    private void InstantCast(Army selfArmy, List<Army> enemyList)
    {
        MyTools.ins.ShowSkill(selfArmy, this, SkillState.INSTANT_CAST);

        //先随机能够攻击的目标数量
        curTrunTargetCount = RandTargetCount();
        //这里应当判断攻击距离TODO



        //然后判断该技能攻击目标的数量，3全打，1、2随机
        switch (curTrunTargetCount)
        {

            case 3:
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Hurt(selfArmy, this);
                }

                //此处应该还有判断DOT挂载，先省略
                break;

            case 1:
                int tempR=(int) MyTools.ins.getRandom(0, 2);
                enemyList[tempR].Hurt(selfArmy, this);
                break;
            case 2:
                int tempR2 = (int)MyTools.ins.getRandom(0, 2);
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (i == tempR2)
                    {
                        continue;
                    }
                    enemyList[i].Hurt(selfArmy, this);
                }

                break; 
        }


        curDamge = getDamage() * curTrunTargetCount;

        totalDamage += curDamge;
     //   MyTools.ins.showSkill(null, this, SkillState.DAMAGE);
        totalCastCount++;

    }
    //是否有BUFF
    private void IsBuff()
    {
        for (int i=0; i < buffList.Count; i++)
        {
            if (buffList[i].isHit())
            {
            //    targetBuffList.Add(buffList[i].Clone());
            }

        }
    }

    //public SkillState CanCast()
    //{
    //    SkillState skillState= SkillState.INSTANT_CAST;

    //    return skillState;
    //}


    public bool IsCast(Army selfArmy, List<Army> enemyList)
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
                    InstantCast(selfArmy,enemyList);
                    
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
                    MyTools.ins.ShowSkill(selfArmy, this, SkillState.READY_1);
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
                InstantCast(selfArmy,enemyList);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int RandTargetCount()
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


    public  float getDamage()
    {
        //TODO:此处先不做伤害浮动，以后再说
        float damge = damageRate;

        return damge;
    }

    public float getTotalDamage()
    {
        return totalDamage;
    }

    public void SummaryData()
    {

    }

}
