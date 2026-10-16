using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ch5()
    {
        SceneManager.LoadScene("ch5");
    }

    public void ch6()
    {
        SceneManager.LoadScene("ch6_GameScene");
    }
}
