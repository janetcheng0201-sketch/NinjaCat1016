using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        float remainingTime = 30 - GameDirector.counttime;

        float speed = 0.1f;

        if (remainingTime <= 10)
        {
            speed = 0.25f;
        }

        transform.Translate(0, -speed, 0);

        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }

        Vector2 p1 = transform.position;//箭頭圓心
        Vector2 p2 = this.player.transform.position;//角色圓心
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;
        float r2 = 1.0f;

        if (d < r1 + r2)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();
            Destroy(gameObject);
        }
    }
}
