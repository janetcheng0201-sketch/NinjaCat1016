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
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        hp = GameObject.Find("HP");
        isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
            Time.timeScale = 1;
        }
        counttime += Time.deltaTime;
        float remainingTime = Mathf.Max(0, 30 - counttime);
        show.text= (30-counttime).ToString("f1");
        if(counttime > 30)
        {
            Time.timeScale = 0;
        }
    }
    // Update is called once per frame
    public void DecreaseHp()
    {
        if (isGameOver) return;

        Image hpImage = hp.GetComponent<Image>();
        hpImage.fillAmount -= 0.1f;

        // 當血量小於或等於 0 時觸發遊戲結束
        if (hpImage.fillAmount <= 0)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            // 這裡未來如果想顯示 UI 的 Game Over 文字物件，也可以在這裡寫 activate
        }
    }

}
