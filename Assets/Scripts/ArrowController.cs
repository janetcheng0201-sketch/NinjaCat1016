
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject player;
    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        // 遊戲結束後停止箭頭
        if (GameDirector.isGameOver)
        {
            return;
        }

        float remainingTime = 30 - GameDirector.counttime;

        float speed = 0.1f;

        // 最後10秒增加箭頭速度
        if (remainingTime <= 10)
        {
            speed = 0.25f;
        }

        transform.Translate(0, -speed, 0);

        // 箭頭超出畫面後消失
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
            return;
        }

        // 玩家碰撞判斷
        Vector2 p1 = transform.position;
        Vector2 p2 = player.transform.position;

        Vector2 dir = p1 - p2;

        float d = dir.magnitude;

        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            director.GetComponent<GameDirector>().DecreaseHp();

            Destroy(gameObject);
        }
    }
}
