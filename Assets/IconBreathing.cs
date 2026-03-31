using UnityEngine;

public class IconBreathing : MonoBehaviour
{
    [Header("呼吸设置")]
    [Tooltip("缩到最小时，是原始大小的多少倍？(例如 0.8 就是 80%)")]
    public float minScale = 0.9f; 

    [Tooltip("放到最大时，是原始大小的多少倍？(例如 1.1 就是 110%)")]
    public float maxScale = 1.1f; 

    [Tooltip("呼吸的速度 (数值越大越快)")]
    public float speed = 3.0f;

    private Vector3 _originalScale;

    void Start()
    {
        // 游戏开始时，先记住它原本是多大
        _originalScale = transform.localScale;
    }

    void Update()
    {
        // 1. 利用 Sin 函数制造平滑的波动 (结果在 -1 到 1 之间变化)
        float wave = Mathf.Sin(Time.time * speed);

        // 2. 把波动映射到 0 到 1 的范围，方便做插值
        // wave + 1 变成 0~2，再除以 2 变成 0~1
        float adjustFactor = (wave + 1f) / 2f;

        // 3. 计算当前应该缩放的倍数 (在 minScale 和 maxScale 之间来回变)
        float currentMultiplier = Mathf.Lerp(minScale, maxScale, adjustFactor);

        // 4. 应用缩放
        transform.localScale = _originalScale * currentMultiplier;
    }
}