using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eCharType
{
    Explorer,
    Healer,
    Assassin,
    Mimic,
    Magician
}

public class Character : MonoBehaviour
{
    private float jump = 14f;
    private float jump2 = 14f;

    int jumpCount = 0;

    private Animator animator;

    private float BoosterTime = 1.8f;
    private float indexspeed;
    public bool invincibility;

    private GameObject Map;

    [SerializeField] private GameObject Map2;
    [SerializeField] private GameObject Map2Back;
    [SerializeField] private GameObject Map2celling;
    [SerializeField] private GameObject Map1;
    [SerializeField] private GameObject Map1Back;
    [SerializeField] private GameObject Map1celling;

    GameOverManager gmovma;

    private void Awake()
    {
        Map2 = GameObject.Find("Map2");
        Map2Back = GameObject.Find("Crystal_Cave");
        Map2celling = GameObject.Find("001_ceiling (1)");

        Map1 = GameObject.Find("Map1");
        Map1Back = GameObject.Find("Basic_Cave");
        Map1celling = GameObject.Find("001_ceiling");

        animator = GetComponent<Animator>();

        Map = GameObject.FindWithTag("Map");
        invincibility = false;

        gmovma = GameObject.Find("GameManager").GetComponent<GameOverManager>();
    }
    private void Update()
    {
        if(this.gameObject.transform.position.y < -3)
        {
            gmovma.GameOverOn();
        }
    }

    public void BoosterOn()
    {
        indexspeed = Map.GetComponent<MapMove>().mapSpeed;
        StartCoroutine(Booster(BoosterTime, indexspeed));
    }

    IEnumerator Booster(float a, float b)
    {
        while (a > 0)
        {
            animator.SetBool("Booster", true);
            invincibility = true;
            Map.GetComponent<MapMove>().mapSpeed = b * 3;
            a--;
            yield return new WaitForSeconds(1);
        }
        Map.GetComponent<MapMove>().mapSpeed = b;



        animator.SetBool("Booster", false);
        yield return new WaitForSeconds(1.5f);
        invincibility = false;
    }

    public void Jump_Btn()
    {
        if (jumpCount == 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump, 0);
            jumpCount += 1;
        }
        else if (jumpCount == 1)
        {
            animator.SetBool("DoubleJump", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump2, 0);
            jumpCount += 1;
            Invoke("SetMove", 0.5f);
        }
    }
    public void SetMove()
    {
        animator.SetBool("DoubleJump", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Land") == 0)
        {
            jumpCount = 0;
        }
    }

    public void SlidBtn()
    {
        animator.SetBool("Sliding", true);
        this.GetComponent<BoxCollider2D>().size = new Vector2(3, 2);
    }

    public void SlidBtnUp()
    {
        animator.SetBool("Sliding", false);
        this.GetComponent<BoxCollider2D>().size = new Vector2(2.113647f, 3.741959f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == Map2)
        {
            Map1Back.SetActive(false);
            Map1celling.SetActive(false);
            Map2Back.SetActive(true);
            Map2celling.SetActive(true);
        }
    }
}
