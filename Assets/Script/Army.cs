
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Army
{
    public string name;
    public float atk;
    public float def;
    public float count;
    public string color;

    public List<BuffClass> buffQueue;

    public List<Skill> skillList;

    private float curHurtDamage;
    private float totalhurtDamage;

    private List<HurtEvent> hurtEventList;


    public Army(string name, float atk, float def, float count)
    {
        this.name =  name ;
        this.atk = atk;
        this.def = def;
        this.count = count;

        buffQueue = new List<BuffClass>();
        skillList = new List<Skill>();
        hurtEventList = new List<HurtEvent>();
}

    public void ResetData()
    {
        foreach (Skill temp in skillList)
        {
            temp.ResetData();
        }      

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

    //计算普攻伤害
    public float getAttackDamage(float atk, float def, float armyCount, float rate)
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
        float tempDamage = getAttackDamage(this.atk, bbb.def, this.count, 1f);


        return bbb.Hurt(tempDamage).ToString("0.0");
    }

    public string Action(List<Army> enemyList)
    {
        //轮到本部队行动


        //首先结算自身的BUFF
        for (int i = 0; i < buffQueue.Count; i++)
        {
            DotBuff temp = (DotBuff)buffQueue[i];

            //MyTools.ppp(temp.effectName);

            if (temp.DoEffect(this) == false)
            {
                MyTools.ins.ShowRemoveBuff(this, temp);
                buffQueue.Remove(temp);   
                temp = null;

            }
            // buffQueue[i].DoEffect(this);

        }




        //然后判断自身是否可以行动，死没晕没

        //然后按照顺序触发主动技能
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].IsCast(this,enemyList);




            //先判断我方部队是否可以发动技能
            //skillList[i].CanCast();
            //如果可以发动技能，再随机这个技能可以打几个目标
            //skillList[i].getTargetCount();
            //对每个敌方目标发动技能，同时判定是否能够触发BUFF
            //for()...
            //skillList[i].attack()
            //正常该敌方行动了，测试中敌方不行动
            //结算DOT伤害


        }


        //然后进行普攻
        //然后触发追击技能





        return null;
    }


    public void Hurt(Army attacker, Skill skill)
    {

        //这里本应该计算自身免伤，该版本忽略TODO
        float inDamage = skill.getDamage() * 1f;
        //记录受到的伤害
        this.curHurtDamage += inDamage;
        totalhurtDamage += curHurtDamage;
        count = Mathf.Max(0, count - inDamage);
            

        HurtEvent hurtEvent = new HurtEvent(attacker.name, skill.name, inDamage);

        hurtEventList.Add(hurtEvent);
        MyTools.ins.ShowHurt(this, inDamage);

    }


    //Dot伤害
    public void Hurt(DotBuff buff)
    {
        //这里本应该计算自身免伤，该版本忽略TODO
        float inDamage = buff.getDamage() * 1f;
        //记录受到的伤害
        this.curHurtDamage += inDamage;
        totalhurtDamage += curHurtDamage;
        count = Mathf.Max(0, count - inDamage);


       // HurtEvent hurtEvent = new HurtEvent(attacker.name, skill.name, inDamage);

      //  hurtEventList.Add(hurtEvent);
        MyTools.ins.ShowDotHurt(buff.skill.army, buff);

    }
    //中buff
    public void HitBuff(BuffClass buff)
    {
        var tempBuff = buffQueue.Find(n => n.effectName.Equals(buff.effectName));
        if(tempBuff!=null)
        {
            switch (buff.switchRule)
            {
                case SwitchRule.NOT:

                    break;

                case SwitchRule.RESET:
                    tempBuff.ResetRound();
                    break;
                case SwitchRule.SWITCH:

                    if (buff.switchWeight >= tempBuff.switchWeight)
                    {
                        buffQueue.Remove(tempBuff);
                        buffQueue.Add(buff);
                    }
                    else
                    {
                        //存在一个更强大的BUFF

                    }
                    break;


            }
        }
        buffQueue.Add(buff);
        MyTools.ins.ShowHitBuff(this, buff);

    }




    public float Hurt(float damage)
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

    public void AddSkill(Skill skill)
    {
        skillList.Add(skill);
    }


    public  struct HurtEvent
    {
        public string attackerName;
        public string skillName;
        public float damage;


        public HurtEvent(string attackerName, string skillName, float damage) : this()
        {
            this.attackerName = attackerName;
            this.skillName = skillName;
            this.damage = damage;
        }
    }

    public void SummaryData(List<Army> enemyArmies,float testCount)
    {
        List<HurtEvent> tempList=new List<HurtEvent>();

        for (int i = 0; i < enemyArmies.Count; i++)
        {
            Army tempArmy = enemyArmies[i];
            //将攻击者为自己的伤害事件筛选出来。
            tempList= tempList.Concat<HurtEvent>(tempArmy.hurtEventList.Where(x => x.attackerName == this.name).ToList()).ToList();

            //tempList = tempArmy.hurtEventList.Where(x => x.attackerName.Equals(name)).ToList();
        }

     //   MyTools.ppp(enemyArmies[0].hurtEventList[0].attackerName+">>>" + tempList.Count);

        for (int i = 0; i < skillList.Count; i++)
        {
            //将技能名字符合的选择出来
            string tempName = skillList[i].name;
            float totalDamage=  tempList.Where(n => n.skillName == tempName).Select(x => x.damage).Sum();

            MyTools.ins.ShowSkillAverageDamage(this, testCount, skillList[i], totalDamage);
        }
        
     

    }


}
