using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
                timeData =  DataService.Instance.dataModel.prodTime;
                timeStr = timeData.hrs + "hrs : " + timeData.mins + "mins"; 
                break;
            case TimeType.Break:
                 timeData = DataService.Instance.dataModel.breakTime;
                timeStr = timeData.hrs + "hrs : " + timeData.mins + "mins";
                break;
            case TimeType.UnAccounted:
                 timeData = DataService.Instance.dataModel.UATime;
                timeStr = timeData.hrs + "hrs : " + timeData.mins + "mins";
                break;
            default:                
                break;
        }



            transform.GetChild(3).GetComponent<TextMeshProUGUI>().text
                    = timeStr; 




    }

}
