using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject GameOver;

    [SerializeField] private GameObject Player;

    private float rotspeed = 500f;
    private bool BoosterDest = false;

    GameOverManager gmovma;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
        gmovma = GameObject.Find("GameManager").GetComponent<GameOverManager>();
    }

    // 몬스터 오크, 나이트 X

    // Update is called once per frame
    void Update()
    {
        if(BoosterDest == true)
        {
            this.transform.parent = null;
            this.gameObject.transform.Rotate(Vector3.back * rotspeed * Time.deltaTime * 4);       //(new Vector3(0, 0, rotspeed * Time.deltaTime));
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(100f, 100f, 0), Time.deltaTime * 40); //(new Vector3(20f * Time.deltaTime * 2, 20f * Time.deltaTime * 2, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Player") == 0)
        {
            if(Player.GetComponent<Character>().invincibility == false)
            {
                gmovma.GameOverOn();
            }
            else if (Player.GetComponent<Character>().invincibility == true)
            {
                BoosterDest = true;
                Destroy(gameObject, 3f);
            }
        }
    }
}
