using UnityEngine;

public class IconManager : MonoBehaviour
{
    private bool areIconsVisible = true; // 记录当前状态

    // 这个函数绑定给“显/隐”按钮
    public void ToggleIcons()
    {
        areIconsVisible = !areIconsVisible; // 状态取反

        // 1. 找到场景里所有的声源 (SoundPoint)
        SoundPoint[] allPoints = FindObjectsOfType<SoundPoint>();

        // 2. 遍历它们，开关显示和点击
        foreach (SoundPoint sp in allPoints)
        {
            // 获取声源身上的渲染器(图片)和碰撞体(点击范围)
            SpriteRenderer sr = sp.GetComponent<SpriteRenderer>();
            Collider2D col = sp.GetComponent<Collider2D>();

            if (sr != null) sr.enabled = areIconsVisible;
            if (col != null) col.enabled = areIconsVisible;
            
            // 注意：我们没有动 AudioSource，所以声音不会断，只是图标看不见了
        }
    }
}