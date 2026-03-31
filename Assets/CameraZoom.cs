using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("缩放范围设置")]
    public float minSize = 5f;  // 放到最大 (数值越小，东西越大)
    public float maxSize = 8f;  // 缩到最小 (数值越大，东西越小)

    [Header("缩放速度")]
    public float zoomSpeedMobile = 0.5f; // 手机双指缩放的灵敏度
    public float zoomSpeedEditor = 2f;   // 电脑滚轮的灵敏度

    private Camera _cam;

    void Start()
    {
        _cam = GetComponent<Camera>();
        // 确保一开始的大小在范围内
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, minSize, maxSize);
    }

    void Update()
    {
        // ----------------------------------------
        // 1. 电脑端：鼠标滚轮缩放 (方便调试)
        // ----------------------------------------
        if (Application.isEditor || Input.mousePresent)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                // 滚轮向上(正) -> Size变小 -> 放大
                _cam.orthographicSize -= scroll * zoomSpeedEditor;
            }
        }

        // ----------------------------------------
        // 2. 手机端：双指捏合缩放
        // ----------------------------------------
        if (Input.touchCount == 2)
        {
            // 获取两根手指
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 计算手指上一帧的位置
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 计算两根手指之前的距离 和 现在的距离
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 距离差值 (如果差值是正的，说明手指在靠近 -> 缩小 -> Size变大)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 应用缩放
            _cam.orthographicSize += deltaMagnitudeDiff * zoomSpeedMobile * Time.deltaTime;
        }

        // ----------------------------------------
        // 3. 限制范围 (关键！)
        // ----------------------------------------
        // 无论怎么缩放，都死死限制在 5 到 8 之间
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, minSize, maxSize);
    }
}