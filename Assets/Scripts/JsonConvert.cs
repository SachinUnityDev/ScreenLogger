using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public class JsonConvert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveModel();
    }

    public class SampleData
    {
        public string response;        

        public SampleData(string res)
        {
            response = res; 
        }
    }
    public class HourlyData
    {
        public string username;
        public int prodMins;
        public int breakMins;
        public int uAmins;

        public HourlyData(string username, int prodMins, int breakMins, int uAmins)
        {
            this.username = username; 
            this.prodMins = prodMins;
            this.breakMins = breakMins;
            this.uAmins = uAmins;
        }
    }



    public void SaveModel()
    {
        SampleData sample = new SampleData("Logged in SuccessFully");

        HourlyData hourlyData = new HourlyData("sachin.s@maintec.in", 25, 10, 25); 

        string mydataPath = "/SAVE/Models.txt";
        Debug.Log(" INSIDE SAVE MODEL ");
        if (!File.Exists(Application.dataPath + mydataPath))
        {
            Debug.Log("does not exist");
            File.CreateText(Application.dataPath + mydataPath);
        }


        string sampleData = JsonUtility.ToJson(hourlyData);
        string saveStr = sampleData + "|";
        Debug.Log(saveStr);
        File.AppendAllText(Application.dataPath + mydataPath, saveStr);
    }
}
