using UnityEngine;
using UnityEngine.UI; // 引用 UI

public class TabController : MonoBehaviour
{
    [Header("UI 组件")]
    public RectTransform glider; // 那个滑动的蓝色胶囊
    public RectTransform[] buttons; // 三个按钮的位置 (0=春, 1=冬, 2=历史)

    [Header("引用地图管理器")]
    public MapManager mapManager; // 用来切换地图

    [Header("动画设置")]
    public float slideSpeed = 10f; // 滑动速度

    private Vector3 targetPosition; // 滑块的目标位置

    void Start()
    {
        // 游戏开始时，默认停在第一个按钮的位置
        if (buttons.Length > 0)
        {
            targetPosition = buttons[0].position;
        }
    }

    void Update()
    {
        // 让滑块每一帧都向目标位置平滑移动 (Lerp插值)
        // 这种写法能产生非常柔和的减速停车效果
        glider.position = Vector3.Lerp(glider.position, targetPosition, slideSpeed * Time.deltaTime);
    }

    // --- 绑定给按钮的点击事件 ---

    public void OnClickSpring()
    {
        targetPosition = buttons[0].position; // 目标设为第1个按钮的位置
        if (mapManager) mapManager.SwitchToSpring(); // 切换地图
    }

    public void OnClickWinter()
    {
        targetPosition = buttons[1].position; // 目标设为第2个按钮的位置
        if (mapManager) mapManager.SwitchToWinter();
    }

    public void OnClickHistory()
    {
        targetPosition = buttons[2].position; // 目标设为第3个按钮的位置
        if (mapManager) mapManager.SwitchToHistory();
    }
}