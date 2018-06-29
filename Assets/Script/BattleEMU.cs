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
        skillA = new Skill("声东击西",50f,1);
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
            for (int i = 0; i < 8; i++)
            {
                if (skillA.IsCast())
                {
                    castCount++;
                }

            }

         //   print("释放了" + castCount + "次");
            total += castCount;
            castCount = 0;

        }

        print("平均释放了" + total/testCount + "次");

    }




}
