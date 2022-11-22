using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


public class PunchLogService : MonoSingletonGeneric<PunchLogService>
{
    /// <summary>
    /// Controll the break punch in and punch out
    /// break time 
    /// </summary>
    public event Action OnStartOfTheShift;
    public event Action OnEndOfTheShift;

    [Header("Date and Time")]
    public string theTime = "";
    public string theDate = "";
    public double theMonth = 0f;
    public string theDay = "";
    [SerializeField] string[] dateStrs;
    [SerializeField] string[] timeStrs;
    void Start()
    {    
        StartShift();      
    }

    public void StartShift()
    {     
        theTime = System.DateTime.Now.ToString("hh:mm:ss"); 
        theDate = System.DateTime.Now.ToString("MM/dd/yyyy");
     
        dateStrs = theDate.Split('-');
        timeStrs = theTime.Split(':');
        On_StartOfTheShift();
    }


    public void On_StartOfTheShift()
    {
        OnStartOfTheShift?.Invoke();
    }
    public void On_EndOfTheShift()
    {
        OnEndOfTheShift?.Invoke();  
    }
}
