using System.Collections;
using UnityEngine;

public class HeartGenerator : MonoBehaviour
{
    public GameObject heartPrefab;

    public float spawnInterval = 5f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 5.5f;

    void Start()
    {
        if (heartPrefab == null)
        {
            Debug.LogError("HeartGenerator 沒有指定 Heart Prefab！");
            return;
        }

        StartCoroutine(SpawnHearts());
    }

    IEnumerator SpawnHearts()
    {
        while (!GameDirector.isGameOver)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minX, maxX),
                spawnY,
                0f
            );

            Instantiate(
                heartPrefab,
                spawnPosition,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}