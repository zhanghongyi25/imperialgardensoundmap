using UnityEngine;
// 需要引入 UI 系统，用于判断是否点在了按钮上
using UnityEngine.EventSystems; 

public class CameraFollow : MonoBehaviour
{
    [Header("设置")]
    public Transform target; // 主角 (Player)
    [Tooltip("当前正在使用的地图图片 SpriteRenderer")]
    public SpriteRenderer currentMapRenderer;
    public float smoothSpeed = 5f; // 跟随平滑度

    [Header("拖拽设置")]
    public float dragSpeed = 1f; // 拖拽灵敏度

    private Camera _cam;
    private float _camHeight, _camWidth;
    // 地图边界数据
    private float _mapMinX, _mapMaxX, _mapMinY, _mapMaxY;

    // --- 【新增变量】 ---
    private Vector3 _dragOrigin; // 记录手指按下的位置
    private bool _isDragging = false; // 当前是否正在拖拽中
    private Vector3 _desiredPosition; // 摄像机期望到达的目标位置

    void Start()
    {
        _cam = GetComponent<Camera>();
        if (currentMapRenderer != null)
        {
            CalculateBounds(currentMapRenderer);
        }
        // 初始化期望位置为当前位置
        _desiredPosition = transform.position;
    }

    // 公开方法：更新地图边界 (给 MapManager 调用)
    public void SetNewMap(SpriteRenderer newMap)
    {
        currentMapRenderer = newMap;
        CalculateBounds(newMap);
    }

    private void CalculateBounds(SpriteRenderer renderer)
    {
        if (renderer == null) return;
        _mapMinX = renderer.bounds.min.x;
        _mapMaxX = renderer.bounds.max.x;
        _mapMinY = renderer.bounds.min.y;
        _mapMaxY = renderer.bounds.max.y;
    }

    // --- 【新增 Update：处理玩家拖拽输入】 ---
    void Update()
    {
        // 如果点在了 UI 按钮上（比如摇杆或顶部按钮），就不触发拖拽地图
        if (EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
        {
             return;
        }

        // 1. 检测按下 (鼠标左键 或 单指触摸开始)
        if (Input.GetMouseButtonDown(0))
        {
            // 记录起始点（屏幕坐标转世界坐标）
            _dragOrigin = _cam.ScreenToWorldPoint(Input.mousePosition);
            _isDragging = true;
        }

        // 2. 检测按住并拖动 (且只有一根手指时，避免和缩放冲突)
        if (Input.GetMouseButton(0) && _isDragging && Input.touchCount < 2)
        {
            // 计算当前手指位置的世界坐标
            Vector3 currentPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            // 计算手指移动的差值向量
            Vector3 difference = _dragOrigin - currentPos;

            // 【核心逻辑】把这个差值加到摄像机身上
            // 比如手指往左滑，difference 是负的，摄像机位置加上负数就往左移，实现了拖拽地图的效果
            transform.position += difference * dragSpeed;
            
            // 告诉后面的逻辑：现在听我的，别听主角的
            _desiredPosition = transform.position;
        }

        // 3. 检测松开
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    // --- LateUpdate：处理跟随和边界限制 (已修改) ---
    void LateUpdate()
    {
        if (currentMapRenderer == null) return;

        // 【核心修改】：只有在没有拖拽的时候，才更新目标为主角的位置
        if (!_isDragging && target != null)
        {
            _desiredPosition = target.position;
        }

        // --- 以下是原有的边界计算和跟随逻辑 ---

        // 实时计算摄像机视野大小
        _camHeight = _cam.orthographicSize;
        _camWidth = _camHeight * _cam.aspect;

        // 确保 Z 轴正确
        Vector3 targetPosWithZ = _desiredPosition;
        targetPosWithZ.z = -10;

        // 使用边界数据进行限制 (Clamp)
        // 无论是由主角决定的位置，还是刚才拖拽出来的位置，都要经过这一步检查
        float clampedX = Mathf.Clamp(targetPosWithZ.x, _mapMinX + _camWidth, _mapMaxX - _camWidth);
        float clampedY = Mathf.Clamp(targetPosWithZ.y, _mapMinY + _camHeight, _mapMaxY - _camHeight);

        Vector3 finalPosition = new Vector3(clampedX, clampedY, -10);

        // 平滑移动到最终计算出的合法位置
        // 如果正在拖拽，平滑速度快一点(看起来更跟手)，否则慢一点(电影感)
        float currentSmooth = _isDragging ? smoothSpeed * 5 : smoothSpeed;
        transform.position = Vector3.Lerp(transform.position, finalPosition, currentSmooth * Time.deltaTime);
    }
}