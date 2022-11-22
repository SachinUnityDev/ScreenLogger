using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataService : MonoSingletonGeneric<DataService>
{

    [Header("references")]
    public DataViewController dataViewController;

    public DataModel dataModel;
    public int keyStrokeBenchMarkMin = 5; 
    public int mouseBenchMarkMin = 5;


    [Header("Global Variables")]
    public bool trackOn = false;
    public bool breakOn = false;

    [SerializeField] float prevTime = 0f; 
    void Start()
    {
        TimeData timedata = new TimeData(10, 70);
        Debug.Log("timeData" + timedata.ConvertTime().mins);
        PunchLogService.Instance.OnStartOfTheShift += OnPunchIN; 

    }

    public void OnPunchIN()
    {
        dataModel = new DataModel();
        TrackMinReset();
       
    }
    public void OnPunchOut()
    {
        // upload data to the network


    }
    void TrackMinReset()
    {
        dataModel.mouseClickCountMin = 0; 
        dataModel.keyStrokesCountMin = 0;
    }

    public void UpdateTimeVals()
    {
        dataModel.mouseClickCountNet += dataModel.mouseClickCountMin;

        dataModel.keyStrokesCountNet += dataModel.keyStrokesCountMin;

        if (dataModel.mouseClickCountMin > mouseBenchMarkMin ||
             dataModel.keyStrokesCountMin > keyStrokeBenchMarkMin)
        {
            dataModel.prodTime.mins++;
            dataModel.prodTime.ConvertTime(); 
        }
        else if(breakOn)
        {
            dataModel.breakTime.mins++;
            dataModel.breakTime.ConvertTime();
        }
        else
        {
            dataModel.UATime.mins++;
            dataModel.UATime.ConvertTime();
        }
        dataViewController.PopulateTimeStats();
    }

    public void ToggleBreak(breakData breakData)
    {
        if (!breakOn)
        {
            dataModel.allBreakData.Add(breakData); 
        }
        breakOn = !breakOn;
        
    }

    private void Update()
    {
        if(Time.time > prevTime+ 40f)
        {
            trackOn = !trackOn;
            if (trackOn)
            {
                // send this after a min...this has a bug
                TrackMinReset();
            }
            else
            {

                UpdateTimeVals();
               
            }
                
            prevTime = Time.time;
        }
        
    }
}
