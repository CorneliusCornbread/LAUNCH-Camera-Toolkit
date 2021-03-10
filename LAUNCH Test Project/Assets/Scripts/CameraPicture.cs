using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CameraPicture : MonoBehaviour
{
    [Tooltip("The full output path for the file.")]
    public string path = @"C:/UnityScreenshots/";

    private void Start()
    {
        if (!System.IO.File.Exists(path)) 
        { 
            System.IO.Directory.CreateDirectory(path);
        }
    }

    public void TakePicture()
    {
        string output = path + "_" + Screen.width + "X" + Screen.height + "Y" + DateTime.Now.ToString().Replace('/', '-').Replace(':', '-');

        if (System.IO.File.Exists(output))
        {
            output += "-" + Guid.NewGuid().ToString();
        }

        output += ".png";

        ScreenCapture.CaptureScreenshot(output);
        Debug.Log($"Screenshot taken: {output}");
    }

    #region Editor Functions
#if UNITY_EDITOR
    [ContextMenu("Take Screenshot")]
    private void EditorTakePicture()
    {
        TakePicture();
    }
#endif
    #endregion
}
