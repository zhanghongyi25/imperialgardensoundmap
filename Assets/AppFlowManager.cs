using UnityEngine;
using UnityEngine.UI;
using System.Collections; 

public class AppFlowManager : MonoBehaviour
{
    [Header("UI 组引用")]
    public GameObject startScreenGroup; 
    public GameObject gameUIGroup;      

    [Header("逻辑引用")]
    public MapManager mapManager;       
    public SideMenuController sideMenu; 
    public TabController tabController; 

    [Header("设置")]
    public string libraryUrl = "https://your-sound-library-url.com"; 

    void Start()
    {
        // 1. 【彻底静音】只要在启动页，就让全局音量为 0
        AudioListener.volume = 0;

        // 2. 强制显示启动页，隐藏游戏UI
        startScreenGroup.SetActive(true);
        gameUIGroup.SetActive(false);

        // 3. 确保后台地图也是关的
        if (mapManager) mapManager.HideAllMaps();
    }

    // --- 按钮 A: 开始体验 ---
    public void OnClickStartExperience()
    {
        StartCoroutine(StartGameSequence());
    }

    // --- 【修复版】启动序列协程 ---
    IEnumerator StartGameSequence()
    {
        // 1. 切换 UI
        startScreenGroup.SetActive(false);
        gameUIGroup.SetActive(true); 

        // 2. 【核心修复】等待一帧
        // 这让 Unity 有时间去刷新 UI 布局，算出按钮的正确坐标
        yield return null; 
        
        // 3. 强制刷新 Canvas (防止玄学 UI Bug)
        Canvas.ForceUpdateCanvases();
        
        // 4. 【恢复声音】进入游戏了，打开音量
        AudioListener.volume = 1;

        // 5. 初始化游戏状态
        if (mapManager) 
        {
            // 先切到别的再切回来？不用，直接切 Spring 就可以
            // 关键是这一步会触发 UpdateCameraBounds
            mapManager.SwitchToSpring(); 
        }

        // 6. 修正顶部滑块位置
        if (tabController)
        {
            // 模拟点击一次，让滑块飞到正确位置
            tabController.OnClickSpring(); 
        }

        // 7. 确保右侧图标按钮状态正确
        if (sideMenu)
        {
            // 简单粗暴：如果不加这个，图标可能是隐藏的
            // 建议你在 SideMenuController 里加个 ForceShowIcons()，或者手动触发两次开关
            // 这里我们假设默认是显示的，不做多余操作，或者你可以调用:
            // sideMenu.OnClickToggleIcons(); 
        }
    }

    // --- 按钮 B: 启动页上的档案库 ---
    public void OnClickOpenLibrary()
    {
        Application.OpenURL(libraryUrl);
    }
}