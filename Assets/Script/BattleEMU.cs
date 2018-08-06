using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEMU : MonoBehaviour
{
    public int testCount = 100;

    public Skill skillA;


    public List<Army> enemyArmiesList;

    public List<Army> ourArmiesList;

    public bool playerFirstAction = true;

    int castCount = 0;
    // Use this for initialization
    void Start()
    {
        InitBattle();
        //InitVS();

        TestModel(testCount);

        MyTools.ins.AllShow();
        //  Battle();
    }

    private void InitBattle()
    {
        enemyArmiesList = new List<Army>(3);
        enemyArmiesList.Add(new Army("敌人前锋", 50, 50, 10000));
        enemyArmiesList.Add(new Army("敌人中军", 50, 50, 10000));
        enemyArmiesList.Add(new Army("敌人大营", 50, 50, 10000));


        ourArmiesList = new List<Army>();

        Army me = new Army("主角", 50, 50, 10000);
        me.AddSkill(ChooseSkill(me, "火烧连营"));//给部队增加一个技能

        me.color = GameConst.Color.greenColor;
        enemyArmiesList[0].color = GameConst.Color.redColor;
        enemyArmiesList[1].color = GameConst.Color.redColor;
        enemyArmiesList[2].color = GameConst.Color.redColor;

        ourArmiesList.Add(me);

    }

    private void InitVS()
    {
        enemyArmiesList = new List<Army>(1);
        enemyArmiesList.Add(new Army("敌人", 50, 50, 10000));
        ourArmiesList = new List<Army>();
        Army me = new Army("主角", 50, 50, 10000);
        //me.AddSkill(ChooseSkill(me, "驱虎吞狼"));//给部队增加一个技能

        me.color = GameConst.Color.greenColor;
        enemyArmiesList[0].color = GameConst.Color.redColor;
        ourArmiesList.Add(me);
    }




    private Skill ChooseSkill(Army army, string name)
    {
        Skill tempSkll = null;
        DotBuff dotBuff = null;

        string[] skillNameArray = { "声东击西", "水淹七军", "溃堤", "危崖困军", "长坂之吼",
                                                "驱虎吞狼", "玄武巨流", "焰焚箕轸", "楚歌四起", "黄天当立",
                                                "逆反毒杀", "复誓业火", "密谋定蜀","冢虎","增援" ,"养精蓄锐",
                                                "安抚军心","火烧连营","毒泉","火辎"};
        int index = 0;
        for (int i = 0; i < skillNameArray.Length; i++)
        {
            if (skillNameArray[i].Equals(name))
            {
                index = i;
            }
        }

        switch (index)
        {
            case 0:
                //  tempSkll= new Skill("声东击西", 50f, 1, 12, 231);
                break;
            case 1:
                tempSkll = new Skill(army, "水淹七军", SkillType.ACTIVE, 50, 1, 22, 205);
                break;

            case 2:
                tempSkll = new Skill(army, "溃堤", SkillType.ACTIVE, 45, 0, 22, 79.8f);
                break;
            case 3:
                tempSkll = new Skill(army, "危崖困军", SkillType.ACTIVE, 50, 1, 22, 210);
                break;
            case 5:
                tempSkll = new Skill(army, "驱虎吞狼", SkillType.ACTIVE, 30f, 0, 33, 143);
                dotBuff = new DotBuff(tempSkll, "围困", SwitchRule.SWITCH, 1, 100, 0, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;

            case 7:
                tempSkll = new Skill(army, "焰焚箕轸", SkillType.ACTIVE, 50f, 1, 23, 119);
                dotBuff = new DotBuff(tempSkll, "燃烧", SwitchRule.SWITCH, 1, 100, 119, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 8:
                tempSkll = new Skill(army, "楚歌四起", SkillType.ACTIVE, 50f, 1, 23, 0);
                dotBuff = new DotBuff(tempSkll, "恐慌", SwitchRule.SWITCH, 2, 100, 127, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 9:
                tempSkll = new Skill(army, "黄天当立", SkillType.ACTIVE, 35f, 1, 33, 176);
                dotBuff = new DotBuff(tempSkll, "妖术", SwitchRule.SWITCH, 2, 100, 96, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 10:
                tempSkll = new Skill(army, "逆反毒杀", SkillType.ACTIVE, 35f, 0, 12, 0);
                dotBuff = new DotBuff(tempSkll, "恐慌", SwitchRule.SWITCH, 2, 100, 83, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 11:
                tempSkll = new Skill(army, "复誓业火", SkillType.ACTIVE, 45f, 0, 22, 0);
                dotBuff = new DotBuff(tempSkll, "燃烧", SwitchRule.SWITCH, 2, 100, 133, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 12:
                tempSkll = new Skill(army, "密谋定蜀", SkillType.ACTIVE, 35f, 0, 22, 0);
                dotBuff = new DotBuff(tempSkll, "恐慌", SwitchRule.SWITCH, 2, 100, 115, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;
            case 13:
                tempSkll = new Skill(army, "冢虎", SkillType.ACTIVE, 35f, 0, 12, 150);               
                break;

            case 14:
                tempSkll = new Skill(army, "增援", SkillType.ACTIVE, 45f, 1, 22, 198);
                break;

            case 15:
                tempSkll = new Skill(army, "养精蓄锐", SkillType.ACTIVE, 45f, 1, 22, 0);
                dotBuff = new DotBuff(tempSkll, "休整", SwitchRule.SWITCH, 2, 100, 122, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;

            case 16:
                tempSkll = new Skill(army, "安抚军心", SkillType.ACTIVE, 35f, 0, 22, 108);
                break;
            case 17:
                tempSkll = new Skill(army, "火烧连营", SkillType.ACTIVE, 35f, 1, 33, 92);
                Skill tempSkll2 = new Skill(army, "", SkillType.ACTIVE, 50f, 0, 33, 133);
                Skill tempSkll3 = new Skill(army, "", SkillType.ACTIVE, 100f, 0, 33, 0);
                dotBuff = new DotBuff(tempSkll, "燃烧", SwitchRule.SWITCH, 2, 50, 76, TargetType.ENEMY);
                tempSkll3.AddBuff(dotBuff);
                tempSkll.subSkillList.Add(tempSkll2);
                tempSkll.subSkillList.Add(tempSkll3);
                break;

            case 18:
                tempSkll = new Skill(army, "毒泉", SkillType.ACTIVE, 50f, 1, 22, 0);
                dotBuff = new DotBuff(tempSkll, "恐慌", SwitchRule.SWITCH, 2, 100, 85, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;

            case 19:
                tempSkll = new Skill(army, "火辎", SkillType.ACTIVE, 50f, 1, 22, 75);
                dotBuff = new DotBuff(tempSkll, "燃烧", SwitchRule.SWITCH, 1, 100, 75, TargetType.ENEMY);
                tempSkll.AddBuff(dotBuff);
                break;

        }

        return tempSkll;
    }


    public void TestModel(int testCount)
    {

        for (int i = 0; i < testCount; i++)
        {
            MyTools.ins.BattleBegin(i + 1);


            ArmyFight(ourArmiesList, enemyArmiesList); 


            MyTools.ins.BattleEnd(i + 1);
             
            MyTools.ins.NextBattle();
        }

        //显示统计数据
        SummaryData();
    }

    private void SummaryData()
    {
        for (int i = 0; i < ourArmiesList.Count; i++)
        {
            ourArmiesList[i].SummaryData(enemyArmiesList, testCount);
        }

    }

    //一场战斗
    public void ArmyFight(List<Army> ourList, List<Army> enemyList)
    {
        //重置战前状态
        foreach (Army temp in ourList)
        {
            temp.ResetData();
        }
        foreach (Army temp in enemyList)
        {
            temp.ResetData();
        }

        //开始8回合战斗
        for (int i = 0; i < 8; i++)
        {
            MyTools.ins.ShowTrun(i + 1);
            OneTrunFight(ourList, enemyList);
            //MyTools.ppp("产生了一些战斗过程");
        }
    }

    //一回合战斗
    private void OneTrunFight(List<Army> playerList, List<Army> enemyList)
    {
        List<Army> attacker = new List<Army>();
        List<Army> defender = new List<Army>();


        if (playerFirstAction)
        {
            attacker = playerList;
            defender = enemyList;
        }
        else
        {
            attacker = enemyList;
            defender = playerList;
        }

        for (int i = 0; i < attacker.Count; i++)
        {
            attacker[i].Action(defender);
        }

        for (int i = 0; i < defender.Count; i++)
        {
            defender[i].Action(attacker);
        }




    }



    //public void Battle()
    //{
    //    float total = 0;

    //    for (int k = 0; k < testCount; k++)
    //    {
    //        MyTools.ins.BattleBegin(k+1);
    //        GameConst.curShowTextConut=k+1;


    //        for (int i = 0; i < 8; i++)
    //        {
    //            MyTools.ins.ShowTrun(i + 1);



    //            //if (skillA.IsCast(null))
    //            //{
    //            //    castCount++;
    //            //}

    //        }

    //     //   print("释放了" + castCount + "次");
    //        total += castCount;
    //        castCount = 0;
    //        skillA.NextTrun();
    //        MyTools.ins.BattleEnd(k+1);

    //    }

    //    ttt("平均释放了" + total / testCount + "次");


    //    print("平均释放了" + total/testCount + "次");
    //    print("平均伤害"+skillA.getTotalDamage()/ testCount);

    //}

    private void ttt(string str)
    {
        MyTools.ins.Append(str);
    }


}
