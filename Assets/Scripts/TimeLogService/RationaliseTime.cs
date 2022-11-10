using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class RationaliseTime 
{
    public static TimeData ConvertTime(this TimeData timeData)
    {
        float mins = timeData.mins;
        float hrsAdd = TimeSpan.FromMinutes(mins).Hours;
        float hrs = timeData.hrs +hrsAdd;

        mins = mins - hrsAdd * 60f;

        TimeData timeDataNew = new TimeData(hrs, mins);
        return timeDataNew;
    }
}
