using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System;
using TMPro;

public class KeyBoardLogger : MonoBehaviour
{

    public string keyLog = "";
    public int keyPressesCount = 0;
    [SerializeField] TextMeshProUGUI keyLogTxt;
    int prev = -1;
    [Header("Time Parameters")]
    float nextActionTime = 0.0f;
    float period = 0.1f;

    [Header("Counter")]
    [SerializeField] int count = 0;
    [SerializeField] TextMeshProUGUI keyCountTxt;

    [Header("Keyboard key press count")]
    [SerializeField] bool isPressed = false;
    [SerializeField] int keyPressed = -1;
    [DllImport("user32.dll")]
    static extern int GetAsyncKeyState(Int32 i);


    void Start()
    {

    }


    void PopulateKeyBoardCount()
    {
        int count = keyLog.Length;
        keyCountTxt.text = count.ToString();
        if (DataService.Instance.trackOn)  // Complete tracking so limitation set
        {
            DataService.Instance.dataModel.keyStrokesCountMin = keyPressesCount;
        }
        else
        {
            keyPressesCount = 0; 
        }
    }
  
    void Update()
    {
        //if (Time.time > nextActionTime)
        //{
        if (GetAsyncKeyState(keyPressed) != 0) return;

            nextActionTime += period;
        // execute block of code here
        for (int i = 32; i < 188; i++)
        {
            int keyState = GetAsyncKeyState(i);
            if (keyState != 0 && !isPressed)
            {
                Debug.Log("Keystat" + (char)i);
                keyPressesCount++;
                keyLog += (char)i;
                keyLogTxt.text = keyLog;
                isPressed = true;
                keyPressed = i;
                PopulateKeyBoardCount();
                
            }
            if (GetAsyncKeyState(keyPressed) == 0)
            {
                isPressed = false;
            }
        }      
    }
}
