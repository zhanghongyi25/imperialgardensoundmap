using UnityEngine;

public class SoundPoint : MonoBehaviour
{
    [Header("在这里填写这个声源的信息")]
    public string pointName = "声源名称";
    [TextArea] 
    public string description = "在这里写史料、年份、文化内涵...";

    // 当鼠标点击这个物体时触发
    void OnMouseDown()
    {
        // 调用 UIManager 显示信息
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowInfo(pointName, description);
        }
    }
}