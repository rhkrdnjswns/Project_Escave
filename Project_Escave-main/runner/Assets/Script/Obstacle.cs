using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    public static bool gameOver = false;

    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Player") == 0)
        {
            if(Player.GetComponent<Character>().invincibility == false)
            {
                gameOver = true;
                GameOver.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
