using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 

public class ScreenLogger : MonoBehaviour
{
    float prevTime = 0f; 
    float nextTime = 0f;
    int count =1;
    void Start()
    {
        
    }

    void ScreenShot()
    {
        var tex = DesktopScreenshot.Capture(DesktopScreenshot.GetDesktopBounds());
        System.IO.File.WriteAllBytes("screenshot.png", tex.EncodeToPNG());
        Debug.Log(Application.persistentDataPath);

    }

    void ScreenShot2()
    {
        var tex = DesktopScreenshot.Capture(DesktopScreenshot.GetDesktopBounds());
        count++; 
        //byte[] itemBGBytes = tex.EncodeToPNG();
        //File.WriteAllBytes(formattedCampaignPath + "/Item Images/Background.png", itemBGBytes);
        // For testing purposes, also write to a file in the project folder
         File.WriteAllBytes(Application.dataPath + $"/SAVE/SavedScreen{count}.png", tex.EncodeToPNG());


    }
    // Update is called once per frame
    void Update()
    {
        
        if(Time.time - prevTime > 500f)
        {
            ScreenShot2();
            prevTime = Time.time;
        }

            
        
    }
}
