using UnityEngine;
using UnityEngine.UI; // 必须引用 UI 库

public class UIManager : MonoBehaviour
{
    [Header("把做好的 UI 组件拖进去")]
    public GameObject infoPanel; // 那个大的面板
    public Text titleText;       // 标题文字
    public Text contentText;     // 内容文字

    // 单例模式：让别的脚本很容易找到它
    public static UIManager Instance;

    void Awake()
    {
        Instance = this;
    }

    // 这个函数给声源调用：用来显示信息
    public void ShowInfo(string title, string content)
    {
        infoPanel.SetActive(true); // 打开面板
        titleText.text = title;    // 更新标题
        contentText.text = content;// 更新内容
    }

    // 这个函数给关闭按钮调用
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
    }
}