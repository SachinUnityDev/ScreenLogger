using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TimeData
{
    public float hrs;
    public float mins;

    public TimeData(float hrs, float mins)
    {
        this.hrs = hrs;
        this.mins = mins;
    }
}

public class breakData
{
    public BreakType breakType;
    public string reasonStr = "";

    public breakData(BreakType breakType, string reasonStr)
    {
        this.breakType = breakType;
        this.reasonStr = reasonStr;
    }
}

[System.Serializable]
public class DataModel 
{    
    public int mouseClickCountMin =0;
    public int mouseClickCountNet = 0;
    public int keyStrokesCountMin = 0;
    public int keyStrokesCountNet = 0; 
    public List<string> websitesVisited = new List<string>();
    public List<string> appsVisited = new List<string>();
    public List<breakData> allBreakData = new List<breakData>();
    public TimeData prodTime;    
    public TimeData breakTime;
    public TimeData UATime; 
   
    public DataModel()
    {
        this.mouseClickCountMin = 0;
        this.mouseClickCountNet = 0;
        this.keyStrokesCountMin = 0;
        this.keyStrokesCountNet = 0;
        prodTime = new TimeData(0, 0); 
        breakTime = new TimeData(0, 0);
        UATime = new TimeData(0, 0);



        websitesVisited.Clear();
        appsVisited.Clear();
    }
}
