using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{

    public string sheetName = "Sheet1";

    public List<Dictionary<string, string>> sheetAllData;

    public List<Dictionary<string, string>> threeList;
    public List<Dictionary<string, string>> fourList;
    public List<Dictionary<string, string>> fivesList;
    public List<Dictionary<string, string>> oneAndTwoList;

    void Start()
    {
        threeList = new List<Dictionary<string, string>>();
        fourList = new List<Dictionary<string, string>>();
        fivesList = new List<Dictionary<string, string>>();
        oneAndTwoList = new List<Dictionary<string, string>>();


        sheetAllData = TableLoader.Instance.GetSheetDataInEditor(sheetName);

        SortGroup(sheetAllData);
        //print(sheetData[2]["id"]);
    }

    public void SortGroup(List<Dictionary<string, string>> value)
    {
        for (int i = 1; i < value.Count; i++)
        {
            Dictionary<string, string> temp = value[i];
            // print(temp["star"]);

            //    //先按星级分组
            int starCount = int.Parse(temp["star"]);
            switch (starCount)
            {
                case 5:
                    fivesList.Add(temp);
                    break;
                case 4:
                    fourList.Add(temp);
                    break;
                case 3:
                    threeList.Add(temp);
                    break;
                case 2:
                    oneAndTwoList.Add(temp);
                    break;
                case 1:
                    oneAndTwoList.Add(temp);
                    break;
            }

            //赛季和觉醒卡牌图片字段清空
            temp["season"] = "";
            temp["specialIcon"] = "";
        }


        print("5星有" + fivesList.Count);
        print("4星有" + fourList.Count);
        print("3星有" + threeList.Count);
        print("1和2星有" + oneAndTwoList.Count);


        //预先清理3星4星的一些无用值
        for (int i = 0; i < fourList.Count; i++)
        {
            fourList[i]["upType"] = "0";
        }
        for (int i = 0; i < threeList.Count; i++)
        {
            threeList[i]["upType"] = "0";
        }
        for (int i = 0; i < oneAndTwoList.Count; i++)
        {
            oneAndTwoList[i]["upType"] = "0";
        }


        //以5星的数量为标准处理
        for (int i = 0; i < fivesList.Count; i++)
        {
            //名字 name
            fourList[i]["name"] = fivesList[i]["name"];
            threeList[i]["name"] = fivesList[i]["name"];

            //攻击距离 atkDistance
            fourList[i]["atkDistance"] = fivesList[i]["atkDistance"];
            threeList[i]["atkDistance"] = fivesList[i]["atkDistance"];

            //同一个英雄的标记 heroMark
            fivesList[i]["heroMark"] = fivesList[i]["id"];
            fourList[i]["heroMark"] = fivesList[i]["heroMark"];
            threeList[i]["heroMark"] = fivesList[i]["heroMark"];

            //兵种 sectId
            fourList[i]["sectId"] = fivesList[i]["sectId"];
            threeList[i]["sectId"] = fivesList[i]["sectId"];

            //性别 sex
            fourList[i]["sex"] = fivesList[i]["sex"];
            threeList[i]["sex"] = fivesList[i]["sex"];

            //阵营 camp
            fourList[i]["camp"] = fivesList[i]["camp"];
            threeList[i]["camp"] = fivesList[i]["camp"];


            //图片 icon
            fourList[i]["icon"] = fivesList[i]["icon"];
            threeList[i]["icon"] = fivesList[i]["icon"];

            //升星后的ID upTargetId
            //fivesList[i]["upTargetId"] = "";
            fourList[i]["upTargetId"] = fivesList[i]["id"];
            threeList[i]["upTargetId"] = fourList[i]["id"];

            // cost
            float tempCost = float.Parse(fivesList[i]["cost"]);
            fourList[i]["cost"] = (tempCost - 0.5f).ToString();
            threeList[i]["cost"] = (tempCost - 1f).ToString();


            //基础属性 baseAtk 	baseDef	baseStrategy	 baseSpeed	baseBuilddmg
            string[] baseAName = { "baseAtk", "baseDef", "baseStrategy", "baseSpeed", "baseBuilddmg" };
            baseAbilityAuto(baseAName,i, threeList, fourList, fivesList);

            //属性成长 growAtk	growDef	growStrategy	growSpeed	growBuilddmg
            string[] growAName = { "growAtk","growDef","growStrategy","growSpeed","growBuilddmg" };
            growAbilityAuto(growAName, i, threeList, fourList, fivesList);




            //如果有不为0的则345相同 tgroup
            if (fivesList[i]["tgroup"].Equals("0") == false)
            {
                fourList[i]["tgroup"] = fivesList[i]["tgroup"];
                threeList[i]["tgroup"] = fivesList[i]["tgroup"];
            }
            else if (fourList[i]["tgroup"].Equals("0") == false)
            {
                fivesList[i]["tgroup"] = fourList[i]["tgroup"];
                threeList[i]["tgroup"] = fourList[i]["tgroup"];
            }
            else if (threeList[i]["tgroup"].Equals("0") == false)
            {
                fivesList[i]["tgroup"] = threeList[i]["tgroup"];
                fourList[i]["tgroup"] = threeList[i]["tgroup"];
            }


            //如果有不为0的则345相同 hgroup
            if (fivesList[i]["hgroup"].Equals("0") == false)
            {
                fourList[i]["hgroup"] = fivesList[i]["hgroup"];
                threeList[i]["hgroup"] = fivesList[i]["hgroup"];
            }
            else if (fourList[i]["hgroup"].Equals("0") == false)
            {
                fivesList[i]["hgroup"] = fourList[i]["hgroup"];
                threeList[i]["hgroup"] = fourList[i]["hgroup"];
            }
            else if (threeList[i]["hgroup"].Equals("0") == false)
            {
                fivesList[i]["hgroup"] = threeList[i]["hgroup"];
                fourList[i]["hgroup"] = threeList[i]["hgroup"];
            }

            //有false则全false isActive
            if (fivesList[i]["isActive"].Equals("false") || fourList[i]["isActive"].Equals("false") || threeList[i]["isActive"].Equals("false"))
            {
                fivesList[i]["isActive"] = "false";
                fourList[i]["isActive"] = "false";
                threeList[i]["isActive"] = "false";
            }

            // upType
            threeList[i]["upType"] = "1#3";
            fourList[i]["upType"] = "1#2";
            fivesList[i]["upType"] = "0";
            

            //   print("==" + fourList[i]["name"] + " " + threeList[i]["name"]);
        }


        TableLoader.Instance.OutToJson(value);


    }


    public void baseAbilityAuto(string[] value,int index,List<Dictionary<string,string>> threeList, List<Dictionary<string, string>>  fourList, List<Dictionary<string, string>> fivesList)
    {
        foreach (string temp in value)
        {
            int tempInt = int.Parse(threeList[index][temp]);
            fourList[index][temp] =  ((int)(tempInt * 1.4f)).ToString();
            fivesList[index][temp] = ((int)(tempInt * 1.9f)).ToString();
        }
    }


    public void growAbilityAuto(string[] value, int index, List<Dictionary<string, string>> threeList, List<Dictionary<string, string>> fourList, List<Dictionary<string, string>> fivesList)
    {
        foreach (string temp in value)
        {
            float tempInt = float.Parse(threeList[index][temp]);
            fourList[index][temp] = (tempInt * 2f).ToString();
            fivesList[index][temp] = (tempInt * 3f).ToString();
        }
    }

}
