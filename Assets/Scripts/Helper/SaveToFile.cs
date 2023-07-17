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

        //  CaptureScreenshot();
        getAllFolderFiles();
    }

    public void CaptureScreenshot()
    {
        ScreenCapture.CaptureScreenshot(fileName, 5);
    }

    public void getAllFolderFiles()
    {
        string[] files;
        string assetPath = Application.dataPath;
        Debug.Log("Unity path " + assetPath);
        if (Directory.Exists(assetPath))
        {
            Debug.Log("Exists ");
            files = Directory.GetFiles(assetPath, @"*.cs", SearchOption.AllDirectories);
            Debug.Log("Exists "+ files.Length);
            for (int i = 0; i < files.Length;i++)
            {
                Debug.Log(Path.GetFileNameWithoutExtension(files[i]));
            }
        }
    }

}
