using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 


public class DataViewController : MonoBehaviour
{
    [Header("Not to be ref")]
    [SerializeField] Transform prodTime;
    [SerializeField] Transform breakTime;
    [SerializeField] Transform unAccTime; 

    void Start()
    {
        prodTime =transform.GetChild(0);
        breakTime =transform.GetChild(1);
        unAccTime =transform.GetChild(2);
    }

    public void PopulateTimeStats()
    {
        DataModel dataModel = DataService.Instance.dataModel; 
        foreach(Transform child in transform)
        {
            child.GetComponent<DataModuleView>().PopulateData();
        }

    }

    void PopulateEachModule(DataModel dataModel)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
