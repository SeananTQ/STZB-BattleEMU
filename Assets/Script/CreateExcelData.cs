using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExcelData    : MonoBehaviour
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

   
        print(sheetAllData[2]["name"]);
        print(sheetAllData[2]["baseAtk"]);

        SortGroup(sheetAllData);
    }

    public List<Dictionary<string, string>> CloneDict(List<Dictionary<string, string>> target)
    {
        List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
        for (int i = 0; i < target.Count; i++)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>(target[i]);

            result.Add(temp);
        }
        print(result.Count);
        return result;
    }


    public void SortGroup(List<Dictionary<string, string>> value)
    {
        //复制出 3 4 5 星
        threeList = CloneDict(value);
        fourList = CloneDict(value);
        fivesList = CloneDict(value);


        print("5星有" + fivesList.Count);
        print("4星有" + fourList.Count);
        print("3星有" + threeList.Count);


        ////预先清理3星4星的一些无用值
        //for (int i = 0; i < fourList.Count; i++)
        //{
        //    fourList[i]["upType"] = "0";
        //}
        //for (int i = 0; i < threeList.Count; i++)
        //{
        //    threeList[i]["upType"] = "0";
        //}


        //以5星的数量为标准处理
        for (int i = 1; i <60; i++)
        {

            //ID
            fivesList[i]["id"] = threeList[i]["id"] + "5";
            fourList[i]["id"] = threeList[i]["id"] + "4";
            threeList[i]["id"] = threeList[i]["id"] + "3";

            //名字 name
            fourList[i]["name"] = fivesList[i]["name"];
            threeList[i]["name"] = fivesList[i]["name"];

            //升星后的ID upTargetId
            //fivesList[i]["upTargetId"] = "";
            fourList[i]["upTargetId"] = fivesList[i]["id"];
            threeList[i]["upTargetId"] = fourList[i]["id"];

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



            // cost
          //  print(">>>cost:"+i+"  "+ fivesList[i]["name"] + "    "+ fivesList[i]["cost"]);

            float tempCost = float.Parse(fivesList[i]["cost"]);
            fourList[i]["cost"] = (tempCost - 0.5f).ToString();
            threeList[i]["cost"] = (tempCost - 1f).ToString();



            //属性成长 growAtk	growDef	growStrategy	growSpeed	growBuilddmg
            string[] growAName = { "growAtk","growDef","growStrategy","growSpeed","growBuilddmg" };
            growAbilityAuto(growAName, i, threeList, fourList, fivesList);

            //基础属性 baseAtk 	baseDef	baseStrategy	 baseSpeed	baseBuilddmg
            string[] baseAName = { "baseAtk", "baseDef", "baseStrategy", "baseSpeed", "baseBuilddmg" };
            baseAbilityAuto(baseAName, growAName, i, threeList, fourList, fivesList);





            //星级
            fourList[i]["star"] = "4";
            fivesList[i]["star"] = "5";






            ////如果有不为0的则345相同 tgroup
            //if (fivesList[i]["tgroup"].Equals("0") == false)
            //{
            //    fourList[i]["tgroup"] = fivesList[i]["tgroup"];
            //    threeList[i]["tgroup"] = fivesList[i]["tgroup"];
            //}
            //else if (fourList[i]["tgroup"].Equals("0") == false)
            //{
            //    fivesList[i]["tgroup"] = fourList[i]["tgroup"];
            //    threeList[i]["tgroup"] = fourList[i]["tgroup"];
            //}
            //else if (threeList[i]["tgroup"].Equals("0") == false)
            //{
            //    fivesList[i]["tgroup"] = threeList[i]["tgroup"];
            //    fourList[i]["tgroup"] = threeList[i]["tgroup"];
            //}


            ////如果有不为0的则345相同 hgroup
            //if (fivesList[i]["hgroup"].Equals("0") == false)
            //{
            //    fourList[i]["hgroup"] = fivesList[i]["hgroup"];
            //    threeList[i]["hgroup"] = fivesList[i]["hgroup"];
            //}
            //else if (fourList[i]["hgroup"].Equals("0") == false)
            //{
            //    fivesList[i]["hgroup"] = fourList[i]["hgroup"];
            //    threeList[i]["hgroup"] = fourList[i]["hgroup"];
            //}
            //else if (threeList[i]["hgroup"].Equals("0") == false)
            //{
            //    fivesList[i]["hgroup"] = threeList[i]["hgroup"];
            //    fourList[i]["hgroup"] = threeList[i]["hgroup"];
            //}

            ////有false则全false isActive
            //if (fivesList[i]["isActive"].Equals("false") || fourList[i]["isActive"].Equals("false") || threeList[i]["isActive"].Equals("false"))
            //{
            //    fivesList[i]["isActive"] = "false";
            //    fourList[i]["isActive"] = "false";
            //    threeList[i]["isActive"] = "false";
            //}

            // upType
            threeList[i]["upType"] = "1#3";
            fourList[i]["upType"] = "1#2";
            fivesList[i]["upType"] = "0";
            

            //   print("==" + fourList[i]["name"] + " " + threeList[i]["name"]);
        }






        //合并 3 4 5 星
        List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();


        for (int i = 1; i < threeList.Count; i++)
        {

            result.Add(threeList[i]);
        }

        for (int i = 1; i < fourList.Count; i++)
        {

            result.Add(fourList[i]);
        }

        for (int i = 1; i < fivesList.Count; i++)
        {

            result.Add(fivesList[i]);
        }




        TableLoader.Instance.OutToJson(result);


    }


    public void baseAbilityAuto(string[] baseName,string[] growName,int index,List<Dictionary<string,string>> threeList, List<Dictionary<string, string>>  fourList, List<Dictionary<string, string>> fivesList)
    {
        for (int i = 0; i < baseName.Length; i++)
        {
            float tempBase = float.Parse(threeList[index][baseName[i]]);
            float tempGrowAtk3 = float.Parse(threeList[index][growName[i]]);
            float tempGrowAtk4 = float.Parse(fourList[index][growName[i]]);

            fourList[index][baseName[i]] = (tempBase + tempGrowAtk3 * 16).ToString("0");

            print(">>>: " + (tempBase + tempGrowAtk3 * 16).ToString("0")  + "   "+ 12.6.ToString("0") );

            float tempBase4 = float.Parse( fourList[index][baseName[i]]);

            print(">>>Base4: " + tempBase4);

           fivesList[index][baseName[i]] = (tempBase4 + tempGrowAtk4 * 18).ToString("0");


            print(">>>Base5: " + fivesList[index][baseName[i]]);

        }



        //foreach (string temp in baseName)
        //{
        //    float tempBase = float.Parse(threeList[index][temp]);
        //    float tempGrowAtk = float.Parse(threeList[index]["growAtk"]);
        //    fourList[index][temp] =  ((int)(tempBase * 1.4f)).ToString();

        //    fourList[index][temp] =( tempBase + tempGrowAtk * 16).ToString();

        //    fivesList[index][temp] = ((int)(tempBase * 1.9f)).ToString();
        //}
    }


    public void growAbilityAuto(string[] value, int index, List<Dictionary<string, string>> threeList, List<Dictionary<string, string>> fourList, List<Dictionary<string, string>> fivesList)
    {
        foreach (string temp in value)
        {
            float tempInt = float.Parse(threeList[index][temp]);
            fourList[index][temp] = (tempInt * 1.6f).ToString("0.00");
            fivesList[index][temp] = (tempInt * 2f).ToString("0.00");
        }
    }

}
