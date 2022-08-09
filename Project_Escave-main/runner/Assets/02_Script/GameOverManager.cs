using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static bool gameOver = false;
    [SerializeField] private GameObject GameOver;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverOn()
    {
        gameOver = true;
        GameOver.SetActive(true);
        Time.timeScale = 0;
    }
}
