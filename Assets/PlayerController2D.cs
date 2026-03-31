using UnityEngine;
using HearXR;

public class PlayerController2D : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5.0f;
    public Joystick joystick;

    [Header("🎧 音效设置 (双通道)")]
    [Tooltip("拖入【踩雪】的 Audio Source")]
    public AudioSource winterSoundAudio;
    [Tooltip("拖入【普通路面(春夏/历史)】的 Audio Source")]
    public AudioSource normalSoundAudio; // ✨ 新增：普通脚步声

    [Header("🗺️ 地图检测 (拖入对应的父物体)")]
    public GameObject winterMapObject;  // 冬雪地图
    public GameObject springMapObject;  // ✨ 新增：春夏地图
    public GameObject historyMapObject; // ✨ 新增：历史地图

    [Header("旋转设置")]
    public float keyboardSensitivity = 2.0f;
    public bool reverseRotation = true;

    // 内部变量
    private Quaternion _lastHeadphoneRotation = Quaternion.identity;
    private float _initialYaw = 0f;
    private bool _isCalibrated = false;
    private float _simulatedYaw = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            HeadphoneMotion.Init();
            HeadphoneMotion.StartTracking();
        }
        HeadphoneMotion.OnHeadRotationQuaternion += OnHeadRotationUpdate;

        if (joystick == null) joystick = FindObjectOfType<Joystick>();
    }

    void OnDestroy()
    {
        HeadphoneMotion.OnHeadRotationQuaternion -= OnHeadRotationUpdate;
    }

    void OnHeadRotationUpdate(Quaternion rotation)
    {
        _lastHeadphoneRotation = rotation;
    }

    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.Q)) _simulatedYaw += keyboardSensitivity;
            if (Input.GetKey(KeyCode.E)) _simulatedYaw -= keyboardSensitivity;
            transform.rotation = Quaternion.Euler(0, 0, _simulatedYaw);
        }
        else
        {
            float currentYaw = _lastHeadphoneRotation.eulerAngles.y;
            if (!_isCalibrated && Time.time > 1.0f && Mathf.Abs(currentYaw) > 0.1f)
            {
                _initialYaw = currentYaw;
                _isCalibrated = true;
            }
            float relativeYaw = currentYaw - _initialYaw;
            float targetZ = reverseRotation ? relativeYaw : -relativeYaw;
            transform.rotation = Quaternion.Euler(0, 0, targetZ);
        }
    }

    void FixedUpdate()
    {
        // 1. 移动逻辑
        float moveX = 0f;
        float moveY = 0f;

        if (joystick != null)
        {
            moveX = joystick.Horizontal;
            moveY = joystick.Vertical;
        }

        if (moveX == 0 && moveY == 0)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }

        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // ==========================================
        // ✨ 智能双音效切换逻辑 ✨
        // ==========================================
        bool isMoving = (Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveY) > 0.01f);

        // 检测地图状态 (防止没拖物体报错，加了 ?. 保护)
        bool isWinter = (winterMapObject != null && winterMapObject.activeInHierarchy);
        // 春夏 或者 历史 任意一个开启，都算"普通路面"
        bool isNormal = (springMapObject != null && springMapObject.activeInHierarchy) || 
                        (historyMapObject != null && historyMapObject.activeInHierarchy);

        if (isMoving)
        {
            if (isWinter)
            {
                // 播放雪声，强制停止普通声
                PlaySound(winterSoundAudio);
                StopSound(normalSoundAudio);
            }
            else if (isNormal)
            {
                // 播放普通声，强制停止雪声
                PlaySound(normalSoundAudio);
                StopSound(winterSoundAudio);
            }
        }
        else
        {
            // 没动，两个都闭嘴
            StopSound(winterSoundAudio);
            StopSound(normalSoundAudio);
        }
    }

    // 小工具函数：安全播放
    void PlaySound(AudioSource audio)
    {
        if (audio != null && !audio.isPlaying) audio.Play();
    }

    // 小工具函数：安全停止
    void StopSound(AudioSource audio)
    {
        if (audio != null && audio.isPlaying) audio.Pause();
    }
}