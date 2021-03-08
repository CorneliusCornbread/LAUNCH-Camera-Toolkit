using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraPicture : MonoBehaviour
{
    [Tooltip("Set button for taking screenshots")]
    public Button takePictureButton;
    /// <summary>
    /// Change path for screenshots
    /// </summary>
    string path = @"C:/UnityScreenshots/";

    void Start()
    {
        Button btn1 = takePictureButton;
        btn1.onClick.AddListener(takePicture);
        if (!System.IO.File.Exists(path)) { 
        System.IO.Directory.CreateDirectory(path);
        }
    }

    private void takePicture()
    {
        var rand = Random.Range(0,1000);
        var rand2 = Random.Range(0,1000);
        ScreenCapture.CaptureScreenshot(path + "_" + Screen.width + "X" + Screen.height + "Y" + rand + rand2 + ".png");
        Debug.Log("Screenshot taken");
    }
    
}
