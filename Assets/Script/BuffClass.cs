using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType
{
    BUFF,
    DOT,
    HOT,


}

public class BuffClass : MonoBehaviour
{

    public BuffType buffType;
    public int maxRound;//最大回合数
    public int currentRound;//当前回合
    public float hitRate;//中BUFF概率


    public void DoEffect()
    {
        switch (buffType)
        {
            case BuffType.BUFF:

                break;
            case BuffType.DOT:
                DoDot();
                break;
            case BuffType.HOT:
                DoHot();
                break;


        }

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


    protected virtual void DoDot()
    {

    }

    protected virtual  void DoHot()
    {

    }

    protected virtual void DoOther()
    {


    }

    public virtual BuffClass Clone()
    {

        return null;
    }


}
