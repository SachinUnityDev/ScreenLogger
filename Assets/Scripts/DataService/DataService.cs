using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

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
        
    }

    public void OnPunchIN()
    {
        dataModel = new DataModel();
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

        if (dataModel.mouseClickCountMin > mouseBenchMarkMin &&
             dataModel.keyStrokesCountMin > keyStrokeBenchMarkMin)
        {
            dataModel.prodTime.mins++;
            dataModel.prodTime.RationTime(); 

        }
        else if(breakOn)
        {
            dataModel.breakTime.mins++;
            dataModel.breakTime.RationTime();
        }
        else
        {
            dataModel.UATime.mins++;
            dataModel.UATime.RationTime();
        }
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
        if(Time.time > prevTime+ 10f)
        {
            trackOn = !trackOn;
            if (trackOn)
            {
                TrackMinReset();
            }
            else
            {
                UpdateTimeVals();
                dataViewController.PopulateTimeStats();
            }
                
            prevTime = Time.time;
        }
        
    }
}