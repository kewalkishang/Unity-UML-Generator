using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UMLMenu : EditorWindow
{
    [MenuItem("Window/UMLDiagram/Take ScreenShot")]
    private static void TakeScreenShot()
    {
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/", "-");
        date = date.Replace(" ", "_");
        string fileName = "UMLDiagram" + date +".png";// + System.DateTime.Now.ToString()+ ".png";
        ScreenCapture.CaptureScreenshot(fileName, 5);
    }

}
