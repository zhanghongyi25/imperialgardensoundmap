using UnityEngine;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour
{
    [Header("设置")]
    public string libraryUrl = "https://your-sound-library-url.com";
    
    [Header("UI 绑定")]
    public Text toggleBtnText;   // 拖入按钮下面的 Text 组件
    public Image toggleBtnBg;    // 拖入按钮自己的 Image 组件

    // 内部状态 (默认一开始是显示的)
    private bool areIconsVisible = true; 

    void Start()
    {
        // 游戏刚开始，初始化一次状态
        UpdateIconState();
    }

    // --- 右下角：打开档案库 ---
    public void OnClickOpenLibrary()
    {
        Application.OpenURL(libraryUrl);
    }

    // --- 右上角：显隐开关 ---
    public void OnClickToggleIcons()
    {
        // 1. 切换状态 (如果是真变假，如果是假变真)
        areIconsVisible = !areIconsVisible;

        // 2. 执行更新
        UpdateIconState();
    }

    // --- 核心逻辑 ---
    void UpdateIconState()
    {
        // A. 真正的去显示/隐藏地图上的物体
        SoundPoint[] allPoints = FindObjectsOfType<SoundPoint>(true); 
        foreach (SoundPoint point in allPoints)
        {
            // 这里根据你的实际情况，如果是 CanvasGroup 就改 alpha，如果是 SpriteRenderer 就改 enabled
            // 这里假设你是用的 CanvasGroup 做显隐
            CanvasGroup cg = point.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.alpha = areIconsVisible ? 1 : 0; // 显示设为1，隐藏设为0
                cg.interactable = areIconsVisible;
                cg.blocksRaycasts = areIconsVisible;
            }
            // 如果你的声源是直接挂 SpriteRenderer 的，解开下面这行的注释：
            // else { if(point.GetComponent<SpriteRenderer>()) point.GetComponent<SpriteRenderer>().enabled = areIconsVisible; }
        }

        // B. 【关键修改】更新按钮上的文字和颜色
        if (toggleBtnText && toggleBtnBg)
        {
            if (areIconsVisible)
            {
                // 状态：现在是显示的
                // 动作：按钮应该提示“去隐藏”
                toggleBtnText.text = "🚫 隐藏声源"; 
                toggleBtnBg.color = Color.white; // 亮色表示激活/开启状态
            }
            else
            {
                // 状态：现在是隐藏的
                // 动作：按钮应该提示“去显示”
                toggleBtnText.text = "👁️ 显示声源";
                toggleBtnBg.color = new Color(0.8f, 0.8f, 0.8f); // 稍微变灰一点表示关闭状态
            }
        }
    }
}