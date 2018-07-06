using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class BuffClass 
{

    public BuffType buffType;
    public int maxRound;//最大回合数
    public int currentRound;//当前回合
    public float hitRate;//中BUFF概率
    public string effectName;//例如 恐慌 火攻 动摇 等
    public TargetType targetType;

    public Skill skill;
    public SwitchRule switchRule;//替换规则
    public float switchWeight;//替换权重

    public BuffClass()
    {
     //   print("被调用了");
    }


    public bool DoEffect(Army army)
    {
        if (currentRound == 0)
        {
            return false;
        }
        else
        {
            currentRound--;
        }


        switch (buffType)
        {
            case BuffType.BUFF:

                break;
            case BuffType.DOT:


                DoDot(army);
                break;
            case BuffType.HOT:
                DoHot(army);
                break;


        }

        return true;

    }

    public bool isHit()
    {

        if (MyTools.ins.getRandom(0, 99) < hitRate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    protected virtual void DoDot(Army army)
    {
        MyTools.ppp("子类未重载该方法");

    }

    protected virtual  void DoHot(Army army)
    {

    }

    protected virtual void DoOther(Army army)
    {


    }

    public virtual BuffClass Clone()
    {

        return null;
    }

    //刷新回合数
    public void ResetRound()
    {
        currentRound = maxRound;

    }

}
