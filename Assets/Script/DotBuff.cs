using UnityEngine;
using UnityEditor;

public class DotBuff : BuffClass
{
    


    public float dotRate;//持续伤害生效的概率
    public float dotDamage;//持续伤害单跳一次的伤害

    public DotBuff() :base()
    {

    }

    public float getDamage()
    {

        return dotDamage;
    }
    //public float 

    public DotBuff(Skill skill, string name,SwitchRule switchRule, int maxRound, float dotRate, float dotDamage, TargetType target)
    {
        this.skill = skill;
        this.maxRound = maxRound;
        this.currentRound = 0;
        this.dotRate = dotRate;
        this.dotDamage = dotDamage;
        this.effectName = name;
        this.targetType = target;
        this.switchRule = switchRule;

        this.switchWeight = getDamage();

        this.buffType = BuffType.DOT;
    }

    protected override void DoDot(Army army)
    {
     
            army.Hurt(this);


       // army.Hurt(this);

    }

    //public override BuffClass Clone()
    //{
    //    //int tempCurrentRound = 0;
    //    DotBuff temp = new DotBuff(this.skill,this.effectName,this.maxRound,  this.dotRate,this.dotDamage,this.targetType);
    //    temp.currentRound = 0;

    //    MyTools.ppp(temp.ToString());
    //    return temp;
    //}


}