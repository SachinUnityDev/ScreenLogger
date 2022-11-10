using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RationaliseTime 
{
    public static TimeData RationTime(this TimeData timeData)
    {
        
        if(timeData.mins % 60 >= 1)
        {
            float hrsAdded = timeData.mins % 60;
            Debug.Log("Hours added" + hrsAdded);
            float minsNow = timeData.mins - hrsAdded * 60; 
            TimeData _timeData = new TimeData(timeData.hrs + hrsAdded, minsNow);             
            
        }


        return null; 
    }
}
