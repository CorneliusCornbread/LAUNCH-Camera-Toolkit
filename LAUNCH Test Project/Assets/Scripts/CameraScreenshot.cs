using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class CameraScreenshot : MonoBehaviour
{
    [Tooltip("Whether or not the folder is an absolute path or a subfolder of Application.persistentDataPath.")]
    public bool usePersistentDataPath = false;
    
    [Tooltip("The absolute path or subfolder the screenshot will be saved to.")]
    public string folder = @"C:/Unity Screenshots";
    
    public Camera targetCamera;
    
    private void Start()
    {
        if (!File.Exists(folder)) 
        { 
            Directory.CreateDirectory(folder);
        }
    }

    /// <summary>
    /// Gets output path as a string.
    /// </summary>
    /// <returns>Output path.</returns>
    public string GetOutputPath()
    {
        return GetOutputPathBuilder().ToString();
    }
    
    /// <summary>
    /// Returns the output path as a string builder.
    /// </summary>
    /// <returns>Reference to string builder with output path.</returns>
    public StringBuilder GetOutputPathBuilder()
    {
        StringBuilder str = new StringBuilder();

        if (usePersistentDataPath)
        {
            str.Append(Application.persistentDataPath);
            str.Append(@"/");
        }
        
        str.Append(folder);
        str.Append(@"/");

        return str;
    }
    
    public void TakePicture()
    {
        StringBuilder str = GetOutputPathBuilder();

        Directory.CreateDirectory(str.ToString());

        string time = DateTime.Now.ToString(CultureInfo.CurrentCulture)
            .Replace('/', '-')
            .Replace(':', '-');
        str.Append($"{Application.productName}_{Screen.width}x{Screen.height}_{time}");

        if (File.Exists(str.ToString()))
        {
            str.Append("-");
            str.Append(Guid.NewGuid());
        }

        str.Append(".png");
        
        string output = str.ToString();

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = targetCamera.targetTexture;
 
        targetCamera.Render();

        RenderTexture targetTexture = targetCamera.targetTexture;
        Texture2D image = new Texture2D(targetTexture.width, targetTexture.height);
        image.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRT;
 
        byte[] bytes = image.EncodeToPNG();
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            Destroy(image);
        }
        else
        {
            DestroyImmediate(image);
        }
#else
        Destroy(image);
#endif

        File.WriteAllBytes(output, bytes);

        Debug.Log($"Screenshot taken: {output}");
    }

    #region Editor Functions
#if UNITY_EDITOR
    [ContextMenu("Take Screenshot")]
    private void EditorTakePicture()
    {
        TakePicture();
    }

    [ContextMenu("Debug Output Path")]
    private void DebugPath()
    {
        Debug.Log($"Screenshot output path: {GetOutputPath()}");
    }

    [ContextMenu("Open Output Folder")]
    private void OpenOutputFolder()
    {
        string folderPath = GetOutputPath();
        
        if (Directory.Exists(folderPath))
        {
            EditorUtility.RevealInFinder(folderPath);
        }
        else
        {
            Debug.Log($"Folder does not exist: {folderPath}");
        }
    }
#endif
    #endregion
}
