using UnityEngine;
using UnityEditor;

public class DotBuff : BuffClass
{
    


    public float dotRate;//持续伤害生效的概率
    public float dotDamage;//持续伤害单跳一次的伤害


    private Skill skill;

    //public float 

    public DotBuff(Skill skill,int maxRound,int currentRound,float dotRate,float dotDamage)
    {
        this.skill = skill;
        this.maxRound = maxRound;
        this.currentRound = currentRound;
        this.dotRate = dotRate;
        this.dotDamage = dotDamage;
    }

    protected override void DoDot()
    {
        
    }

    public override BuffClass Clone()
    {
        int tempCurrentRound = 0;
        DotBuff temp = new DotBuff(this.skill,this.maxRound, tempCurrentRound, this.dotRate,this.dotDamage);

        return temp;
    }

}