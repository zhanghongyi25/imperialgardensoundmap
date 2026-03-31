using UnityEngine;
using HearXR; // 1. 引用正确的命名空间

public class PlayerController : MonoBehaviour
{
    [Header("输入源")]
    public FixedJoystick joystick;

    [Header("头部转动设置")]
    public bool invertRotation = true; // 勾选解决反向问题
    public float baseYOffset = 0f;     // 基础修正角度

    [Header("智能归位设置")]
    public float autoAlignDelay = 1.5f; // 移动多久后归位
    public float alignSmooth = 5f;      // 归位顺滑度

    // --- 内部变量 ---
    private Quaternion _rawHeadRotation = Quaternion.identity; // 存储从耳机收到的最新数据
    private float _movingTimer = 0f;
    private float _currentBodyOffset = 0f; // 身体朝向的偏移修正

    // --- 初始化与事件绑定 ---
    void Start()
    {
        // 确保插件已初始化 (抄自你发的源码)
        HeadphoneMotion.Init();
        
        if (HeadphoneMotion.IsHeadphoneMotionAvailable())
        {
            HeadphoneMotion.StartTracking();
        }

        // 订阅事件：每当耳机有新数据，就通知 HandleHeadRotation
        HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotation;
    }

    void OnDestroy()
    {
        // 养成好习惯：脚本销毁时取消订阅，防止报错
        HeadphoneMotion.OnHeadRotationQuaternion -= HandleHeadRotation;
    }

    // --- 核心：接收耳机数据 ---
    // 这个函数会自动被插件调用
    private void HandleHeadRotation(Quaternion rotation)
    {
        _rawHeadRotation = rotation;
    }

    // --- 每帧处理逻辑 ---
    void Update()
    {
        // 1. 处理耳机数据 (解决反转)
        Vector3 headEuler = _rawHeadRotation.eulerAngles;
        float currentHeadYaw = headEuler.y;

        if (invertRotation)
        {
            currentHeadYaw = -currentHeadYaw;
        }

        // 2. 处理摇杆移动
        if (joystick != null)
        {
            Vector2 input = joystick.Direction;
            Vector3 moveDir = new Vector3(input.x, 0, input.y).normalized;

            if (input.magnitude > 0.1f)
            {
                // 玩家在移动
                transform.Translate(moveDir * 5f * Time.deltaTime, Space.World); // 假设速度是5
                _movingTimer += Time.deltaTime;

                // 3. 智能归位逻辑 (移动 > 1.5秒)
                if (_movingTimer > autoAlignDelay)
                {
                    // 计算前进方向的角度
                    float targetMoveYaw = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

                    // 计算需要的补偿值：补偿 = 目标角度 - 当前头转角度
                    // 这样当 Compensate + Head = Target
                    float targetOffset = targetMoveYaw - currentHeadYaw;

                    // 平滑插值
                    _currentBodyOffset = Mathf.LerpAngle(_currentBodyOffset, targetOffset, alignSmooth * Time.deltaTime);
                }
            }
            else
            {
                // 停止移动，重置计时
                _movingTimer = 0f;
            }
        }

        // 4. 应用最终旋转
        // 最终角度 = 耳机读数(处理过反转的) + 自动归位修正 + 基础修正
        float finalYaw = currentHeadYaw + _currentBodyOffset + baseYOffset;

        // 应用到物体 (只转 Y 轴)
        transform.rotation = Quaternion.Euler(0, finalYaw, 0);
    }
}