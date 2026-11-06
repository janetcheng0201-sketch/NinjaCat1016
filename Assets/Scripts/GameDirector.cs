using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hp;

    public TextMeshProUGUI show;

    public static float counttime = 0;
    public static bool isGameOver = false;

    public float maxHp = 1f;
    public float currentHp = 1f;

    public Image hpImage;

    void Start()
    {
        if (hpImage == null)
        {
            Debug.LogError("請在 Inspector 指定 HP Image！");
        }

        Time.timeScale = 1f;

        counttime = 0f;
        isGameOver = false;

        currentHp = maxHp;
        hpImage.fillAmount = currentHp / maxHp;

        hp = GameObject.Find("HP");
    }

    public void DecreaseHp()
    {
        if (isGameOver) return;

        currentHp -= 0.1f;
        currentHp = Mathf.Clamp(currentHp, 0f, maxHp);

        hpImage.fillAmount = currentHp / maxHp;

        if (currentHp <= 0f)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            Time.timeScale = 0f;
        }
    }

    public void HealHp()
    {
        if (isGameOver) return;

        currentHp += 0.1f;
        currentHp = Mathf.Clamp(currentHp, 0f, maxHp);

        hpImage.fillAmount = currentHp / maxHp;
    }

    // 其他原本的程式，例如 Update()、倒數計時等
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
            Time.timeScale = 1;
        }
        counttime += Time.deltaTime;
        float remainingTime = Mathf.Max(0, 30 - counttime);
        show.text = (30 - counttime).ToString("f1");
        if (counttime > 30)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("EndScene");
        }
    }

    
}