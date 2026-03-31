using UnityEngine;
using System.IO; // 处理文件路径

public class LibraryManager : MonoBehaviour
{
    // 如果你有在线网址，填在这里最省事
    public string onlineUrl = ""; 
    
    // 如果没有，就填你放在 StreamingAssets 里的文件名，比如 "library.html"
    public string localFileName = "index.html"; 

    public void OpenLibrary()
    {
        string finalPath = "";

        // 优先检查有没有填在线网址
        if (!string.IsNullOrEmpty(onlineUrl))
        {
            finalPath = onlineUrl;
        }
        else
        {
            // 处理本地路径 (分平台)
            string filePath = Path.Combine(Application.streamingAssetsPath, localFileName);
            
            // 在 Android/iOS 上，本地 HTML 有时无法直接通过 file:// 打开
            // 如果你在电脑上测试，下面这行是可以的：
            finalPath = "file://" + filePath;
        }

        // 调用系统浏览器打开
        Application.OpenURL(finalPath);
    }
}