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

    private float BoosterTime = 3.0f;
    private float indexspeed;
    public bool invincibility;

    private GameObject Map;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Map = GameObject.FindWithTag("Map");
        invincibility = false;
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
            invincibility = true;
            Map.GetComponent<MapMove>().mapSpeed = b * 3;
            a--;
            yield return new WaitForSeconds(1);
        }
        Map.GetComponent<MapMove>().mapSpeed = b;
        StartCoroutine(invinoff());
    }

    IEnumerator invinoff()
    {
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
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jump2, 0);
            jumpCount += 1;
        }
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
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90f);
        animator.SetBool("Sliding", true);
    }

    public void SlidBtnUp()
    {
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("Sliding", false);
    }
}
