using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 讓雲朵沿著指定方向來回移動，並帶著站在上面的角色一起走。
/// 掛在雲朵（或 cloud prefab）上即可，雲需要有 Rigidbody2D(Kinematic) 與 Collider2D。
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CloudMover : MonoBehaviour
{
    [Header("移動設定")]
    [Tooltip("移動方向：(1,0)=左右、(0,1)=上下、(1,1)=斜向")]
    public Vector2 moveDirection = Vector2.right;

    [Tooltip("從起點出發的移動距離（到達後折返）")]
    public float distance = 3f;

    [Tooltip("移動速度（單位/秒）")]
    public float speed = 1.5f;

    [Tooltip("勾選後在兩端會慢慢減速、加速，看起來更自然")]
    public bool smooth = true;

    [Tooltip("起始時間偏移，讓多朵雲不要同步移動")]
    public float startOffset = 0f;

    Rigidbody2D rb;
    Collider2D col;
    Vector2 startPos;
    Vector2 dir;
    float time;
    readonly List<Rigidbody2D> riders = new List<Rigidbody2D>();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        startPos = rb.position;
        dir = moveDirection.normalized;
        time = startOffset;
    }

    void FixedUpdate()
    {
        if (distance <= 0f || speed <= 0f) return;

        time += Time.fixedDeltaTime;

        // t 會在 0 ~ distance 之間來回
        float t = Mathf.PingPong(time * speed, distance);
        if (smooth) t = Mathf.SmoothStep(0f, distance, t / distance);

        Vector2 newPos = startPos + dir * t;
        Vector2 delta = newPos - rb.position;

        rb.MovePosition(newPos);

        // 帶著站在雲上的角色一起移動（避免雲滑走、角色留在原地）
        for (int i = riders.Count - 1; i >= 0; i--)
        {
            if (riders[i] == null) { riders.RemoveAt(i); continue; }
            riders[i].position += delta;
        }
    }

    // 只有「踩在雲上方」的物體才會被帶著走，撞到側面或底部不算
    void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D other = collision.rigidbody;
        if (other == null || other.bodyType != RigidbodyType2D.Dynamic) return;

        bool isOnTop = collision.collider.bounds.min.y >= col.bounds.max.y - 0.15f;

        if (isOnTop)
        {
            if (!riders.Contains(other)) riders.Add(other);
        }
        else
        {
            riders.Remove(other);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody != null) riders.Remove(collision.rigidbody);
    }

    // 在 Scene 視窗畫出移動路線，方便調整距離
    void OnDrawGizmosSelected()
    {
        Vector3 from = Application.isPlaying ? (Vector3)startPos : transform.position;
        Vector3 to = from + (Vector3)(moveDirection.normalized * distance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(from, to);
        Gizmos.DrawWireSphere(from, 0.15f);
        Gizmos.DrawWireSphere(to, 0.15f);
    }
}