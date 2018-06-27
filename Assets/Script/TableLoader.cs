using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using Excel;
using System.Data;
using Newtonsoft.Json;
#else
using Newtonsoft.Json;

#endif

public class TableLoader : MonoBehaviour
{
    public static TableLoader Instance { get; private set; }



    public string assetsFolderName;
    public string excelFolderName = "bbq" + ".xlsx";
    public string excelName;
    public string jsonFolderName;
    public string jsonName;

    private string streamingPath;
    public  int nameRowIndex = 0;
    public int infoRowIndex = 1;
    public int typeRowIndex = 2;
    public int dataRowIndex = 3;


    public Dictionary<string, List<Dictionary<string, string>>> tableDict;


#if UNITY_EDITOR
    DataSet resultSet;
#endif
    private void Awake()
    {
        Instance = this;
        tableDict = new Dictionary<string, List<Dictionary<string, string>>>();
#if UNITY_EDITOR
        InitInEdit();
#else
        InitInWindows();
#endif
    }




    private void InitInWindows()
    {
        List<string> sheetNameList = new List<string>();
        sheetNameList.Add("FoodDataTable");

        streamingPath = Application.streamingAssetsPath + "/" + jsonFolderName + "/" + sheetNameList[0] + ".json";
        string tempJson = File.ReadAllText(streamingPath);
        List<Dictionary<string, string>> tempSheet = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(tempJson);
        tableDict.Add(sheetNameList[0], tempSheet);

        print(">>Path :" + streamingPath);
        print(">>tempSheet :" + tempSheet.Count);

    }



    //void Start()
    //{


    //    List<Dictionary<string, string>> sheetData = ReadExcel(resultSet, "FoodDataTable");

    //   print( "==== "  +FoodData.ReadTable(sheetData)[1003].foodName);
    //}
    public List<Dictionary<string, string>> GetSheetData(string sheetName)
    {
        return tableDict[sheetName]; 
    }





#if UNITY_EDITOR
    /// <summary>
    /// 在Dataset文件中读取指定名字的Sheet
    /// </summary>
    /// <param name="resultSet"></param>
    /// <param Sheet名="sheetName"></param>
    /// <returns></returns>
    public List<Dictionary<string, string>> ReadExcel(DataSet resultSet, string sheetName)
    {

        if (resultSet == null)
        {
            print(">> resultSet is null");
        }

        int sheetCount = resultSet.Tables.Count;

        //判断Excel文件中是否存在数据表
        if (sheetCount < 1)
            return null;

        Debug.Log("总共有:" + sheetCount + "个Sheet 将读取" + sheetName);

        DataTable mSheet = null;
        //查找对应名字的Sheet
        foreach (DataTable tempDT in resultSet.Tables)
        {
            //查找Sheet名字，忽略大小写
            if (tempDT.TableName.Equals(sheetName, StringComparison.OrdinalIgnoreCase))
            {
                mSheet = tempDT;

                break;

            }
        }
        if (mSheet == null)
        {
            Debug.LogError("没有找到对应名字的Sheet :" + sheetName);
        }


        //判断数据表内是否存在数据
        if (mSheet.Rows.Count < 1)
        {
            Debug.Log("该表格似乎为空，请检查内容！");
            return null;
        }

        //读取数据表行数和列数
        int rowCount = mSheet.Rows.Count;
        int colCount = mSheet.Columns.Count;

        Debug.Log("这个Sheet一共有:" + "rowCount:" + rowCount + "colCount:" + colCount);

        //准备一个列表存储整个表的数据
        List<Dictionary<string, string>> table = new List<Dictionary<string, string>>();


        //读取数据类型
        Dictionary<string, string> typeRow = new Dictionary<string, string>();
        for (int j = 0; j < colCount; j++)
        {
            //读取第1行数据作为表头字段
            string field = mSheet.Rows[nameRowIndex][j].ToString();
            //读取数据类型
            string dataType = mSheet.Rows[typeRowIndex][j].ToString();


            //如果字段为空，则说明该字段时注释将被忽略
            if (field.Equals(""))
            {
                //   colCount--;
                continue;
            }
            else
            {
                //Key-Value对应
                typeRow[field] = dataType;
            }

        }
        table.Add(typeRow);

            //读取数据
        for (int i = dataRowIndex; i < rowCount; i++)
        {
            //准备一个字典存储每一行的数据
            Dictionary<string, string> row = new Dictionary<string, string>();
            for (int j = 0; j < colCount; j++)
            {
                //读取第1行数据作为表头字段
                string field = mSheet.Rows[nameRowIndex][j].ToString();
                //读取数据类型
                string dataType = mSheet.Rows[typeRowIndex][j].ToString();




                //如果字段为空，则说明该字段时注释将被忽略
                if (field.Equals(""))
                {
                    //   colCount--;
                    continue;
                }
                else
                {
                    //Key-Value对应
                    row[field] = mSheet.Rows[i][j].ToString();
                }

            }

            //添加到表数据中
            table.Add(row);
        }

        return table;
    }


    private void InitInEdit()
    {
        //初始化
        streamingPath = Application.streamingAssetsPath + @"/TableJson/";
        //注意这里需要对路径进行处理
        //目的是去除Assets这部分字符以获取项目目录
        //  pathRoot = pathRoot.Substring(0, pathRoot.LastIndexOf("/"));
        //获取Excel文件的绝对路径
        string excelPath = Application.dataPath + "/" + assetsFolderName + "/" + excelFolderName + "/" + excelName;

        print(">>Path:" + excelPath);

        FileStream excelFileStream = File.Open(excelPath, FileMode.Open, FileAccess.Read);

        if (excelFileStream == null)
        {
            print(">>excelFileStream  is Null");
        }


        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(excelFileStream);

        if (excelReader == null)
        {
            print(">>excelReader  is Null");
        }
        resultSet = excelReader.AsDataSet(false);


        if (resultSet == null)
        {
            print(">>resultSet  is Null");
        }
    }


    public List<Dictionary<string, string>> GetSheetDataInEditor(string sheetName)
    {

        List<Dictionary<string, string>> sheetData = ReadExcel(resultSet, sheetName);


        //生成Json字符串
        string tempJson = JsonConvert.SerializeObject(sheetData, Newtonsoft.Json.Formatting.Indented);


        string tempPath = Application.dataPath + "/" + assetsFolderName + "/" + jsonFolderName + "/" + jsonName;

        //string head = "\"data\":";
        //tempJson ="{"+ head+ tempJson+"}";

        //写入文件
        using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        {

            using (TextWriter textWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                textWriter.Write(tempJson);
            }
        }


        //print(tempJson);

        return sheetData;
    }



    public void OutToJson(List<Dictionary<string, string>> sheetData)
    {


        //生成Json字符串
        string tempJson = JsonConvert.SerializeObject(sheetData, Newtonsoft.Json.Formatting.Indented);


        string tempPath = Application.dataPath + "/" + assetsFolderName + "/" + jsonFolderName + "/" + jsonName;

        //string head = "\"data\":";
        //tempJson ="{"+ head+ tempJson+"}";

        //写入文件
        using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
        {

            using (TextWriter textWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                textWriter.Write(tempJson);
            }
        }


    }


#endif
}
