using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using UnityEngine;
using System;
using TMPro;

public class AppLogger : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    string appName;
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] string appString = "";
    [SerializeField] string prevAppName = "";
    void Start()
    {

        appString = "";
    }

    void PrintAppName()
    {
        uint a = 0;

        IntPtr hwnd = GetForegroundWindow();

        GetWindowThreadProcessId(hwnd, out a);
        if (a == 0) return;
        Process p = Process.GetProcessById((int)a);

        appName = p.ProcessName;
        if (appName == prevAppName) return;
        if (appName != null)
        {
            UnityEngine.Debug.Log(appName);
            if (appName != prevAppName)
            {
                appString = appString + "\n" + appName;
                textMesh.text = appString;
                prevAppName = appName;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


            //       PrintAppName();

    }



}

