using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;
    public static bool gameOver = false;

    [SerializeField] private GameObject Player;

    private float rotspeed = 700f;
    private bool BoosterDest = false;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
    }

    // 몬스터 오크, 나이트 X

    // Update is called once per frame
    void Update()
    {
        if(BoosterDest == true)
        {
            this.transform.parent = null;
            this.gameObject.transform.Rotate(new Vector3(0, 0, rotspeed * Time.deltaTime));
            this.gameObject.transform.Translate(new Vector3(20f * Time.deltaTime * 5, 20f * Time.deltaTime * 5, 0));
        }
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
            else if (Player.GetComponent<Character>().invincibility == true)
            {
                BoosterDest = true;
                Destroy(gameObject, 3f);
            }
        }
    }
}
