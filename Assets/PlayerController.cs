using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float minX = -8.0f;
    private float maxX = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.isGameOver) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-3);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(3);
        }
    }

    public void Rbutton()
    {
        if (GameDirector.isGameOver) return;
        Move(1);
    }

    public void Lbutton()
    {
        if (GameDirector.isGameOver) return;
        Move(-1);
    }

    void Move(float amount)
    {
        Vector3 pos = transform.position;
        pos.x += amount;

        // 使用 Mathf.Clamp 將 x 座標強制限制在 minX 與 maxX 之間
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        transform.position = pos;
    }
}
