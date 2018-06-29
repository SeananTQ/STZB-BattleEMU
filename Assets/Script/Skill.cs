using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mytools;

public class Skill : MonoBehaviour
{

    public string name;
    public float castRate;

    public int readyTime;
    private int curTime = 0;

    public Skill(string name, float castRate, int readyTime)
    {
        this.name = name;
        this.castRate = castRate;
        this.readyTime = readyTime;

        curTime = readyTime;
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
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
