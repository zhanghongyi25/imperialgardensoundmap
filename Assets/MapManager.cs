using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("地图对象引用")]
    public GameObject springMap;   // 春夏地图父物体
    public GameObject winterMap;   // 冬雪地图父物体
    public GameObject historyMap;  // 历史地图父物体

    [Header("摄像机引用")]
    [Tooltip("请把挂了 CameraFollow 脚本的 Main Camera 拖进来")]
    public CameraFollow cameraFollow; 

    void Start()
    {
        // 【关键修改】游戏刚开始时，强制关闭所有地图
        // 这样启动页显示时，背景就是黑的（或者你自己的图），且没有任何地图声音
        HideAllMaps();
    }

    // --- 功能：关闭所有地图 (静音) ---
    public void HideAllMaps()
    {
        if (springMap) springMap.SetActive(false);
        if (winterMap) winterMap.SetActive(false);
        if (historyMap) historyMap.SetActive(false);
    }

    // --- 切换到：春夏 ---
    public void SwitchToSpring()
    {
        // 1. 显隐控制
        if (springMap) springMap.SetActive(true);
        if (winterMap) winterMap.SetActive(false);
        if (historyMap) historyMap.SetActive(false);

        // 2. 更新摄像机边界 (解决镜头卡住或乱跑的问题)
        UpdateCameraBounds(springMap);
    }

    // --- 切换到：冬雪 ---
    public void SwitchToWinter()
    {
        if (springMap) springMap.SetActive(false);
        if (winterMap) winterMap.SetActive(true);
        if (historyMap) historyMap.SetActive(false);

        UpdateCameraBounds(winterMap);
    }

    // --- 切换到：历史 ---
    public void SwitchToHistory()
    {
        if (springMap) springMap.SetActive(false);
        if (winterMap) winterMap.SetActive(false);
        if (historyMap) historyMap.SetActive(true);

        UpdateCameraBounds(historyMap);
    }

    // 辅助函数：通知摄像机更新边界
    private void UpdateCameraBounds(GameObject activeMap)
    {
        if (cameraFollow != null && activeMap != null)
        {
            // 获取当前激活地图上的 SpriteRenderer (用来计算边界)
            SpriteRenderer mapRenderer = activeMap.GetComponent<SpriteRenderer>();
            
            // 如果父物体上没挂 SpriteRenderer，尝试去第一个子物体找
            if (mapRenderer == null)
            {
                mapRenderer = activeMap.GetComponentInChildren<SpriteRenderer>();
            }

            // 把新的地图边界传给摄像机脚本
            if (mapRenderer != null)
            {
                cameraFollow.SetNewMap(mapRenderer);
            }
        }
    }
}