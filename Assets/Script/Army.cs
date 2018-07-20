
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
    public float curTroops;
    public float maxTroops;
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
        this.maxTroops = count;
        this.curTroops = maxTroops;

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
        
        //清空身上挂的BUFF
        buffQueue = new List<BuffClass>();
        //恢复兵力
        this.curTroops = maxTroops;
    }




    public bool isLife
    {
        get
        {

            if (curTroops < 1)

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



    //普通攻击
    public void Attack(Army targetArmy   )
    {
        //先计算挥出伤害
        float outDamge = getAttackDamage(this.atk, targetArmy.def, this.curTroops, 1f);

        //再给对敌人落实伤害
        float finalDamage = targetArmy.Hurt(outDamge,false);
        MyTools.ins.ShowAttack(this, targetArmy);
        MyTools.ins.ShowHurt(targetArmy, finalDamage);
    }

    public string Action(List<Army> enemyList)
    {
        //轮到本部队行动


        List<BuffClass> removeList = new List<BuffClass>();
        //首先结算自身的BUFF
        for (int i = 0; i < buffQueue.Count; i++)
        {
            BuffClass temp =buffQueue[i];

            //MyTools.ppp(temp.effectName);

            if (temp.DoEffect(this) == false)
            {
                removeList.Add(temp);//标记删除
            } 

        }

        for (int i = removeList.Count-1; i >= 0; i--)
        {
            var tempBuff = removeList[i];
            MyTools.ins.ShowRemoveBuff(this, tempBuff);
            buffQueue.Remove(tempBuff);
            tempBuff = null;
        }
        removeList.Clear();


        //然后判断自身是否可以行动，死没晕没

        //然后按照顺序触发主动技能
        for (int i = 0; i < skillList.Count; i++)
        {
            skillList[i].IsCast(this,enemyList);

        }


        //然后进行普攻
        //暂时忽略攻击距离
        int tempIndex = (int)MyTools.ins.getRandom(0, enemyList.Count-1);
        Attack(enemyList[tempIndex]);


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
        curTroops = Mathf.Max(0, curTroops - inDamage);
            

        HurtEvent hurtEvent = new HurtEvent(attacker.name, skill.name, inDamage);

        hurtEventList.Add(hurtEvent);
        MyTools.ins.ShowHurt(this, inDamage);

    }


    //Dot伤害
    public void Hurt(DotBuff buff)
    {
        if (buff.getDamage() == 0)
        {
            return ;
        }

        //这里本应该计算自身免伤，该版本忽略TODO
        float inDamage = buff.getDamage() * 1f;
        //记录受到的伤害
        this.curHurtDamage += inDamage;
        totalhurtDamage += curHurtDamage;
        curTroops = Mathf.Max(0, curTroops - inDamage);


        HurtEvent hurtEvent = new HurtEvent(buff.skill.army.name, buff.skill.name, inDamage);

        hurtEventList.Add(hurtEvent);
        MyTools.ins.ShowDotHurt(this, buff);

    }
    //中buff
    public void HitBuff(BuffClass buff)
    {
        var tempBuff = buffQueue.Find(n => n.effectName.Equals(buff.effectName));
        if (tempBuff != null)
        {
            //找到一个相同效果的BUFF，然后来判断替换规则

            switch (buff.switchRule)
            {
                case SwitchRule.NOT:

                    break;

                case SwitchRule.RESET:
                    tempBuff.ResetRound();
                    break;
                case SwitchRule.SWITCH:

                    if (buff.switchWeight > tempBuff.switchWeight)
                    {
                        buffQueue.Remove(tempBuff);
                        buffQueue.Add(buff);
                        MyTools.ins.ShowHitBuff(this, buff,2);
                    }
                    else
                    {
                        //存在一个更强大的BUFF
                        MyTools.ins.ShowHitBuff(this, buff,0);
                    }
                    break;


            }
        }
        else
        {
            //未找到则直接追加
            buffQueue.Add(buff);
            MyTools.ins.ShowHitBuff(this, buff,1);
        }



    }



    //受到普通攻击
    public float Hurt(float damage,bool isMagic)
    {

        float tempCount = curTroops;

        // damage =    (float)  Math.Round(Convert.ToDouble(damage), MidpointRounding.AwayFromZero);
        curTroops -= damage;
        curTroops = Mathf.Max(0, curTroops);

        if (curTroops > 0)
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
        return curTroops;
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
