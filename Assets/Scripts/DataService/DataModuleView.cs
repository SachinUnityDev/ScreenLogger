using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class DataModuleView : MonoBehaviour
{

    [SerializeField] TimeType timeType; 
    void Start()
    {
    }

    public void PopulateData()
    {
        string timeStr = "";
        TimeData timeData = null; 
        switch (timeType)
        {
            case TimeType.Prod:
                timeData =  DataService.Instance.dataModel.prodTime.ConvertTime();
                timeStr = timeData.hrs + " hrs : " + timeData.mins + " mins"; 
                break;
            case TimeType.Break:
                 timeData = DataService.Instance.dataModel.breakTime.ConvertTime(); 
                timeStr = timeData.hrs + " hrs : " + timeData.mins + " mins";
                break;
            case TimeType.UnAccounted:
                 timeData = DataService.Instance.dataModel.UATime.ConvertTime(); 
                timeStr = timeData.hrs + " hrs : " + timeData.mins + " mins";
                break;
            default:                
                break;
        }
            transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
                    = timeStr;
        PopulateCircleNPercent(timeData);
    }

    void PopulateCircleNPercent(TimeData timeData)
    {
        // benchmark 8 hrs
        float netWorkingMins = (float)TimeSpan.FromHours(8f).TotalMinutes;

        float calcFraction = (((float)TimeSpan.FromHours(timeData.hrs).TotalMinutes) + timeData.mins)/netWorkingMins;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = calcFraction;
        transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text
            = Mathf.Round(calcFraction * 100f) + "%";  

    }



}
