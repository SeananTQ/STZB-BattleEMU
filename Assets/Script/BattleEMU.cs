using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEMU : MonoBehaviour
{
    public int testCount=100;

    public Skill skillA;

    // Use this for initialization
    void Start()
    {
        // skillA = new Skill("声东击西",50f,1,12,231);
        //skillA = new Skill("水淹七军", 50f, 1, 22, 205);
        // skillA = new Skill("溃堤", 45f, 0, 22, 79.8f);
        //skillA = new Skill("危崖困军", 50f, 1, 22, 210);
        //skillA = new Skill("长坂之吼", 75f, 2, 22, 450);
        //skillA = new Skill("驱虎吞狼", 30f, 0, 33, 143);
        skillA = new Skill("玄武巨流", 30f, 1, 33, 150);
        Battle();
    }

    // Update is called once per frame
    void Update()
    {

    }


    int castCount = 0;

    public void Battle()
    {
        float total = 0;

        for (int k = 0; k < testCount; k++)
        {
            MyTools.ins.BattleBegin(k+1);

            GameConst.curShowTextConut=k+1;

            for (int i = 0; i < 8; i++)
            {
                MyTools.ins.Trun(i + 1);
                if (skillA.IsCast())
                {
                    castCount++;
                }

            }

         //   print("释放了" + castCount + "次");
            total += castCount;
            castCount = 0;

            MyTools.ins.BattleEnd(k+1);

        }

        ttt("平均释放了" + total / testCount + "次");


        print("平均释放了" + total/testCount + "次");
        print("平均伤害"+skillA.getTotalDamage()/ testCount);

    }

    private void ttt(string str)
    {
        MyTools.ins.ttt(str);
    }


}
