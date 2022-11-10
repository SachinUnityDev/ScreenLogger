using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Windows;


public class NewBehaviourScript : MonoBehaviour
{

    [DllImport("gdi32.dll")]
    static extern bool BitBlt(IntPtr hdcDest, int nxDest, int nyDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int nHeight);

    [DllImport("gdi32.dll")]
    static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    static extern IntPtr DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll")]
    static extern IntPtr DeleteObject(IntPtr hObject);

    [DllImport("user32.dll")]
    static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll")]
    static extern IntPtr GetWindowDC(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

    [DllImport("gdi32.dll")]
    static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

    // import DLL unless that it will not work 
    const int SRCCOPY = 0x00CC0020;

    const int CAPTUREBLT = 0x40000000;
   
    public void BitMapConvertor()
    {
        //MemoryStream msFinger = new MemoryStream();
        //bitmapCurrentframeRed.Save(msFinger, bitmapCurrentframeRed.RawFormat);
        //redCamera.LoadImage(msFinger.ToArray());
        //redFilter.GetComponent<Renderer>().material.mainTexture = redCamera;

    }

    public Bitmap CaptureRegion(Rectangle region)
    {
        IntPtr desktophWnd;
        IntPtr desktopDc;
        IntPtr memoryDc;
        IntPtr bitmap;
        IntPtr oldBitmap;
        bool success;
        Bitmap result;

        desktophWnd = GetDesktopWindow();
        desktopDc = GetWindowDC(desktophWnd);
        memoryDc = CreateCompatibleDC(desktopDc);
        bitmap = CreateCompatibleBitmap(desktopDc, region.Width, region.Height);
        oldBitmap = SelectObject(memoryDc, bitmap);

        success = BitBlt(memoryDc, 0, 0, region.Width, region.Height, desktopDc, region.Left, region.Top, SRCCOPY | CAPTUREBLT);

        try
        {
            if (!success)
            {
                throw new Win32Exception();
            }

            result = Image.FromHbitmap(bitmap);
        }
        finally
        {
            SelectObject(memoryDc, oldBitmap);
            DeleteObject(bitmap);
            DeleteDC(memoryDc);
            ReleaseDC(desktophWnd, desktopDc);
        }

        return result;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




