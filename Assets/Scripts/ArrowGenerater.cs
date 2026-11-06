using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerater : MonoBehaviour
{
    public GameObject arrowPrefab;
    float span = 1.0f;
    float delta = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.isGameOver) return;

        float remainingTime = 30 - GameDirector.counttime;
        if (remainingTime <= 10)
        {
            span = 0.25f;  // 每 0.3 秒生成一支
        }
        else
        {
            span = 1.0f;  // 每 1 秒生成一支
        }

        
        delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0;
            GameObject go = Instantiate(arrowPrefab);
            float px = Random.Range(-8, 8);
            go.transform.position = new Vector3(px, 5.8f, 0);
        }
        //GameDirector.counttime
        
    }
}
