using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class MouseClickLogger : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int VirtualKeyPressed);

    public bool isPressed = false;

    public TextMeshProUGUI mouseClickTxt;
    [SerializeField] int count = 0;

    private void Update()
    {

        if (!DataService.Instance.trackOn) return; // limited tracking 
        if (GetLeftMousePressed() && !isPressed)
        {
            Debug.Log("Left mouse pressed");
            count++;
            PopulateMouseClick();
            isPressed = true;
        }
    }

    public void PopulateMouseClick()
    {
        mouseClickTxt.text = count.ToString();
        DataService.Instance.dataModel.mouseClickCountMin = count;
       
    }

    public bool GetLeftMousePressed()
    {
        if (GetAsyncKeyState(0x01) == 0)
            return true;
        else
        {
            isPressed = false;
            return false;
        }

    }
}
