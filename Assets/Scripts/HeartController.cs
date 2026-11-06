
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    GameObject player;
    GameObject director;

    // 愛心下落速度
    public float fallSpeed = 2.0f;

    // 愛心與玩家的碰撞半徑
    public float heartRadius = 0.5f;
    public float playerRadius = 1.0f;

    // 是否已經被取得
    bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        // 遊戲結束後停止愛心
        if (GameDirector.isGameOver)
        {
            return;
        }

        // 愛心向下掉落
        transform.Translate(
            0,
            -fallSpeed * Time.deltaTime,
            0
        );

        // 愛心超出畫面後消失
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
            return;
        }

        // 玩家碰撞判斷
        Vector2 heartPos = transform.position;
        Vector2 playerPos = player.transform.position;

        float distance = Vector2.Distance(
            heartPos,
            playerPos
        );

        // 碰到玩家
        if (distance < heartRadius + playerRadius)
        {
            CollectHeart();
        }
    }

    // 取得愛心
    void CollectHeart()
    {
        if (isCollected) return;

        if (GameDirector.isGameOver) return;

        isCollected = true;

        // 恢復玩家血量
        director.GetComponent<GameDirector>().HealHp();

        // 愛心消失
        Destroy(gameObject);
    }
}
