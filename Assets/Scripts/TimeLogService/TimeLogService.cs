using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 


public class TimeLogService : MonoSingletonGeneric<TimeLogService>
{
    /// <summary>
    /// Controll the break punch in and punch out
    /// break time 
    /// </summary>
    public event Action OnStartOfTheShift;
    public event Action OnEndOfTheShift;

    public TimeModel timeModel;

    public bool breakOn = false;


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
        //Debug.Log(theMonth);
    }

    void StartShift()
    {
        timeModel = new TimeModel();
        theTime = System.DateTime.Now.ToString("hh:mm:ss"); 
        theDate = System.DateTime.Now.ToString("MM/dd/yyyy");
     
        dateStrs = theDate.Split('-');
        timeStrs = theTime.Split(':'); 

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
