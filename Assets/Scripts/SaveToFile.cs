using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveToFile : MonoBehaviour
{
    public string fileName = "UMLdiagram.png";
    // Start is called before the first frame update
    void Start()
    {

        CaptureScreenshot();
    }

    public void CaptureScreenshot()
    {
        ScreenCapture.CaptureScreenshot(fileName, 5);
    }


}
