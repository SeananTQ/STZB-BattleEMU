using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill 
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

    public Army army;
    public SkillType skillType;
    
    /// <summary>
    /// 技能构造函数
    /// </summary>
    /// <param name="name">技能名称</param>
    /// <param name="castRate">发动概率</param>
    /// <param name="readyTime">准备回合数</param>
    /// <param name="targetCount">可攻击目标数量，十位为下限，个位为上限</param>
    /// <param name="damage">技能伤害率</param>
    public Skill(Army army,string name,SkillType skillType, float castRate, int readyTime, int targetCount, float damage)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;
        this.targetLowerCount = targetCount / 10;
        this.targetUpperCount = targetCount % 10;
        this.damageRate = damage;
        this.army = army;
        this.skillType = skillType;
        buffList = new List<BuffClass>();
    }

    //重置状态，用于开始下一场战斗
    public void ResetData()
    {
        curTime = 0;
    }


    public void AddBuff(BuffClass buff)
    {
        if (buff == null)
        {
           MyTools.ppp("加进来就是空的");
        }

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
        MyTools.ins.ShowCastSkill(selfArmy, this, SkillState.INSTANT_CAST);

        //这里应当判断对自身生效的BUFF TODO

        //先随机能够攻击的目标数量
        curTrunTargetCount = RandTargetCount();
        //这里应当判断攻击距离TODO

        List<Army> tempEnemyList = new List<Army>();

        //然后判断该技能攻击目标的数量，3全打，1、2随机
        switch (curTrunTargetCount)
        {
            case 3:
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (this.damageRate > Mathf.Epsilon)//判断仅能是否有直接伤害，有些毒技能没有直接伤害
                    {
                        enemyList[i].Hurt(selfArmy, this);
                    }
       

                    tempEnemyList.Add(enemyList[i]);
      
                }

                break;

            case 1:
                int tempR=(int) MyTools.ins.getRandom(0, 2);
                if (this.damageRate > Mathf.Epsilon)//判断仅能是否有直接伤害，有些毒技能没有直接伤害
                {
                    enemyList[tempR].Hurt(selfArmy, this);
                }
                tempEnemyList.Add(enemyList[tempR]);
                break;
            case 2:
                int tempR2 = (int)MyTools.ins.getRandom(0, 2);//随机打2个目标的技能，其实只需要随机1个未被打中的即可
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (i == tempR2)
                    {
                        continue;
                    }

                    if (this.damageRate > Mathf.Epsilon)//判断仅能是否有直接伤害，有些毒技能没有直接伤害
                    {
                        enemyList[i].Hurt(selfArmy, this);
                    }
                    tempEnemyList.Add(enemyList[i]);
                }

                break;
        }

        for (int i = 0; i < tempEnemyList.Count; i++)
        {
            //对受击目标施加BUFF
            for (int k = 0; k < buffList.Count; k++)
            {
                var tempBuff = buffList[k];

                switch (tempBuff.targetType)
                {
                    case TargetType.ENEMY:
                        tempEnemyList[i].HitBuff(buffList[k].Clone());// 克隆一个新的BUFF给敌人
                      //  enemyList[i].HitBuff(buffList[k]);// 克隆一个新的BUFF给敌人
                        break;

                }
            }
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
                    MyTools.ins.ShowCastSkill(selfArmy, this, SkillState.READY_1);
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
